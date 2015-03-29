using UnityEngine;
using System.Collections;
using System.IO.Ports;

namespace Blink.Hardware
{
    public class ArduinoFanInterface
    {
        public static SerialPort serial;

        public static void StartArduino()
        {
            if (serial == null)
            {
                Debug.LogError("Serial is null and we are trying to start the arduino!");
                return;
            }
            serial.Open();
        }

        public static void EndArduino()
        {
            if (serial == null)
            {
                Debug.LogError("Serial is null and we are trying to end the arduino!");
                return;
            }
            serial.Close();
        }

        public static void UpdateArduino(float angle, float magnitude, bool rumble)
        {
            int fan = (int)(angle / 45);
            Debug.Log(fan);
            if (serial.IsOpen == false)
                serial.Open();

            if (magnitude > 0)
            {
                switch (fan)
                {
                    case 0:
                        serial.Write("a");
                        break;
                    case 1:
                        serial.Write("b");
                        break;
                    case 2:
                        serial.Write("c");
                        break;
                    case 3:
                        serial.Write("d");
                        break;
                    case 4:
                        serial.Write("e");
                        break;
                    case 5:
                        serial.Write("f");
                        break;
                    case 6:
                        serial.Write("g");
                        break;
                    case 7:
                        serial.Write("h");
                        break;
                }
            }
        }
    }
}
