using UnityEngine;
using System.Collections;

public class GoAround : MonoBehaviour {

    public float velocity = 0.5f;
    
    // Use this for initialization
    void Start () {
	
	}

    void Update()
    {
        if (Input.GetKey(KeyCode.A)) {
            transform.localPosition = transform.localPosition + Vector3.left * Time.deltaTime * velocity;
        }

        if (Input.GetKey(KeyCode.D)) {
            transform.localPosition = transform.localPosition + Vector3.right * Time.deltaTime * velocity;
        }
    }
}
