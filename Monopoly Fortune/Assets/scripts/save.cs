using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class save : MonoBehaviour
{
    public void CreateSceneCopy()
    {
        string sceneName = "continue";
        string[] scenePaths = AssetDatabase.FindAssets("t:Scene " + sceneName);

        if (scenePaths.Length > 0)
        {
            string scenePath = AssetDatabase.GUIDToAssetPath(scenePaths[0]);
            EditorSceneManager.CloseScene(SceneManager.GetSceneByPath(scenePath), true);
            AssetDatabase.DeleteAsset(scenePath);
        }

        // Получаем информацию о текущей активной сцене
        Scene currentScene = SceneManager.GetActiveScene();

        // Если игра находится в режиме проигрывания, остановить проигрывание
        bool isPlaying = EditorApplication.isPlaying;
        if (isPlaying)
            EditorApplication.isPlaying = false;

        // Создаем новую пустую сцену
        Scene newScene = SceneManager.CreateScene(sceneName);

        // Копируем все объекты из текущей сцены в новую сцену
        foreach (GameObject obj in currentScene.GetRootGameObjects())
        {
            GameObject newObj = Instantiate(obj);
            SceneManager.MoveGameObjectToScene(newObj, newScene);
        }

        // Сохраняем новую сцену
        string newScenePath = "Assets/Scenes/" + sceneName + ".unity";
        EditorSceneManager.SaveScene(newScene, newScenePath);

        // Если игра была в режиме проигрывания, возобновить проигрывание
        if (isPlaying)
            EditorApplication.isPlaying = true;
    }
}
