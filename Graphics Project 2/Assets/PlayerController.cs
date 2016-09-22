using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	public float velocity;
	public float rotateSpeed;

	List<Vector3> track;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKey ("w")) {
			this.transform.localPosition += Vector3.forward * Time.deltaTime * velocity;
		}else if(Input.GetKey ("s")){
			this.transform.localPosition -= Vector3.forward * Time.deltaTime * velocity;
		}


		if (Input.GetKey ("a")) {
			this.transform.Rotate (Vector3.forward*rotateSpeed*Time.deltaTime);
		}
		if (Input.GetKey ("d")) {
			this.transform.Rotate (Vector3.back*rotateSpeed*Time.deltaTime);
		}



	}
}
