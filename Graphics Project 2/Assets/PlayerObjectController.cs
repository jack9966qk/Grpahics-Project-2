using UnityEngine;
using System.Collections;
using System;

public class PlayerObjectController : DestroyableController {
    public Player player { get; private set; }
    public PlayerOriginController playerOriginController;

    void addItemToPlayer(Item item) {
        player.item = item;
        item.controller = playerOriginController;
    }

    void Start() {
        player = new Player(100);
        setSelfToBeDestroyedWith(player);
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
