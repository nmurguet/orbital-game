using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Planet : MonoBehaviour {

	private CircleCollider2D radius; 

	public float influence; 


	public float mass; 
	


	public List<string>	nombres;

	public string Aleatorio; 

	// Use this for initialization
	void Start () {


		Transform child = transform.GetChild (0);
		radius = child.GetComponent<CircleCollider2D> (); 



		radius.radius = influence; 
		AddNames ();

		Aleatorio = nombres [Random.Range (0, nombres.Count)];

	
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void AddNames(){
		
		nombres.Add ("Tierra");
		nombres.Add ("Marte");
		nombres.Add ("Neptuno");
		nombres.Add("Jupiter");
		nombres.Add("Saturno");


	}
}
