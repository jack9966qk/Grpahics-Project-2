using UnityEngine;
using System.Collections;

public class StateController {
    public static void goToGameplay() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GamePlay");
    }

    public static void goToSettings() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Settings");
    }

    public static void goToMainMenu() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
    public static void goToInstructions()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Instruction");
    }
}
