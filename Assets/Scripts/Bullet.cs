using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public Vector2 path;
    public float life_time;
    public float bullet_speed = 10;

    void Start() {
        life_time = 0;
    }

    public void Move(Vector2 path) {
        GetComponent<Rigidbody2D>().velocity = path.normalized * bullet_speed;
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.name == "Asteroid")
        Death();
    }

    void Update() {
        life_time += 0.1f;
        if (life_time > 40) Death();
    }
    
    void Death() {
        life_time = 0;
        Spawner.PutObjectIntoPool(this.gameObject);
    }
}
