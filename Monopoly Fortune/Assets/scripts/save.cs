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

        // �������� ���������� � ������� �������� �����
        Scene currentScene = SceneManager.GetActiveScene();

        // ���� ���� ��������� � ������ ������������, ���������� ������������
        bool isPlaying = EditorApplication.isPlaying;
        if (isPlaying)
            EditorApplication.isPlaying = false;

        // ������� ����� ������ �����
        Scene newScene = SceneManager.CreateScene(sceneName);

        // �������� ��� ������� �� ������� ����� � ����� �����
        foreach (GameObject obj in currentScene.GetRootGameObjects())
        {
            GameObject newObj = Instantiate(obj);
            SceneManager.MoveGameObjectToScene(newObj, newScene);
        }

        // ��������� ����� �����
        string newScenePath = "Assets/Scenes/" + sceneName + ".unity";
        EditorSceneManager.SaveScene(newScene, newScenePath);

        // ���� ���� ���� � ������ ������������, ����������� ������������
        if (isPlaying)
            EditorApplication.isPlaying = true;
    }
}
