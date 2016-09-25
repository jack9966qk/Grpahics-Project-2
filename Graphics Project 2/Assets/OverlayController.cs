using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OverlayController : MonoBehaviour {

    public GameObject scoreTextLabel;

    public void loadScore(int score) {
        scoreTextLabel.GetComponent<Text>().text = score.ToString();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
