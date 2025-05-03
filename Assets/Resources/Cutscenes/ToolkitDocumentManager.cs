using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToolkitDocumentManager : MonoBehaviour
{
    public static event Action fadeOut;

    public static void transitionScene(string scene)
    {
        fadeOut?.Invoke();
        SceneManager.LoadSceneAsync(scene);
    }

}
