using UnityEngine;
using System.Collections;

public class MainMenuAudio : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (GameObject.FindGameObjectsWithTag("MainMenuMusic").Length > 1) {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
	}
}
