using UnityEngine;
using System.Collections;

public class CustomPlayerMovement : MonoBehaviour {

	OVRPlayerController controller;

	public GameObject orangeTerrain;
	public GameObject blueTerrain;

	public int totalLevels;

	public Camera leftEye, rightEye;
	Color orange = new Color(1,0.65f,0);
	Color blue = Color.cyan;

	bool blinked = false;
	public bool dead = false;

	Vector3 startPos;
	Vector3 deathPos;

	// Use this for initialization
	void Awake () {
		startPos = transform.position;
		controller = this.gameObject.GetComponent<OVRPlayerController>();

		orangeTerrain.SetActive (false);
		blueTerrain.SetActive (true);

		leftEye.backgroundColor = blue;
		rightEye.backgroundColor = blue;
	}
	
	// Update is called once per frame
	void Update () {
		if (!dead) {
			if (Input.GetAxis ("Jump") > 0) {
				controller.Jump ();
			}
		
			if (!blinked && Input.GetAxis ("Blink") > 0) {
				Blink ();
				blinked = true;
			} else if (Input.GetAxis ("Blink") == 0) {
				blinked = false;
			}
		} else {
			//transform.position = deathPos;
			controller.HaltUpdateMovement = true;
			if (Input.GetAxis("Blink") > 0) {
				transform.position = startPos;
				dead = false;
				controller.HaltUpdateMovement = false;
			}
		}
	}

	void Blink() {
		orangeTerrain.SetActive (!orangeTerrain.activeSelf);
		blueTerrain.SetActive (!blueTerrain.activeSelf);

		Color switchTo = (orangeTerrain.activeSelf ? orange : blue);
		leftEye.backgroundColor = switchTo;
		rightEye.backgroundColor = switchTo;
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag.Equals ("Deadly")) {
			dead = true;
			print ("Dead");
			controller.HaltUpdateMovement = true;	
			deathPos = this.transform.position;
		}


		if (other.gameObject.tag.Equals ("Bouncy")) {
			controller.Bounce();
		}


		if (other.gameObject.tag.Equals ("Portal")) {
			Application.LoadLevel((Application.loadedLevel+1)%totalLevels);
		}

	}


}
