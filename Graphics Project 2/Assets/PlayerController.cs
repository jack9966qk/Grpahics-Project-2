using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	List<Vector3> track;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKey ("w")) {
			this.transform.localPosition += Vector3.forward * Time.deltaTime;
		}else if(Input.GetKey ("s")){
			this.transform.localPosition -= Vector3.forward * Time.deltaTime;
		}


		if (Input.GetKey ("a")) {
			this.transform.Rotate (Vector3.forward);
		}
		if (Input.GetKey ("d")) {
			this.transform.Rotate (Vector3.back);
		}



	}
}
