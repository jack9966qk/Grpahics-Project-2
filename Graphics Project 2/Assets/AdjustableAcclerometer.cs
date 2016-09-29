using UnityEngine;
using System.Collections;

public static class AdjustableAcclerometer {
    public static Vector3 getAdjustedAccleration() {
        return Input.acceleration - GlobalState.instance.settings.acclerometerOffset;
    }

    public static void recalibrate() {
        GlobalState.instance.settings.acclerometerOffset = Input.acceleration;
    }
}
