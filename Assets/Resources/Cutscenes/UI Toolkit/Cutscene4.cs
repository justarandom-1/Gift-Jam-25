using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene4 : DocumentTemplate
{
    protected override void generateContent()
    {
        transitionScene();
        throw new System.NotImplementedException();
    }

    protected override void nextSceneRequested()
    {
        SceneManager.LoadSceneAsync("Menu");
        throw new System.NotImplementedException();
    }
}
