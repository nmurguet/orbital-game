using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rocket : MonoBehaviour {

	public Rigidbody2D rb;
	public float torque;
	public float thrust; 
	public float strafe_thrust; 

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


	private ParticleSystem emitter; 


	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody2D> ();
		Debug.Log ("Inicio"); 
		pulled = false; 
		emitter = GetComponentInChildren<ParticleSystem> (); 


		
	}
	
	// Update is called once per frame
	void Update () {

		Movement (); 

	
		pullForce = (target.lossyScale.y / distance) * 5f; 

		if (pullForce > 5.5f) {
			pullForce = 5.5f;
		}
		//pullForce = target.lossyScale.y/2f;


		velocity = rb.velocity.magnitude; 

		if (Input.GetKey (KeyCode.R)) {

		
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);

		}

		
	}


	void FixedUpdate()
	{
		ManageGravity (); 
		//DrawTrajectory (this.transform.position, rb.velocity); 

	}

	void Movement()
	{

		//rb.AddRelativeForce (Vector2.up * thrust);
		rb.velocity = Vector2.ClampMagnitude (rb.velocity, 10f);

		if (thrust > 10f) {
			thrust = 10f;

		}
		if (thrust < -10f) {
			thrust = -10f;

		}

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

		}

		if (Input.GetKey (KeyCode.Q)) {
			//thrust += 0.01f;
			rb.AddRelativeForce (Vector2.right * -thrust);

		}
		if (Input.GetKey (KeyCode.E)) {
			//thrust -= 0.01f;
			rb.AddRelativeForce (Vector2.right * thrust);

		}


		if (Input.GetKeyUp (KeyCode.W)) {
			emitter.Stop (); 

		}






	}


	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "orbit") {
			target = other.transform;
			other.gameObject.GetComponentInParent<SpriteRenderer> ().color = new Color (Color.blue.r,Color.blue.g,Color.blue.b,1f);


		}

	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "orbit") {
			target = other.transform; 
			//other.gameObject.GetComponent<SpriteRenderer> ().color = new Color (Color.white.r,Color.white.g,Color.white.b,1f);
			other.gameObject.GetComponentInParent<SpriteRenderer> ().color = new Color (Color.white.r,Color.white.g,Color.white.b,1f);
		}
	}


	void ManageGravity(){
		atmosphere = target.lossyScale.y;
		dir = transform.position - target.transform.position;
		distance = Vector2.Distance (transform.position, target.position);

		if (distance < target.lossyScale.y + 20) {
			rb.AddForce (-dir.normalized * pullForce);
			pulled = true; 

		} else if (distance > target.lossyScale.y + 20) {


			pulled = false;
		}
		if (distance < target.lossyScale.y) {
			rb.drag = 0.2f; 
		} else if (distance > target.lossyScale.y) {
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
