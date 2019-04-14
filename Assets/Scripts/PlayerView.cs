using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class PlayerView : MonoBehaviour {

    public Text lives;
    public Text timer;
    public int min, sec;
    public int lives_cnt;
    public int time;
    public ReactiveProperty<bool> TimeIsOut = new ReactiveProperty<bool>();

    void Start () {
        lives = GameObject.Find("Canvas/Lives").GetComponent<Text>();
        timer = GameObject.Find("Canvas/Timer").GetComponent<Text>();
        timer.text = "00:00";
        lives.text = "Lives: ";
    }

    
    public void SetTimer(int seconds) {
        time = (int)Time.timeSinceLevelLoad + seconds;
    }

    public void SetLives(int lives_count) {
        lives_cnt = lives_count;
    }

    public void ShowStats() {
        min = (int)((time - Time.timeSinceLevelLoad) / 60);
        sec = (int)((time - Time.timeSinceLevelLoad) % 60);
        timer.text = ((min < 10) ? "0" + min.ToString() : min.ToString()) + ":" +
                     ((sec < 10) ? "0" + sec.ToString() : sec.ToString());
    }

    void Update () {
        TimeIsOut.Value = time < Time.timeSinceLevelLoad;
        if (!TimeIsOut.Value) ShowStats();
    }
}
