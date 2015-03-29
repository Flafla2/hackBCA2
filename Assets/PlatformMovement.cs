using UnityEngine;
using System.Collections;

public class PlatformMovement : MonoBehaviour {
	public GameObject WholeBlueTerrain,WholeOrangeTerrain;
	public Transform startMarker;
	public Transform endMarker;
	public bool isBlue;
	
	Transform temp, OGStart, OGEnd;
	public float speed = 1.0F;
	private float startTime;
	private float journeyLength;
	//bool goingForward = true;
	
	public float smooth = 5.0F;
	float prevX,prevY,prevZ,newX,newY,newZ;
	void Start() {
		OGStart = startMarker; OGEnd = endMarker;
		endMarker.transform.parent = null;
		startMarker.transform.parent = null;
		prevX = this.transform.position.x;
		prevY = this.transform.position.y;
		prevZ = this.transform.position.z;
		
		startTime = Time.time;
		journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
		if (isBlue) {
			this.transform.parent = WholeBlueTerrain.transform;
		} else {
			this.transform.parent = WholeOrangeTerrain.transform;
		}
		
	}
	void FixedUpdate() {
		//print (isBlue);
		prevX = this.transform.position.x;
		prevY = this.transform.position.y;
		prevZ = this.transform.position.z;
		if (this.transform.position == endMarker.position) {
			temp = startMarker;
			startMarker = endMarker;
			endMarker = temp;
			startTime = Time.time;
			journeyLength = Vector3.Distance (startMarker.position, endMarker.position);
		}
		
		
		
		
		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;
		transform.position = Vector3.Lerp (startMarker.position, endMarker.position, fracJourney);
		newX = this.transform.position.x;
		newY = this.transform.position.y;
		newZ = this.transform.position.z;
		
	}
	
	
	void OnTriggerStay (Collider other) {
		print ("work");
		if (other.gameObject.tag.Equals("Character")) {
			print ("noice?");
			//print(other.rigidbody.velocity);
			//	print (newX-prevX);
			//other.GetComponent<Rigidbody>().position = new Vector3(other.gameObject.transform.position.x+newX-prevX, other.gameObject.transform.position.y+newY-prevY,other.gameObject.transform.position.z+newZ-prevZ);
			other.transform.position = new Vector3(other.gameObject.transform.position.x+newX-prevX, other.gameObject.transform.position.y+newY-prevY,other.gameObject.transform.position.z+newZ-prevZ);
			
			
		} else {
			other.transform.parent=null;
		}
	}
	
}
