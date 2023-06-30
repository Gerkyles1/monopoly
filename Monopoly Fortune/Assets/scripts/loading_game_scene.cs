using UnityEngine;
using UnityEngine.SceneManagement;

public class loading_game_scene : MonoBehaviour
{

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
