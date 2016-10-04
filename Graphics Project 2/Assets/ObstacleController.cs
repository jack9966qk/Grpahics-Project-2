using UnityEngine;
using System.Collections;

public class ObstacleController : DestroyableController {

    Obstacle obstacle = new NormalObstacle(30);

    void OnCollisionEnter(Collision c) {
        var controller = c.gameObject.GetComponent<PlayerObjectController>();
        if (controller != null) {
            controller.player.deductHp(obstacle.hp);
            makeExplostion();
            obstacle.deductHp(obstacle.hp);
        }
    }

    void makeExplostion() {
        // TODO
    }

	// Use this for initialization
	void Start () {
        setSelfToBeDestroyedWith(obstacle);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
