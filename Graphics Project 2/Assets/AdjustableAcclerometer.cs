using UnityEngine;
using System.Collections;

public class AdjustableAcclerometer {
    private Vector3 offset = Vector3.zero;
    public Vector3 adjustedAcceleration {
        get {
            return Input.acceleration - offset;
        }
    }

    public void recalibrate() {
       offset = Input.acceleration;
    }
}
