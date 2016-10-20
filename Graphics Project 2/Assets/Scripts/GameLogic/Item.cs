using System.Collections;

public abstract class Item {

    public Player player;
    public PlayerOriginController controller;
	public bool InEffect = false;

    private bool isPlayerEffectComplete = false;
    private bool isControllerEffectComplete = false;

    public abstract string getDescription();
    public abstract string getName();
    public void applyEffect() {
        if (player != null) {
            applyEffectOnPlayer(player);
        }

        if (controller != null) {
            applyEffectOnPlayerController(controller);
        }

		InEffect = true;
    }
    protected abstract void applyEffectOnPlayer(Player p);
    protected abstract void applyEffectOnPlayerController(PlayerOriginController c);

    protected void markPlayerEffectComplete() {
        isPlayerEffectComplete = true;
        checkEffectComplete();
    }

    protected void markControllerEffectComplete() {
        isControllerEffectComplete = true;
        checkEffectComplete();
    }


    private void checkEffectComplete() {
        if (isPlayerEffectComplete && isControllerEffectComplete) {
            onEffectComplete();
        }
    }

    protected virtual void onEffectComplete() {
        player.item = null;
		InEffect = false;
    }

}
