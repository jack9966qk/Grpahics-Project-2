using UnityEngine;
using System.Collections;

public class ItemBoxController : MonoBehaviour {


	//private Item item;


	public void PutItem(Vector3 origin, Vector3 back, float degree, float radius) {


		float radian = (degree+30) * Mathf.PI / 180;
		this.transform.eulerAngles =  new Vector3(0,0,180-15-degree);
		this.transform.localPosition = new Vector3 (origin.x + radius * Mathf.Sin (radian), origin.y + radius * Mathf.Cos (radian),
			origin.z);

		this.transform.localScale = new Vector3 (2*Mathf.Tan(Mathf.PI/12),2*Mathf.Tan(Mathf.PI/12),2*Mathf.Tan(Mathf.PI/12));


	}

	public void OnTriggerEnter(Collider other) {
		Debug.Log ("Collide");
		var obj = other.gameObject;
		if (obj.tag == "Player") {
			obj.GetComponent<PlayerObjectController> ().addItemToPlayer(ItemPicker.PickOneRandom ());
		}
			
		this.gameObject.SetActive (false);
		GlobalState.instance.destroyedObjects.Enqueue (this.gameObject);
	}


}
