using UnityEngine;
using System.Collections;
using Blink.Hardware;

public class MainMenu : MonoBehaviour {

    public string FirstScene;

    public void ExitGame()
    {
        ArduinoFanInterface.EndArduino();
        Application.Quit();
    }

    public void PlayGame()
    {
        Application.LoadLevel(FirstScene);
    }
}
