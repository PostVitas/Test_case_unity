using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour {

    public enum GameState { play, pause, win, lose}
    
    GameObject pauseMenuUI;
    public Button quit;
    public Button resume;
    public Button restart;
    public Button next;
    public static GameState state;

    private void Start() {
        pauseMenuUI = GameObject.Find("Canvas/PauseMenu");
        state = GameState.play;
        restart.gameObject.SetActive(false);
        next.gameObject.SetActive(false);
        resume.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update () {

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Debug.Log("pause");
            switch(state){
                case GameState.pause:
                    Resume();
                    break;
                case GameState.play:
                    state = GameState.pause;
                    Pause(GameState.pause);
                    break;
            }
        }

        pauseMenuUI.SetActive(state != GameState.play);
        if (state != GameState.play) {
            Pause(state);
        }

        Debug.Log(state);
    }

    public void Resume() {
        restart.gameObject.SetActive(false);
        next.gameObject.SetActive(false);
        resume.gameObject.SetActive(false);
        Debug.Log("res");
        state = GameState.play;
        Time.timeScale = 1f;
    }

    public void Restart() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("PlayZone", LoadSceneMode.Single);
    }

    public void QuitToMenu() {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        SceneManager.LoadScene("LevelSelector", LoadSceneMode.Single);
    }

    public void NextLevel() {
        Time.timeScale = 1f;
        if (Model.model.levels_list[Model.model.activeLevelNumber + 1] != null) {
            Model.model.activeLevelNumber++;
            SceneManager.LoadScene("PlayZone", LoadSceneMode.Single);
        }
        else {
            SceneManager.LoadScene("LevelSelector", LoadSceneMode.Single);
        }
    }

    public static void Lose() {
        state = GameState.lose;
    }

    public static void Win() {
        state = GameState.win;
        Debug.Log("Win lvl " + Model.model.activeLevelNumber);
        Model.model.LevelComplete(Model.model.activeLevelNumber);
        Model.SaveGame();
    }

    void Pause(GameState gameState) {
        switch (gameState) {
            case GameState.pause:
                resume.gameObject.SetActive(true);
                break;
            case GameState.win:
                next.gameObject.SetActive(true);
                break;
            case GameState.lose:
                restart.gameObject.SetActive(true);
                break;
        }
        Time.timeScale = 0f;
    }
}
