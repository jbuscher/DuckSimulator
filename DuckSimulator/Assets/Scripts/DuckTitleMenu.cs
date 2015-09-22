using UnityEngine;
using System.Collections;

public class DuckTitleMenu : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    private Rigidbody rb;

    Vector3 resetLoc;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        resetLoc = this.transform.position;
        StartCoroutine(ResetDuck());
    }

    void FixedUpdate()
    {
        float moveHorizontal = 0f;
        float moveVertical = -1f;

        Vector3 pos = gameObject.transform.position;

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        if (rb)
            rb.AddForce(movement * speed);
    }

    IEnumerator ResetDuck()
    {
        this.transform.position = resetLoc;
        yield return new WaitForSeconds(5);
        StartCoroutine(ResetDuck());
    }
}
