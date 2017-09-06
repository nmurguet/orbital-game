using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	public Transform target; 

	public float speed; 

	private SpriteRenderer sr; 

	public string target_name; 

	public string collider_name; 

	// Use this for initialization
	void Start () {
		sr= GetComponent<SpriteRenderer> ();
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		target_name = target.gameObject.name;

		float step = speed * Time.deltaTime;

		transform.position = Vector3.MoveTowards (transform.position, target.position, step);


	//	if(transform.position.x > Screen.width

		Vector3	viewPos = Camera.main.WorldToViewportPoint (transform.position); 
		viewPos.x = Mathf.Clamp01 (viewPos.x); 
		viewPos.y = Mathf.Clamp01 (viewPos.y); 
		transform.position = Camera.main.ViewportToWorldPoint (viewPos); 



	}
	/*
	void Update()
	{
		if (transform.position == target.position) {
			sr.enabled = false; 


		} else
			sr.enabled = true; 
		
	}
*/
	void OnTriggerEnter2D(Collider2D other)
	{
		/*
		collider_name = other.gameObject.name; 
		if (other.gameObject.name == target.gameObject.name) {
			sr.enabled = false; 
			Debug.Log ("paso por aca");


		} 
*/

	}


	void OnTriggerExit2D(Collider2D other)
	{
		/*
		collider_name = other.gameObject.name;
		if (other.gameObject.name == target.gameObject.name) {
			sr.enabled = true; 

		}

*/

	}



}
