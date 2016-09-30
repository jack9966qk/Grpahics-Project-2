using UnityEngine;
using System.Collections;
using System;

public class InvincibleBoost : Item {
    const float EFFECT_LENGTH = 5f;
    const float BOOST_VELOCITY = 10f;

    IEnumerator doAfterSeconds(float secs, Action action) {
        yield return new WaitForSeconds(secs);
        action();
    }

    protected override void applyEffectOnPlayer(Player p) {
        p.isInvincible = true;
        GlobalState.instance.gameController.StartCoroutine(
            doAfterSeconds(EFFECT_LENGTH, delegate {
                p.isInvincible = false;
                markPlayerEffectComplete();
            })
        );
    }

    protected override void applyEffectOnPlayerController(PlayerOriginController c) {
        c.extraVelocity += BOOST_VELOCITY;
        GlobalState.instance.gameController.StartCoroutine(
            doAfterSeconds(EFFECT_LENGTH, delegate {
                c.extraVelocity -= BOOST_VELOCITY;
                markControllerEffectComplete();
            })
        );
        doAfterSeconds(EFFECT_LENGTH, delegate {
            c.extraVelocity -= BOOST_VELOCITY;
            markControllerEffectComplete();
        });
    }

    public override string getDescription() {
        return "An item that makes the player temporarily invinvible, and gives temporary speed-up";
    }

    public override string getName() {
        return "Invincible Boost";
    }
}
