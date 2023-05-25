using UnityEngine.SceneManagement;
using UnityEngine;

public class loading_game_scene : MonoBehaviour
{
    public string sceneName; 

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }

}
