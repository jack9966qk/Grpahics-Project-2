using System.Collections;

public abstract class Obstacle : Destroyable {

    public abstract void onCollisionWithPlayer(Player p);

    protected Obstacle(int maxHp) : base(maxHp) {
        return;
    }

}
