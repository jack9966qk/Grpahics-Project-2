using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerController : MonoBehaviour {

    public Player player { get; private set; }
	private float velocity = 2f;
    private float accleration = 0.5f;

	List<Vector3> track;
    public float trackRadius = 5f;

	// Use this for initialization
	void Start () {
        player = new Player(100);
        player.destroyActions.Add(delegate {
            GameObject.Destroy(this.gameObject);
        });
	}
	
	// Update is called once per frame
	void Update () {
        applyAccleration();
        moveForwardByDist(velocity * Time.fixedDeltaTime);
	}

    void applyAccleration() {
        this.velocity = Math.Max(0f, this.velocity += this.accleration);
    }

    // make the player object move forward by t
    void moveForwardByDist(float t) {
        throw new System.NotImplementedException();
    }
}
