using UnityEngine;
using System.Collections;

public class CustomPlayerMovement : MonoBehaviour {

	OVRPlayerController controller;

	public GameObject orangeTerrain;
	public GameObject blueTerrain;

	int totalLevels = 3;

	public Camera leftEye, rightEye;
	Color orange = new Color(1,0.65f,0);
	Color blue = Color.cyan;

	bool blinked = false;
	public bool dead = false;

	Vector3 startPos;
	Quaternion startRot;
	Vector3 deathPos;

	// Use this for initialization
	void Awake () {
		startPos = transform.position;
		startRot = transform.rotation;
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
			transform.position = deathPos;
			
			if (Input.GetAxis("Blink") > 0) {
				transform.position = startPos;
				transform.rotation = startRot;
				dead = false;
				controller.enabled = true;
				controller.HaltUpdateMovement = false;
				orangeTerrain.SetActive(false);
				blueTerrain.SetActive(true);
				Color switchTo = (orangeTerrain.activeSelf ? orange : blue);
				leftEye.backgroundColor = switchTo;
				rightEye.backgroundColor = switchTo;
				blinked = true;
				controller.MoveThrottle = new Vector3(0,0,0);
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
			if (!dead) {
				deathPos = this.transform.position;

				print ("test");
			}
			dead = true;
			print ("Dead");
			controller.HaltUpdateMovement = true;
			controller.enabled = false;
		}


		if (other.gameObject.tag.Equals ("Bouncy")) {
			controller.MoveThrottle = new Vector3(0,0,0);
			controller.FallSpeed = 0;
			controller.Bounce();
		}


		if (other.gameObject.tag.Equals ("Portal")) {
			Application.LoadLevel((Application.loadedLevel+1)%totalLevels);
		}

	}


}
