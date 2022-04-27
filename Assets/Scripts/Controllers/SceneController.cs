using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject GameOverlay;
    [SerializeField] private GameObject MenuOverlay;
    [SerializeField] private GameObject SubmitOverlay;

    private bool showMenu = false;

    private void Update() {
        
        if(Input.GetKeyDown(KeyCode.Escape)){
            UpdateVisibility();
        }
    }

    public void NavigateToScene(string sceneName){
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name != sceneName){
            SceneManager.LoadSceneAsync(sceneName);
            return;
        }
        Debug.Log("Already in Scene");
    }

    public void NavigateToNextInSequence(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void CloseGame(){
        Debug.Log("Game Closed");
        Application.Quit();
    }

    public void ShowRecord(){
        GameOverlay.SetActive(false);
        MenuOverlay.SetActive(false);
        SubmitOverlay.SetActive(true);
    }

    public void UpdateVisibility(){
        showMenu = !showMenu;
        if(showMenu) Time.timeScale = 0;
        else Time.timeScale = 1;
        GameOverlay.SetActive(!showMenu);
        MenuOverlay.SetActive(showMenu);
    }
}
