using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    private Scene scene;
    private bool isLoading = false;
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    public void NavigateToScene(string sceneName){
        if(scene.name != sceneName && isLoading == false){
            SceneManager.LoadSceneAsync(sceneName);
            isLoading = true;
            return;
        }
        Debug.Log("Already in Scene");
    }

    public void NavigateToNextInSequence(){
        SceneManager.LoadScene(scene.buildIndex + 1);
    }

    public static void CloseGame(){
        Debug.Log("Game Closed");
        Application.Quit();
    }
}
