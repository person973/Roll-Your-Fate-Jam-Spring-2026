using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Change_Scene : MonoBehaviour
{
    public void NewScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
