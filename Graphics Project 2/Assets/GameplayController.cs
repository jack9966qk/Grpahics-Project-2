using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class GameplayController : MonoBehaviour {


    public GameObject resultPage;
    public GameObject overlay;

    public GameObject player;
    public GameObject tunnelBlockPrefab;

    private GameObject lastBlock;
    private GameObject currentBlock;
    private GameObject nextBlock;
    private GameObject nextNextBlock;

    private int score {
        get {
            return Mathf.RoundToInt(player.GetComponent<PlayerController>().dist);
        }
    }

    GameObject generateNewTunnelBlock(List<Vector3> track) {
        var newBlock = GameObject.Instantiate(tunnelBlockPrefab);
        newBlock.GetComponent<TunnelBlock>().GenerateTunnel(track);
        return newBlock;
    }

	GameObject instantGenerateNewTunnelBlock(List<Vector3> track) {
		var newBlock = GameObject.Instantiate(tunnelBlockPrefab);
		newBlock.GetComponent<TunnelBlock>().InstantGenerateTunnel(track);
		return newBlock;
	}

	public void generateMore(){
		nextBlock.GetComponent<TunnelBlock> ().GenerateOneCircle ();
	}

	public void deleteMore(){
		lastBlock.GetComponent<TunnelBlock> ().DeleteOneCirle ();
	}

    public List<Vector3> gotoNextBlock() {
        // destroy lastBlock
        if (lastBlock != null) {
            GameObject.Destroy(lastBlock);
        }

		generateMore ();
		deleteMore ();

        // pass over
        lastBlock = currentBlock;
        currentBlock = nextBlock;
        nextBlock = nextNextBlock;

        // generate nextnextBlock
        List<Vector3> newTrack = TrackFactory.instance.getBlock();
        nextNextBlock = generateNewTunnelBlock(newTrack);

        return currentBlock.GetComponent<TunnelBlock>().track;

    }

    void displayResultPage() {
        resultPage.GetComponent<ResultPageController>().loadScore(this.score);
        resultPage.SetActive(true);
    }

    // Use this for initialization
    void Start() {
        resultPage.SetActive(false);
        overlay.SetActive(true);

        player.GetComponent<PlayerController>().player.destroyActions.Add(delegate {
            overlay.SetActive(false);
            displayResultPage();
        });

		lastBlock = instantGenerateNewTunnelBlock(TrackFactory.instance.getBlock());
        currentBlock = instantGenerateNewTunnelBlock(TrackFactory.instance.getBlock());
		nextBlock = generateNewTunnelBlock(TrackFactory.instance.getBlock());
		nextNextBlock = generateNewTunnelBlock(TrackFactory.instance.getBlock());
        player.GetComponent<PlayerController>().initialise(this.currentBlock.GetComponent<TunnelBlock>().track);
    }

    // Update is called once per frame
    void Update() {
        //resetIfOutOfBound();
        overlay.GetComponent<OverlayController>().loadScore(score);

        if (Input.GetKeyDown("s")) {
            player.GetComponent<PlayerController>().player.deductHp(200);
        }
    }



}
