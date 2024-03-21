using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Arena");
    }

    public void SelectChar()
    {
        SceneManager.LoadScene("CharacterMenu");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Arena");
    }

    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        // If we're in the Unity Editor, stop playing
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
