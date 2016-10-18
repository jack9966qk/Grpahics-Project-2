using UnityEngine;

public class GameSettings  {
    public enum CameraSetting { FirstPerson, ThirdPerson }
    public float acclerometerSensitivity = 250f;
    public Vector3 acclerometerOffset = Vector3.zero;
    public CameraSetting cameraSetting = CameraSetting.ThirdPerson;
}
