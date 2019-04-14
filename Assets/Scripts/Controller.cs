using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Controller : MonoBehaviour {

    
    PlayerView view;
    Vector2 bullet_path;
    public GameObject bullet_prefab;
    public float reload_time = 0.5f;
    float last_shot;

    
    void Start() {
        Model.model.player.lives.Value = 3;
        view = gameObject.GetComponent<PlayerView>();
        view.SetTimer(Model.model.ActiveLevel.WinCondiitionTimer);
        view.SetLives(Model.model.player.lives.Value);
        
        Observable.EveryUpdate()
                .Where(_ => Input.GetMouseButton(0))
                .Subscribe(_ => Shoot());

        Model.model.player.lives
            .ObserveEveryValueChanged(x => x.Value)
            .Subscribe(x => view.lives.text = "Lives: " + x);

        Model.model.player.lives
            .ObserveEveryValueChanged(x => x.Value)
            .Where(xs => xs == 0)
            .Subscribe(_ => Death());

        this.OnCollisionEnter2DAsObservable()
            .Where(coll => coll.collider.gameObject.name == "Asteroid")
            .Subscribe(_ => Model.model.player.lives.Value--);

        view.TimeIsOut
            .ObserveEveryValueChanged(x => x.Value)
            .Where(xs => xs)
            .Subscribe(_ => Win());
        
    }

    void Update() {
        Move();
    }
    
    void Death() {
        Debug.Log("dead");
        PauseMenu.Lose();
    }

    void Win() {
        Debug.Log("WIN!");
        PauseMenu.Win();
    }

    void Move() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
        GetComponent<Rigidbody2D>().velocity = movement * Model.model.player.movement_speed;

        if (moveHorizontal != 0 || moveVertical != 0)
            GetComponent<Rigidbody2D>().rotation = Mathf.Atan2(-moveHorizontal, moveVertical) * 180 / Mathf.PI;
    }

    public void Shoot() {
        if (Time.time - last_shot > reload_time) {
            bullet_path = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x,
                                      Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y);
            Spawner.GetObjectFromPool(bullet_prefab, transform.position, gameObject).GetComponent<Bullet>().Move(bullet_path);
            last_shot = Time.time;
        }
    }

}
