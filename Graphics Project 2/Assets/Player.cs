using System.Collections;
using System.Collections.Generic;

public class Player : Destroyable {
    public List<Item> items { get; private set; }
    public bool isInvincible = false;

    public void triggerItem() {

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
