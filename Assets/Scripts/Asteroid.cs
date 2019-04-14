using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {
    
    public Vector2 path;
    public float asteroid_speed = 2f;
    public float life_time;
    GameObject player;

	void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        life_time = 0;
	}

    private void OnCollisionEnter2D(Collision2D collision){
        Death();
    }

    public void Move(Vector2 path) {
        GetComponent<Rigidbody2D>().velocity = path.normalized * asteroid_speed;
    }

    void Update() {
        if (PauseMenu.state == PauseMenu.GameState.pause) life_time += 0.1f;
        if (life_time > 50 || (transform.position - player.transform.position).magnitude > 20) Death();
	}
   
    void Death() {
        Spawner.PutObjectIntoPool(gameObject);
        life_time = 0;
        Spawner.asteroid_count--;
    }
}
