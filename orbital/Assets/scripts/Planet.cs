using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

	private CircleCollider2D radius; 

	public float influence; 


	public float mass; 



	// Use this for initialization
	void Start () {
		Transform child = transform.GetChild (0);
		radius = child.GetComponent<CircleCollider2D> (); 

		radius.radius = influence; 
	}
	
	// Update is called once per frame
	void Update () {
		
	}



}
