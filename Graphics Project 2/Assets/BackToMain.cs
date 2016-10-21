using UnityEngine;
using System.Collections;

public class BackToMain : MonoBehaviour {

    public void Back()
    {
        StateController.goToMainMenu();
    }
}
