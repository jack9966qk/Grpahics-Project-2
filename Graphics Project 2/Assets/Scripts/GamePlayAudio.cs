using UnityEngine;
using System.Collections;

public class GamePlayAudio : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    foreach (var obj in GameObject.FindGameObjectsWithTag("MainMenuMusic")) {
            Destroy(obj);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
