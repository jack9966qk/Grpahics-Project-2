using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeObstacleController : MonoBehaviour
{
	private NormalObstacle obstacleModel;
    public GameObject Explosion;

	void Start() {
		obstacleModel = new NormalObstacle (20);
		obstacleModel.destroyActions.Add( delegate {
			this.gameObject.SetActive(false);
			GlobalState.instance.destroyedObjects.Enqueue(this.gameObject);

            if (Explosion != null) {
                var exp = GameObject.Instantiate(Explosion);
                exp.transform.position = this.transform.position;
            }
		});
	}

	public void PutCube(Vector3 origin, Vector3 back, float degree, float radius) {

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
			obstacleModel.onCollisionWithPlayer (obj.GetComponent<PlayerObjectController> ().player);
		}
	}

}
