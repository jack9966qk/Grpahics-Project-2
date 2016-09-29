using UnityEngine;
using System.Collections;
using System;

public class InvincibleBoost : Item {
    public override void applyEffectOnPlayer(Player p) {
        p.isInvincible = true;
        // TODO disable after a certain time
    }

    public override void applyEffectOnPlayerController(PlayerOriginController c) {
        c.extraVelocity = 10f;
        // TODO disable after a certain time
    }

    public override string getDescription() {
        return "An item that makes the player temporarily invinvible, and gives temporary speed-up";
    }

    public override string getName() {
        return "Invincible Boost";
    }
}
