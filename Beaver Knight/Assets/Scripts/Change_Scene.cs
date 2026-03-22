using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Change_Scene : MonoBehaviour
{
    /// <summary>
    /// Starts the game at level 1
    /// </summary>
    public void ToLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    /// <summary>
    /// Starts the game at level 2
    /// </summary>
    public void ToLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    /// <summary>
    /// Starts the game at level 3
    /// </summary>
    public void ToLevel3()
    {
        SceneManager.LoadScene("Level3");
    }

    /// <summary>
    /// Changes the scene to the main menu
    /// </summary>
    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Changes the scene to the level select
    /// </summary>
    public void ToLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void ToQuit()
    {
        
    }
}
