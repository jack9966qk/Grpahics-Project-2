using System.Collections;
using System.Collections.Generic;

public class Player : Destroyable {
    public List<Item> items { get; private set; }

    public void triggerItem() {

    }

    public Player(int maxHp) : base(maxHp) {
        return;
    }

}
