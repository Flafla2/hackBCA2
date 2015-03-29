using UnityEngine;
using System.Collections;

public class CustomPlayerMovement : MonoBehaviour {

	OVRPlayerController controller;

	public GameObject orangeTerrain;
	public GameObject blueTerrain;

	public Camera leftEye, rightEye;
	Color orange = new Color(1,0.65f,0);
	Color blue = Color.cyan;

	bool blinked = false;

	// Use this for initialization
	void Awake () {
		controller = this.gameObject.GetComponent<OVRPlayerController>();

		orangeTerrain.SetActive (false);
		blueTerrain.SetActive (true);

		leftEye.backgroundColor = blue;
		rightEye.backgroundColor = orange;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("Jump") > 0) {
			controller.Jump();
		}
		
		if (!blinked && Input.GetAxis ("Blink") > 0) {
			Blink ();
			blinked = true;
		} else if (Input.GetAxis ("Blink") == 0) {
			blinked = false;
		}
	}

	void Blink() {
		orangeTerrain.SetActive (!orangeTerrain.activeSelf);
		blueTerrain.SetActive (!blueTerrain.activeSelf);

		Color switchTo = (orangeTerrain.activeSelf ? orange : blue);
		leftEye.backgroundColor = switchTo;
		rightEye.backgroundColor = switchTo;
	}
}
