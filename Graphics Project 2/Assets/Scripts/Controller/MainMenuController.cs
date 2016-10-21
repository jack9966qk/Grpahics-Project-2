using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	void Start(){
		Screen.orientation = ScreenOrientation.Landscape;
	}

    public void goToGamePlay() {
        StateController.goToGameplay();
    }

    public void goToSettings() {
        StateController.goToSettings();
    }

    public void goToInstructions()
    {
        StateController.goToInstructions();
    }

}
