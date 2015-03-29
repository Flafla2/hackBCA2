using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO.Ports;
using Blink.Hardware;

public class COMPortManager : MonoBehaviour {

    public static string _CurrentPort;
    public static string CurrentPort
    {
        get { return _CurrentPort; }
        set { _CurrentPort = value; ArduinoFanInterface.serial = new SerialPort(_CurrentPort, 9600); }
    }

    public Transform ButtonHolder;
    public Transform ButtonPrefab;
    public Text currentPortText;

    void Start()
    {
        UpdateCOMPorts();

        if(CurrentPort == null || CurrentPort.Equals("")) {
            string[] ports = SerialPort.GetPortNames();
            if(ports.Length > 0)
            CurrentPort = ports[0];
        }
    }

    void Update()
    {
        currentPortText.text = "SELECT CURRENT PORT (current: " + CurrentPort + ")";
    }

    public void UpdateCOMPorts()
    {
        foreach (Transform c in ButtonHolder)
        {
            if (c.GetComponent<COMPortButton>() != null)
                Destroy(c.gameObject);
        }

        string[] ports = SerialPort.GetPortNames();
        
        foreach (string port in ports)
        {
            Transform c = Instantiate(ButtonPrefab) as Transform;
            COMPortButton b = c.GetComponent<COMPortButton>();

            b.port = port;
            c.GetComponentInChildren<Text>().text = port;

            c.SetParent(ButtonHolder, false);
        }
    }
}
