using System;
using System.Collections;

public class NormalObstacle : Obstacle {
//    private const int COLLISION_DAMAGE = 20;

    public override void onCollisionWithPlayer(Player p) {
		p.deductHp(this.hp);
		this.deductHp (this.hp);
    }



    public NormalObstacle(int maxHp) : base(maxHp) {
        return;
    }
}
