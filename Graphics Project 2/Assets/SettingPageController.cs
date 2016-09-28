using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingPageController : MonoBehaviour {

    public Text accelerometerSensitivityLabel;

    void reclibrateAcclerometer() {
        throw new System.NotImplementedException();
    }

    void changeAcclerometerSensitivity(float amount) {
        GlobalState.instance.settings.acclerometerSensitivity += amount;
    }

    public void backToMainMenu() {
        StateController.goToMainMenu();
    }
    
    public void addSensitivity() {
        changeAcclerometerSensitivity(5f);
    }

    public void reduceSensitivity() {
        changeAcclerometerSensitivity(-5f);
    }

    public void recalibrate() {
        GlobalState.instance.acclerometer.recalibrate();
    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        accelerometerSensitivityLabel.text =
            "Accelerometer Sensitivity: " + GlobalState.instance.settings.acclerometerSensitivity.ToString();
    }
}
