using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

	public float moveSpeed;
	public float rotateSpeed;
	public float jumpForce;
	public float correctionSpeed;
	private Rigidbody rb;
	private int jumpCount;
	private bool onWater;
	private float waterHeight;
	private float[] tubWallLocations = new float[4];
	public GameObject explosion;

	void Start(){
		rb = GetComponent<Rigidbody>();
		jumpCount = 1;
		waterHeight = GameObject.Find ("WaterProDaytime").transform.position.y;

		tubWallLocations[0] = GameObject.Find ("LongWall").transform.position.x;
		tubWallLocations[1] = GameObject.Find ("LongWall (1)").transform.position.x;
		tubWallLocations[2] = GameObject.Find ("ShortWall").transform.position.z;
		tubWallLocations[3] = GameObject.Find ("ShortWall (1)").transform.position.z;
	}

	void FixedUpdate(){
		if (rb) {
			float moveRotate = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical") * -1;

			float rotation = gameObject.transform.rotation.y * Mathf.PI;

			Vector3 pos = gameObject.transform.position;
			onWater = Mathf.Abs(pos.y - waterHeight) < 1; //Is the duck within 1 unit of the water height

			if (Input.GetKeyDown(KeyCode.Space) && onWater) {
				Jump();
			}

			float moveX = 0, moveZ = 0;
			if(moveVertical != 0) {
				moveX = moveVertical * Mathf.Sin (rotation);
				moveZ = moveVertical * Mathf.Cos (rotation);
				Vector3 movement = new Vector3 (moveX, 0.0f, moveZ);
				rb.AddForce (movement * moveSpeed);
			}

			if (moveRotate != 0) {
				rb.transform.Rotate(moveRotate * Vector3.up * rotateSpeed * Time.deltaTime);
			}

			if (onWater)
				CorrectOrientation();

			if (!InTheTub(gameObject.transform.position) && gameObject.transform.position.y < 8) {
				StartCoroutine(BlowUp());
			}
		}
	}

	void Jump() {
		rb.AddForce (Vector3.up * jumpForce * (jumpCount), ForceMode.Impulse);
		jumpCount++;
	}

	void CorrectOrientation() {
		float rotateX = rb.transform.rotation.x;
		float rotateZ = rb.transform.rotation.z;
		bool shouldRotateX = rotateX > .01 || rotateX < -.01;
		bool shouldRotateZ = rotateZ > .01 || rotateZ < -.01;

		if (rotateX > 0 && shouldRotateX)
			rb.transform.Rotate (Vector3.left * Mathf.Abs(rotateX) * correctionSpeed);
		else if (rotateX < 0 && shouldRotateX)
			rb.transform.Rotate (Vector3.right * Mathf.Abs(rotateX) * correctionSpeed);

		if (rotateZ > 0 && shouldRotateZ)
			rb.transform.Rotate (Vector3.back * Mathf.Abs(rotateZ) * correctionSpeed);
		else if (rotateZ < 0 && shouldRotateZ)
			rb.transform.Rotate (Vector3.forward * Mathf.Abs(rotateZ) * correctionSpeed);
	}

	/*
	 * You can find me in da tub
		with my mister Bob
		listen to this pretty sound
		it comes from my rubber duck 
		I'm in to sitting down
		I ain't used to standing up
		so come, give me a hug
		if you're in to getting scrubbed
	*/
	bool InTheTub(Vector3 position) {
		return position.x < tubWallLocations[0] && 
				position.x > tubWallLocations[1] &&
				position.z > tubWallLocations[2] &&
				position.z < tubWallLocations[3];
	}

	IEnumerator BlowUp() {
		Instantiate(explosion, transform.position, transform.rotation);
		transform.localScale = Vector3.zero;
		rb.velocity = Vector3.zero;
		yield return new WaitForSeconds(3.0f);
		Application.LoadLevel ("Plane");
	}

}
