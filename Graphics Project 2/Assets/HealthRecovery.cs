using UnityEngine;
using System.Collections;
using System;

public class HealthRecovery : Item {
    private const int RECOVERY_AMOUNT = 50;

    public override void applyEffectOnPlayer(Player p) {
        p.deductHp(-RECOVERY_AMOUNT);
    }

    public override void applyEffectOnPlayerController(PlayerOriginController c) {
        return;
    }

    public override string getDescription() {
        return "An item that restores health when used";
    }

    public override string getName() {
        return "Health Recovery";
    }
}
