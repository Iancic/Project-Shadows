using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Demo");
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
