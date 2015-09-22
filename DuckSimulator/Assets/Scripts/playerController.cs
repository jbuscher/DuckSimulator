using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

	public float speed;
	public float jumpForce;
	private Rigidbody rb;

	void Start(){
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate(){
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 pos = gameObject.transform.position;

		if (Input.GetKeyDown(KeyCode.Space) && pos.y < 6.5) {
			Jump();
		}

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		if(rb)
			rb.AddForce (movement * speed);
	}

	void Jump() {
		if(rb)
			rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
	}
}
