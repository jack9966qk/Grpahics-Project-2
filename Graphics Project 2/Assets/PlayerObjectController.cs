using UnityEngine;
using System.Collections;
using System;

public class PlayerObjectController : DestroyableController {
    public Player player { get; private set; }
    public PlayerOriginController playerOriginController;

    void Start() {
        player = new Player(100);
        setSelfToBeDestroyedWith(player);
        player.onItemTriggeredAction.Add(item =>
            item.applyEffectOnPlayerController(playerOriginController)
        );
    }
}
