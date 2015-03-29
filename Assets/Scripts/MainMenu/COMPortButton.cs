using UnityEngine;
using System.Collections;

public class COMPortButton : MonoBehaviour {

    public string port;

    public void Click()
    {
        COMPortManager.CurrentPort = port;
    }
}
