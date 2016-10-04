using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class GameplayController : MonoBehaviour {
    public GameObject resultPage;
    public GameObject overlay;

    public PlayerOriginController playerOriginController;
    public PlayerObjectController playerObjectController;
	public GameObject playerWorldPosition;
    public GameObject tunnelBlockPrefab;

    private GameObject currentBlock;
    private GameObject nextBlock;
	private GameObject nextNextBlock;


	private GameObject straight;
	private GameObject xCos;
	private GameObject yCos;
	private GameObject xSemiCos;
	private GameObject ySemiCos;

	private int blockCounter = -1;

    private int Score {
        get {
            return Mathf.RoundToInt(playerOriginController.dist);
        }
    }

	GameObject GenerateNewTunnelBlock(List<Vector3> track) {
		var newBlock = GameObject.Instantiate(tunnelBlockPrefab);
		newBlock.GetComponent<TunnelBlock>().InstantGenerateTunnel(track);
		return newBlock;
	}

    public List<Vector3> GoToNextBlock() {
        
        currentBlock = nextBlock;
		nextBlock = nextNextBlock;
		nextNextBlock = GetNextBlock();
		nextNextBlock.transform.position = nextNextBlock.GetComponent<TunnelBlock> ().track [0];
        
        return currentBlock.GetComponent<TunnelBlock>().track;

    }

	private GameObject GetNextBlock(){
		blockCounter++;

		if(blockCounter%5 == 0){
			straight.GetComponent<TunnelBlock>().SetTrack(TrackFactory.GetStraight());
			return straight;
		}
		if(blockCounter%5 == 1){
			xCos.GetComponent<TunnelBlock>().SetTrack(TrackFactory.GetFullCos(0f));
			return xCos;
		}
		if(blockCounter%5 == 2){
			yCos.GetComponent<TunnelBlock>().SetTrack(TrackFactory.GetFullCos(1));
			return yCos;
		}
		if(blockCounter%5 == 3){
			xSemiCos.GetComponent<TunnelBlock>().SetTrack(TrackFactory.GetSemiCos(0f));
			return xSemiCos;
		}
		if(blockCounter%5 == 4){
			ySemiCos.GetComponent<TunnelBlock>().SetTrack(TrackFactory.GetSemiCos(1f));
			return ySemiCos;
		}
		return null;
	}

    void DisplayResultPage() {
        if (resultPage != null) {
            if (overlay != null) {
                overlay.SetActive(false);
            }
            resultPage.GetComponent<ResultPageController>().loadScore(this.Score);
            resultPage.SetActive(true);
        }
    }

    void PrepareUI() {
        if (resultPage != null) {
            resultPage.SetActive(false);
        }

        if (overlay != null) {
            overlay.SetActive(true);
        }
    }

    // Use this for initialization
    void Start() {

		straight = GenerateNewTunnelBlock (TrackFactory.GetStraightOnce());
		xCos = GenerateNewTunnelBlock (TrackFactory.GetFullCosOnce(0f));
		yCos = GenerateNewTunnelBlock (TrackFactory.GetFullCosOnce(1f));
		xSemiCos = GenerateNewTunnelBlock (TrackFactory.GetSemiCosOnce(0f));
		ySemiCos = GenerateNewTunnelBlock (TrackFactory.GetSemiCosOnce(1f));

		xCos.transform.position = new Vector3 (0, 10, 0);
		yCos.transform.position = new Vector3 (0, 10, 0);
		xSemiCos.transform.position = new Vector3 (0, 10, 0);
		ySemiCos.transform.position = new Vector3 (0, 10, 0);

		currentBlock = GetNextBlock();
		nextBlock = GetNextBlock();
		nextBlock.transform.position = nextBlock.GetComponent<TunnelBlock> ().track [0];
		nextNextBlock = GetNextBlock();
		nextNextBlock.transform.position = nextNextBlock.GetComponent<TunnelBlock> ().track [0];

        Application.targetFrameRate = 60;
        GlobalState.instance.gameController = this;
        PrepareUI();

        playerObjectController.player.destroyActions.Add(delegate {
            DisplayResultPage();
        });


		playerOriginController.initialise(currentBlock.GetComponent<TunnelBlock>().track);

    }

    // Update is called once per frame
    void Update() {
        //resetIfOutOfBound();
        if (overlay != null) {
            var controller = overlay.GetComponent<OverlayController>();
            controller.loadScore(Score);
            controller.loadItem(playerObjectController.player.item);
            controller.loadHP(playerObjectController.player.hp,
                playerObjectController.player.maxHp);
        }

        if (Input.GetKeyDown("s")) {
            playerObjectController.player.deductHp(50);
        }
    }



}
