using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultPageController : MonoBehaviour {

    public GameObject scoreTextLabel;

    public void goToMainMenu() {
        StateController.goToMainMenu();
    }

    public void loadScore(int score) {
        scoreTextLabel.GetComponent<Text>().text = "Score: " + score.ToString();
    }

}
