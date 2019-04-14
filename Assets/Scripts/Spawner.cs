using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Spawner : MonoBehaviour {

    static Dictionary<string, LinkedList<GameObject>> poolsDictionary
                    = new Dictionary<string, LinkedList<GameObject>>();
        
    public static int asteroid_count;
    GameObject player;
    public GameObject asteroid;
    public int kills_count = 0;
    public Vector3 pos, path;
    public int max_asteroid_count = 10;

    //управление пулом
    public static GameObject GetObjectFromPool(GameObject prefab, Vector2 position, GameObject parent) {

        if (!poolsDictionary.ContainsKey(prefab.name)) {
            poolsDictionary[prefab.name] = new LinkedList<GameObject>();
        }

        GameObject result;
        
        if (poolsDictionary[prefab.name].Count > 0) {
            result = poolsDictionary[prefab.name].First.Value;
            poolsDictionary[prefab.name].RemoveFirst();
            result.transform.position = position;
            result.SetActive(true);
            return result;
        }

        result = Instantiate(prefab);
        result.name = prefab.name;
        result.transform.position = position;
        result.transform.parent = parent.transform;

        return result;
    }

    public static void PutObjectIntoPool(GameObject target) {
        poolsDictionary[target.name].AddFirst(target);
        target.SetActive(false);
    }
    
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        asteroid_count = 0;
    }

    void Spawn() {
        pos = new Vector3(player.transform.position.x + Random.value * 20 - 10, 
                          player.transform.position.y + Random.value * 20 - 10, 0);

        if ((pos - player.transform.position).magnitude > 5) {
            path = player.transform.position - pos + new Vector3(Random.value * 10 - 5, Random.value * 10 - 5, 0);
            GetObjectFromPool(asteroid, pos, gameObject).GetComponent<Asteroid>().Move(path);
            asteroid_count++;
        }
    }

    void Update() {
        if(asteroid_count < max_asteroid_count) Spawn();
    }
}
