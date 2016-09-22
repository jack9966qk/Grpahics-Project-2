using UnityEngine;
using System.Collections;

public class SettingPageController : MonoBehaviour {

    void reclibrateAcclerometer() {
        throw new System.NotImplementedException();
    }

    void changeAcclerometerSensitivity(int amount) {
        GlobalState.instance.settings.acclerometerSensitivity += amount;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
