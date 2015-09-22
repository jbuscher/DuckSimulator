using UnityEngine;
using System.Collections;

public class FloatOnWater : MonoBehaviour {
	
	float waterHeight;
	private Rigidbody rb;
	public float waterForce;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		waterHeight = GameObject.Find("WaterProDaytime").transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		float duckY = gameObject.transform.position.y - .5f;
		if (duckY < waterHeight)
			rb.AddForce (Vector3.up * waterForce * (waterHeight - duckY));
	}
}
