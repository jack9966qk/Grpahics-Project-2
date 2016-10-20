using System;
using System.Collections;
using System.Collections.Generic;

public class Player : Destroyable {
    private Item _item = null;
    public Item item {
        get {
            return _item;
        } set {
            _item = value;
            if (value != null) {
                _item.player = this;
            }
        }
    }

    public bool isInvincible = false;

    public void triggerItem() {
		if (item != null && item.InEffect == false) {
            item.applyEffect();
        }
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
