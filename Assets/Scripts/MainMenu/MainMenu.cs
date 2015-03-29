using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public string FirstScene;

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        Application.LoadLevel(FirstScene);
    }
}
