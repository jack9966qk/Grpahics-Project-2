using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameplayController : MonoBehaviour {

	public GameObject player;
	public GameObject tunnelBlockPrefab;

	private GameObject currentBlock;
	private GameObject nextBlock;
	private GameObject nextNextBlock;

	private int z = 0;

//	private GameObject generateNextBlockPoints() {
//	}
//

	GameObject generateNewTunnelBlock(List<Vector3> track) {
		var newBlock = GameObject.Instantiate (tunnelBlockPrefab);
		newBlock.GetComponent<TunnelBlock> ().GenerateTunnel (track);
		return newBlock;
	}

	private void gotoNextBlock() {
		// generate nextnextBlock
		List<Vector3> track = TrackFactory.instance.getBlock();

		nextNextBlock = generateNewTunnelBlock(track);

		// destory current block
		GameObject.Destroy(currentBlock);
//		currentBlock.GetComponent<TunnelBlock>().destroy();

		currentBlock = nextBlock;
		nextBlock = nextNextBlock;

		// reset positions
		Vector3 playerPos = player.transform.position;
		player.transform.position = Vector3.zero;
		Vector3 playerMovement = Vector3.zero - playerPos;
		currentBlock.transform.Translate (playerMovement);

	}


	// Use this for initialization
	void Start () {
		
		currentBlock = generateNewTunnelBlock(TrackFactory.instance.getSin());
		nextBlock = generateNewTunnelBlock(TrackFactory.instance.getSin());
		nextNextBlock = generateNewTunnelBlock(TrackFactory.instance.getSin());
//		nextBlock = generateNextBlockPoints ();
//		nextNextBlock = generateNextBlockPoints ();
//
//		GenerateTunnel (points);
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.R)) {
			gotoNextBlock ();
		}
		

	}



}
