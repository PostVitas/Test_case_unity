using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory
{
    public Controller controller { get; private set; }
    public PlayerView view       { get; private set; }
    public Spawner    spawner    { get; private set; }

    
    public void Load() {

        Debug.Log("loading");
        GameObject prefab, instance;
        if(GameObject.Find("Player") == null) {
            prefab = Resources.Load<GameObject>("Player");
            instance = Object.Instantiate(prefab);
            instance.name = prefab.name;
            view = instance.GetComponent<PlayerView>();
            controller = instance.GetComponent<Controller>();
        }

        if (GameObject.Find("Spawner") == null) {
            prefab = Resources.Load<GameObject>("Spawner");
            instance = Object.Instantiate(prefab);
            instance.name = prefab.name;
            spawner = instance.GetComponent<Spawner>();
        }
    }

}
