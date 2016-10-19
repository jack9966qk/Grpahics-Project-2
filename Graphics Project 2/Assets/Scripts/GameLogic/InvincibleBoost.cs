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

		foreach (var obj in GameObject.FindGameObjectsWithTag("Afterburner")) {
			var ps = obj.GetComponent<ParticleSystem> ();
			ps.startSize = 0.15f;
			ps.startSpeed = 1.0f;
//			var col = ps.colorOverLifetime;
//			var key1 = col.color.gradient.colorKeys [1];
//			var key2 = col.color.gradient.colorKeys [2];
//			key1.color = new Color (0, 0, 200);
//			key2.color = new Color (0, 0, 255);
//			col.color.gradient.colorKeys [1] = key1;
//			col.color.gradient.colorKeys [2] = key2;
//			col.color.gradient.SetKeys( new GradientColorKey[] { new GradientColorKey(Color.blue, 0.0f), new GradientColorKey(Color.red, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) } );

		}

        foreach (var obj in GameObject.FindGameObjectsWithTag("Protection")) {
            obj.GetComponent<ParticleSystem>().Play();
        }
    }

	protected override void onEffectComplete() {
		base.onEffectComplete ();
		foreach (var obj in GameObject.FindGameObjectsWithTag("Afterburner")) {
			var ps = obj.GetComponent<ParticleSystem> ();
			ps.startSize = 0.06f;
			ps.startSpeed = 0.8f;
		}

        foreach (var obj in GameObject.FindGameObjectsWithTag("Protection")) {
            obj.GetComponent<ParticleSystem>().Stop();
        }
    }

    public override string getDescription() {
        return "An item that makes the player temporarily invinvible, and gives temporary speed-up";
    }

    public override string getName() {
        return "Invincible Boost";
    }
}
