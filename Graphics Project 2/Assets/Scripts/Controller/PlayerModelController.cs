using UnityEngine;
using System.Collections;

public class PlayerModelController : MonoBehaviour {

    public float MaxTiltAngle = 30f;
    public float RecoverySpeed = 0.1f;
    public float TiltSpeed = 1f;

	public void Tilt(float moveDist) {
        Vector3 angles = this.transform.localEulerAngles;
        angles.z -= moveDist * TiltSpeed;
        this.transform.localEulerAngles = angles;
    }
	
	// Update is called once per frame
	void Update () {
        float tilt = this.transform.localEulerAngles.z;
        if (tilt == 0) {
            return;
        }
        if (tilt > 0 && tilt < 180) {
            tilt = Mathf.Max(0, tilt - RecoverySpeed);
        } else if (tilt > 180 && tilt < 360) {
            tilt += RecoverySpeed;
            if (tilt > 360) {
                tilt = 0;
            }
        }
        Vector3 angles = this.transform.localEulerAngles;
        angles.z = tilt;
        this.transform.localEulerAngles = angles;
    }
}
