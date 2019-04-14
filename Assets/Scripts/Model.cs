using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class Model {

    public Player player = new Player();
    public List<Level> levels_list = new List<Level>();
    public int activeLevelNumber;

    public static Model model = new Model(); //Singleton

    public Level ActiveLevel {
        get { return levels_list[activeLevelNumber]; }
    }
    
    public void AddLevel(int win_timer = 30) {
        if(levels_list.Count == 0 || levels_list[levels_list.Count - 1].game_status == Level.Status.done)
            levels_list.Add(new Level(Level.Status.open, win_timer));
        else
            levels_list.Add(new Level(Level.Status.closed, win_timer));
    }

    public void LevelComplete(int level_number) {
        levels_list[level_number].game_status = Level.Status.done;
        if(levels_list[level_number + 1] != null)
            levels_list[level_number + 1].game_status = Level.Status.open;
    }
    
    public static void SaveGame() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/gamesave.save");
        bf.Serialize(file, model);
        file.Close();
    }
    
    public static void LoadGame() {
        BinaryFormatter bf = new BinaryFormatter();
        if(File.Exists(Application.dataPath + "/gamesave.save")) {
            FileStream file = File.Open(Application.dataPath + "/gamesave.save", FileMode.Open);
            model = (Model)bf.Deserialize(file);
            file.Close();
        }
        if (model.levels_list.Count == 0) model.AddLevel();
    }
}


[System.Serializable]
public class Level {
    public enum Status { open, closed, done }
    public Status game_status;
    public int WinCondiitionTimer;

    public Level(Status status, int win_timer = 30) {
        WinCondiitionTimer = win_timer;
        game_status = status;
    }
}