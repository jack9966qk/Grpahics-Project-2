using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

    public void goToGamePlay() {
        StateController.goToGameplay();
    }

    public void goToSettings() {
        StateController.goToSettings();
    }
    
}
