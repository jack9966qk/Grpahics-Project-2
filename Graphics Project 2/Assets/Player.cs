using System;
using System.Collections;
using System.Collections.Generic;

public class Player : Destroyable {
    public Item item;
    public bool isInvincible = false;
    public List<Action<Item>> onItemTriggeredAction = new List<Action<Item>>();

    public void triggerItem() {
        item.applyEffectOnPlayer(this);
        foreach (Action<Item> a in onItemTriggeredAction) {
            a(this.item);
        }
        item = null;
    }

    override public void deductHp(int amount) {
        if (!isInvincible) {
            base.deductHp(amount);
        }
    }

    public Player(int maxHp) : base(maxHp) {
        return;
    }

}
