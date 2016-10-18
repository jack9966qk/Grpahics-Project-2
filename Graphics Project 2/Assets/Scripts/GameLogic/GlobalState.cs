using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalState {

	private static GlobalState _instance = null;

	private List<Vector3> current;

	public static GlobalState instance {
		get {
			if (_instance == null) {
				_instance = new GlobalState ();
			}
			return GlobalState._instance;
		}
	}

    public MonoBehaviour gameController;
    public GameSettings settings { get; private set; }

    protected GlobalState() {
        settings = new GameSettings();
    }

	public Queue<GameObject> destroyedObjects = new Queue<GameObject>();


}
