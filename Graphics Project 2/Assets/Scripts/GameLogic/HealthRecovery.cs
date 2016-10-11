using UnityEngine;
using System.Collections;
using System;

public class HealthRecovery : Item {
    private const int RECOVERY_AMOUNT = 50;

    protected override void applyEffectOnPlayer(Player p) {
        p.deductHp(-RECOVERY_AMOUNT);
        markPlayerEffectComplete();
    }

    protected override void applyEffectOnPlayerController(PlayerOriginController c) {
        markControllerEffectComplete();
    }

    public override string getDescription() {
        return "An item that restores health when used";
    }

    public override string getName() {
        return "Health Recovery";
    }
}
