using UnityEngine;
using System.Collections;

public class ItemBoxController : MonoBehaviour {

    void OnCollisionEnter(Collision c) {
        var controller = c.gameObject.GetComponent<PlayerObjectController>();
        if (controller != null) {
            controller.addItemToPlayer(ItemPicker.PickOneRandom());
            Destroy(this);
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
