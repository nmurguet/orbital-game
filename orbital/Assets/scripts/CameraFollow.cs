using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Camera myCamera; 

	public Transform target;

	public float speed; 

	public bool following; 
	public float offset_x;
	public float offset_y; 


	public float zoom; 



	private float store_zoom; 
	private bool zoomout; 
	// Use this for initialization
	void Start () {
		following = true; 
		zoomout = false; 
		
	}
	
	// Update is called once per frame
	void Update () {

		zoom = Input.GetAxis ("Mouse ScrollWheel");
		if (zoom > 0f) {
			myCamera.orthographicSize -= 1f; 

		}
		if (zoom < 0f) {
			myCamera.orthographicSize += 1f; 

		}


		if (following == true) {
			if (target) {
				transform.position = Vector3.Lerp (transform.position, target.position, speed) + new Vector3 (offset_x, offset_y, -10f);


			}

		}

		if (Input.GetKeyDown (KeyCode.Z)) {
			if (!zoomout) {
				store_zoom = myCamera.orthographicSize; 

				myCamera.orthographicSize = 500f;
				zoomout = true; 
			} else {
				myCamera.orthographicSize = store_zoom;
				zoomout = false; 

			}







		}



	}
}
