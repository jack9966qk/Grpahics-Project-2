using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject firstPersonCamera;
    public GameObject thirdPersonCamera;

    bool wait = true;

    public void applySetting() {
        if (firstPersonCamera == null || thirdPersonCamera == null) {
            wait = true;
            return;
        }

        var camSetting = GlobalState.instance.settings.cameraSetting;
        if (camSetting == GameSettings.CameraSetting.FirstPerson) {
            setAsFirstPerson();
        } else if (camSetting == GameSettings.CameraSetting.ThirdPerson) {
            setAsThirdPerson();
        }
        wait = false;
    }

    public void setAsFirstPerson() {
        firstPersonCamera.SetActive(true);
        thirdPersonCamera.SetActive(false);
    }

    public void setAsThirdPerson() {
        firstPersonCamera.SetActive(false);
        thirdPersonCamera.SetActive(true);
    }

    void Start() {
        applySetting();
    }

    void Update() {
        if (wait == true) {
            applySetting();
        }
    }
}
