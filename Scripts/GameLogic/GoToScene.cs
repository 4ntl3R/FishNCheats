using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : Singleton<GoToScene>
{

   public void NextLevel(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void ExitPressed()
    {
        Application.Quit();
    }
}
