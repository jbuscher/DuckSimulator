using UnityEngine;
using System.Collections;

public class MainCameraTitleMenu : MonoBehaviour {

    Vector3 pos1 = new Vector3(0f, 1f, -10f);
    Vector3 pos2 = new Vector3(7.5f, 1f, -5f);
    Vector3 pos3 = new Vector3(-7.5f, 5f, -5f);
    Vector3 duckPos;

    public Transform Duck;

    // Use this for initialization
    void Start () {
        duckPos = Duck.position;
		StartCoroutine(MoveCamera());
    }
	
	// Update is called once per frame
	void Update () {

	}

    IEnumerator MoveCamera()
    {
        this.transform.position = pos1;
        this.transform.LookAt(Duck);
        yield return new WaitForSeconds(5);

        this.transform.position = pos2;
        this.transform.LookAt(Duck);
        yield return new WaitForSeconds(5);

        this.transform.position = pos3;
        this.transform.LookAt(Duck);
        yield return new WaitForSeconds(5);

        StartCoroutine(MoveCamera());
    }
}
