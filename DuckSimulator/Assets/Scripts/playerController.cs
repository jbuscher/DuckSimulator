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

	void Start(){
		rb = GetComponent<Rigidbody>();
		jumpCount = 1;
		waterHeight = GameObject.Find ("WaterProDaytime").transform.position.y;
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

			if(onWater)
				CorrectOrientation();
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
}
