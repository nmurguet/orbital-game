using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet_Spawner : MonoBehaviour {

	public GameObject planet; 

	public Transform Home_planet; 

	public int count; 

	public float distance;

	public Vector2 pos;

	public List<Vector2> planetPositions; 
	public List<GameObject> planets;


	public Arrow arrow;

	// Use this for initialization
	void Start () {

		CreatePlanets (count); 

	}

	private void CreatePlanets(int targetCount, int minDistance=60) {
		int createdCount = 0;
		do {
			Vector2 randomPos = new Vector2 (Random.Range (-500, 500), Random.Range (-500, 500));

			if (randomPos.magnitude > minDistance &&
				!planetPositions.Exists (
					planetPosition => (Vector2.Distance (planetPosition, randomPos) < minDistance)
				)
			) {
				planet = Instantiate(planet, randomPos, transform.rotation);
				planet.name = "Planet "+createdCount;
				planets.Add (planet);
				planetPositions.Add (randomPos);
				createdCount++;

			}
		} while(createdCount < targetCount);	
	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetKeyDown (KeyCode.T)) {
			//arrow.SetTarget (.position);
			arrow.SetTarget(planets [Random.Range (0, planets.Count)].transform);
		}



		
	}
}
