using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public void LoadNext()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextSceneIndex);
    }
}