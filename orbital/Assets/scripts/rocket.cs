using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rocket : MonoBehaviour {

	public Rigidbody2D rb;
	public float torque;
	public float thrust; 
	public float strafe_thrust; 

	public float max_speed; 

	public bool pulled; 

	private Vector2 dir; 

	public Transform target; 

	private float distance; 

	public float pullForce; 

	public float atmosphere; 


	public float velocity;


	public Transform spawnPoint; 

	private float xSpeed; 

	private float ySpeed; 


	//
	private ParticleSystem emitter; 
	private ParticleSystem right_emitter; 
	private ParticleSystem left_emitter; 
	private ParticleSystem front_emitter; 


	public float mass; 


	public float scale; 


	public float check_distance; 

	public Transform tar;
	public Transform tar2;


	public List<GameObject> overlapping; 



	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody2D> ();
		Debug.Log ("Inicio"); 
		pulled = false; 
		GameObject frontEmitter = GameObject.Find ("main_rocket");
		GameObject left_rocket = GameObject.Find ("left_rocket");
		GameObject right_rocket = GameObject.Find ("right_rocket");
		GameObject front_rocket = GameObject.Find ("front_rocket");
		emitter = frontEmitter.GetComponent<ParticleSystem> (); 
		right_emitter = right_rocket.GetComponent<ParticleSystem> ();
		left_emitter = left_rocket.GetComponent<ParticleSystem> ();
		front_emitter = front_rocket.GetComponent<ParticleSystem> ();

		//emitter = GetComponentInChildren<ParticleSystem> (); 





		
	}
	
	// Update is called once per frame
	void Update () {
		//ManageTargets ();
		check_distance = Vector2.Distance(tar2.transform.position, tar.transform.position);

		mass = target.GetComponent<Planet> ().mass; 
		pullForce = (mass / distance) * 6f; 
		//pullForce = (target.lossyScale.y / distance) * 5f; 

		if (pullForce > 5.5f) {
			pullForce = 5.5f;
		}



		velocity = rb.velocity.magnitude; 


		Movement (); 



		if (Input.GetKey (KeyCode.R)) {

		
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);

		}

		
	}


	void FixedUpdate()
	{
		

		ManageGravity (); 
		//DrawTrajectory (this.transform.position, rb.velocity); por?

	}

	void Movement()
	{


		rb.velocity = Vector2.ClampMagnitude (rb.velocity, max_speed);


		if (Input.GetKey (KeyCode.A)) {
			rb.AddTorque (0.3f * torque);


		}
		if (Input.GetKey (KeyCode.D)) {
			rb.AddTorque (-0.3f * torque);
		}

		if (Input.GetKey (KeyCode.W)) {
			//thrust += 0.01f;
			rb.AddRelativeForce (Vector2.up * thrust);
			emitter.Play (); 

		}
		if (Input.GetKey (KeyCode.S)) {
			//thrust -= 0.01f;
			rb.AddRelativeForce (Vector2.up * -thrust);
			front_emitter.Play ();

		}

		if (Input.GetKeyDown (KeyCode.Q)) {
			//thrust += 0.01f;
			rb.AddRelativeForce (Vector2.right * -strafe_thrust);
			right_emitter.Emit (200);

		}
		if (Input.GetKeyDown (KeyCode.E)) {
			//thrust -= 0.01f;
			rb.AddRelativeForce (Vector2.right * strafe_thrust);
			left_emitter.Emit (200);

		}


		if (Input.GetKeyUp (KeyCode.W)) {
			emitter.Stop (); 

		}
		if (Input.GetKeyUp (KeyCode.S)) {
			front_emitter.Stop (); 

		}






	}


	void OnTriggerStay2D(Collider2D other)
	{



		// Posible fix a esto. Agregar a una lista los objetos que entran al trigger, y de ahi chequear la distancia,
		// el mas cercano es el que tendria que estar activo. Ahora si hay overlap hace cualquier cosa. 

		if (other.gameObject.tag == "orbit") {

			target = other.transform.parent.transform;
			scale = target.lossyScale.y; 
			other.gameObject.GetComponentInParent<SpriteRenderer> ().color = new Color (Color.blue.r,Color.blue.g,Color.blue.b,1f);
			}



		}


	void ManageTargets()
	{
		float dist = 0;

		for (int i = 0; i < overlapping.Count; i++) {
			float check = Vector2.Distance (gameObject.transform.position, overlapping [i].transform.position);
			if (check > dist) {
				target = overlapping [i].transform;

			}
		}


	}

	void OnTriggerEnter2D(Collider2D other)
	{
		/*
		if (other.gameObject.tag == "orbit") {
			overlapping.Add (other.transform.parent.gameObject);

		}*/




	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "orbit") {
			
			target = other.transform.parent.transform;

			other.gameObject.GetComponentInParent<SpriteRenderer> ().color = new Color (Color.white.r,Color.white.g,Color.white.b,1f);
			overlapping.Remove (other.transform.parent.gameObject);
		}
	}


	void ManageGravity(){
		atmosphere = mass;
		dir = transform.position - target.transform.position;
		distance = Vector2.Distance (transform.position, target.position);

		if (distance < mass + 100) {
			rb.AddForce (-dir.normalized * pullForce);
			pulled = true; 

		} else if (distance > mass + 100) {


			pulled = false;
		}
		if (distance < mass) {
			rb.drag = 0.2f; 
		} else if (distance > mass) {
			rb.drag = 0f; 
		}


	}



	void DrawTrajectory(Vector2 startPos, Vector2 startVelocity)
	{
		int verts = 40; 
		var line = this.gameObject.GetComponent<LineRenderer> (); 

		line.positionCount = (verts); 

		Vector2 pos = startPos; 
		Vector2 vel = startVelocity; 
		Vector2 grav = new Vector2 ((transform.position.x - target.transform.position.x) * -pullForce, (transform.position.y - target.transform.position.y)*-pullForce);

		for (int i = 0; i < verts; i++) {
			line.SetPosition (i, new Vector3 (startPos.x, startPos.y, 0));
			vel = vel + grav * Time.fixedDeltaTime;
			pos = pos + vel * Time.fixedDeltaTime;

		}


	}

}
