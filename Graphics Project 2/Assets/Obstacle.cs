using System.Collections;

public abstract class Obstacle : Destroyable {

    public abstract void onCollisionWithPlayer(Player p);

    public Obstacle(int maxHp) : base(maxHp) {
        return;
    }

}
