using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet_Spawner : MonoBehaviour {

	public GameObject planet; 

	public Transform Home_planet; 

	public int count; 

	public float distance;

	public Vector2 pos;

	public List<Vector2> list_of_planets;

	private bool can_deploy;


	// Use this for initialization
	void Start () {

		can_deploy = false; 

		list_of_planets.Add (Home_planet.transform.position);



		for (int i = 0; i <= count; i++) {
			
			pos = new Vector2 (Random.Range (-500, 500), Random.Range (-500, 500));
			for (int x = 0; x <= list_of_planets.Count; x++) {
				if (x < list_of_planets.Count) {
					distance = Vector2.Distance (list_of_planets [x], pos);	
					if(distance < 60){
						break;
					}

				}
				if (x == list_of_planets.Count) {
					can_deploy = true;

				} else
					can_deploy = false; 
			}
			if (can_deploy == true) {
				list_of_planets.Add (pos);
				Instantiate (planet, pos, transform.rotation);
			}
		}

		
	}
	
	// Update is called once per frame
	void Update () {



		
	}
}
