using UnityEngine;
using System.Collections;
using System;

public class PlayerObjectController : DestroyableController {
    public Player player { get; private set; }
    public PlayerOriginController playerOriginController;
	public GameObject[] cameras;

    public void addItemToPlayer(Item item) {
        player.item = item;
        item.controller = playerOriginController;
    }

    void Start() {
        player = new Player(100);
		player.destroyActions.Add (delegate {
			foreach(var camera in cameras) {
				camera.transform.SetParent(playerOriginController.transform);
			}
			this.gameObject.SetActive(false);
			playerOriginController.accleration = -0.1f;
		});
    }

    void handleTouch() {
        if (Input.touchCount > 0) {
            if (Input.GetTouch(0).phase == TouchPhase.Began) {
                player.triggerItem();
            }
        }
    }

    void Update() {
        handleTouch();

        if (Input.GetKeyDown(KeyCode.Space)) {
            player.triggerItem();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            addItemToPlayer(new HealthRecovery());
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            addItemToPlayer(new InvincibleBoost());
        }
    }
}
