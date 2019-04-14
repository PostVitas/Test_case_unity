using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

[System.Serializable]
public class Player  {
    
    public ReactiveProperty<int> lives { get; set; }
    public float movement_speed { get; set; }
    
    public Player() {
        lives = new ReactiveProperty<int>(3);
        movement_speed = 5;
    }
}
