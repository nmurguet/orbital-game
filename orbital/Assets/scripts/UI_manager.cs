using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_manager : MonoBehaviour {

	public GameObject arrow;

	public GameObject ship; 

	public Text speed; 

	public Text altitude;

	public Text targetName; 

	public GameObject circle; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		arrow.transform.rotation = ship.transform.rotation; 

		speed.text = "Speed: " + Mathf.Round(ship.GetComponent<rocket> ().velocity*100f)/100f;

		//altitude.text = "Distance: " + Mathf.Round((Vector2.Distance (ship.transform.position, ship.GetComponent<rocket> ().target.transform.position) - (ship.GetComponent<rocket>().target.lossyScale.y/2)));
		altitude.text = "Distance: " + Mathf.Round((Vector2.Distance (ship.transform.position, circle.GetComponent<Arrow> ().target.transform.position) - (circle.GetComponent<Arrow>().target.lossyScale.y/2)));
		targetName.text = "Target Name: " + circle.GetComponent<Arrow> ().target_name; 
	}
}
