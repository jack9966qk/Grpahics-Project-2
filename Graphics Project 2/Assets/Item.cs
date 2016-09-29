using System.Collections;

public abstract class Item {

    public abstract string getDescription();
    public abstract string getName();
    public abstract void applyEffectOnPlayer(Player p);
    public abstract void applyEffectOnPlayerController(PlayerOriginController c);

}
