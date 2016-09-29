using UnityEngine;
using System.Collections;

public class PlayerObjectController : DestroyableController {
    public Player player { get; private set; }

    void Start() {
        player = new Player(100);
        setSelfToBeDestroyedWith(player);
    }
}
