using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public GameObject prefabButton;
    List<Button> level_buttons = new List<Button>();

    void Start() {
        Model.LoadGame();
        InitButtons();
        InitLevels();
    }

    void InitButtons() {
        GameObject button = Instantiate(prefabButton);
        button.transform.position = new Vector3(Screen.width - 100, Screen.height - 100, 0);
        button.transform.SetParent(gameObject.transform.parent);
        button.transform.name = "Reset levels";

        Button temp_button = button.GetComponent<Button>();
        temp_button.GetComponentInChildren<Text>().text = "Reset to 1 level";
        temp_button.onClick.AddListener(() => ResetLevels());
        
        button = Instantiate(prefabButton);
        button.transform.position = new Vector3(Screen.width - 100, Screen.height - 150, 0);
        button.transform.SetParent(gameObject.transform.parent);
        button.transform.name = "Reset ten levels";

        temp_button = button.GetComponent<Button>();
        temp_button.GetComponentInChildren<Text>().text = "Reset to 10 level";
        temp_button.onClick.AddListener(() => ResetLevels(10));

        button = Instantiate(prefabButton);
        button.transform.position = new Vector3(Screen.width - 100, Screen.height - 550, 0);
        button.transform.SetParent(gameObject.transform.parent);
        button.transform.name = "Exit";

        temp_button = button.GetComponent<Button>();
        temp_button.GetComponentInChildren<Text>().text = "Exit";
        temp_button.onClick.AddListener(() => Application.Quit());
    }

    void InitLevels() {

        foreach(Transform child in transform ) {
            Destroy(child.gameObject);
        }

        int levelNumber = 0;
        foreach (Level lvl in Model.model.levels_list) {
            int button_height = Random.Range(100, Screen.height - 100);
            int button_widht = Random.Range(100, Screen.width - 100);

            GameObject button = Instantiate(prefabButton);
            button.transform.position = new Vector3(button_widht, button_height, 0);
            button.transform.SetParent(gameObject.transform);
            button.transform.name = "Button level " + (levelNumber + 1);

            Button temp_button = button.GetComponent<Button>();
            temp_button.GetComponentInChildren<Text>().text = Model.model.levels_list[levelNumber].game_status.ToString() + (levelNumber + 1).ToString();
            temp_button.interactable = !(Model.model.levels_list[levelNumber].game_status == Level.Status.closed);
            temp_button.GetComponent<Image>().color = (Model.model.levels_list[levelNumber].game_status == Level.Status.done) ? Color.green : Color.white;
            level_buttons.Add(temp_button);
            temp_button.onClick.AddListener(() => LoadLevel(level_buttons.IndexOf(temp_button)));
            levelNumber++;
        }
    }

    void LoadLevel(int level_number) {
        Model.model.activeLevelNumber = level_number;
        Debug.Log("Start lvl " + Model.model.activeLevelNumber);
        SceneManager.LoadScene("PlayZone", LoadSceneMode.Single);
    }

    void ResetLevels(int levels_count = 1) {
        Model.model.levels_list.Clear();
        for(int i = 0; i < levels_count; i++) {
            Model.model.AddLevel();
        }
        Model.SaveGame();
        InitLevels();
    }

}
