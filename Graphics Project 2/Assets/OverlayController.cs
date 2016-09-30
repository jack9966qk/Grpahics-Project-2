using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OverlayController : MonoBehaviour {

    public GameObject scoreTextLabel;
    public GameObject itemLabel;
    public GameObject hpLabel;

    public void loadScore(int score) {
        scoreTextLabel.GetComponent<Text>().text = score.ToString();
    }

    public void loadItem(Item item) {
        if (item == null) {
            itemLabel.GetComponent<Text>().text = "No Item";
        } else {
            itemLabel.GetComponent<Text>().text = item.getName();
        }
    }

    public void loadHP(float hp, float maxHp) {
        hpLabel.GetComponent<Text>().text = (100f * hp / maxHp).ToString("0.0") + "%";
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
