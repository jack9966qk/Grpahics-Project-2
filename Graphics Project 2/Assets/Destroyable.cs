using System;
using System.Collections;
using System.Collections.Generic;


public abstract class Destroyable {
    public int hp { get; private set; }
    public int maxHp { get; private set; }
    public List<Action> destroyActions = new List<Action>();
    public List<Action> damageActions = new List<Action>();
    public List<Action> recoveryActions = new List<Action>();

    public void deductHp(int amount) {
        if (amount >= 0) {
            this.hp = Math.Max(this.hp - amount, 0);
            doActions(damageActions);
            if (this.hp <= 0) {
                doActions(destroyActions);
            }
        } else {
            this.hp = Math.Min(this.maxHp, this.hp - amount);
            doActions(recoveryActions);
        }
    }

    public Destroyable(int maxHp) {
        this.maxHp = maxHp;
        this.hp = maxHp;
    }


    private void doActions(List<Action> actions) {
        foreach (Action a in actions) {
            a();
        }
    }
	
}

