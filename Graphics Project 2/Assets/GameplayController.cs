using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class GameplayController : MonoBehaviour {
    public GameObject resultPage;
    public GameObject overlay;

    public PlayerOriginController playerOriginController;
    public PlayerObjectController playerObjectController;
    public GameObject tunnelBlockPrefab;

    private GameObject lastBlock;
    private GameObject currentBlock;
    private GameObject nextBlock;
    private GameObject nextNextBlock;

    private int score {
        get {
            return Mathf.RoundToInt(playerOriginController.dist);
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
        if (resultPage != null) {
            if (overlay != null) {
                overlay.SetActive(false);
            }
            resultPage.GetComponent<ResultPageController>().loadScore(this.score);
            resultPage.SetActive(true);
        }
    }

    void prepareUI() {
        if (resultPage != null) {
            resultPage.SetActive(false);
        }

        if (overlay != null) {
            overlay.SetActive(true);
        }
    }

    // Use this for initialization
    void Start() {
        prepareUI();

        playerObjectController.player.destroyActions.Add(delegate {
            displayResultPage();
        });

		lastBlock = instantGenerateNewTunnelBlock(TrackFactory.instance.getBlock());
        currentBlock = instantGenerateNewTunnelBlock(TrackFactory.instance.getBlock());
		nextBlock = generateNewTunnelBlock(TrackFactory.instance.getBlock());
		nextNextBlock = generateNewTunnelBlock(TrackFactory.instance.getBlock());
        playerOriginController.initialise(this.currentBlock.GetComponent<TunnelBlock>().track);
    }

    // Update is called once per frame
    void Update() {
        //resetIfOutOfBound();
        if (overlay != null) {
            overlay.GetComponent<OverlayController>().loadScore(score);
        }

        if (Input.GetKeyDown("s")) {
            playerObjectController.player.deductHp(200);
        }
    }



}
