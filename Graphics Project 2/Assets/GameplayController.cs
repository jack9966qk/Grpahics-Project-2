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

    private GameObject currentBlock;
    private GameObject nextBlock;

	private GameObject straight;
	private GameObject xCos;
	private GameObject yCos;
	private GameObject xSemiCos;
	private GameObject ySemiCos;

	private GameObject straightObs;
	private GameObject xCosObs;
	private GameObject yCosObs;
	private GameObject xSemiCosObs;
	private GameObject ySemiCosObs;
	private int lastBlock = 0;
	private int blockCounter = -1;
	private int lastlastBlock = 0;

    private int Score {
        get {
            return Mathf.RoundToInt(playerOriginController.dist);
        }
    }

	GameObject GenerateTunnelBlock(List<Vector3> track) {
		var newBlock = GameObject.Instantiate(tunnelBlockPrefab);
		newBlock.GetComponent<TunnelBlock>().GenerateTunnel(track);
		return newBlock;
	}

	GameObject GenerateTunnelBlockWithSomeObs(List<Vector3> track) {
		var newBlock = GameObject.Instantiate(tunnelBlockPrefab);
		newBlock.GetComponent<TunnelBlock>().GenerateTunnelWithSomeObs(track);
		return newBlock;
	}

	GameObject GenerateTunnelBlockWithRoundObs(List<Vector3> track) {
		var newBlock = GameObject.Instantiate(tunnelBlockPrefab);
		newBlock.GetComponent<TunnelBlock>().GenerateTunnelWithRoundObs(track);
		return newBlock;
	}

	GameObject GenerateTunnelBlockWithItem(List<Vector3> track) {
		var newBlock = GameObject.Instantiate(tunnelBlockPrefab);
		newBlock.GetComponent<TunnelBlock>().GenerateTunnelWithItems(track);
		return newBlock;
	}

    public List<Vector3> GoToNextBlock() {
        currentBlock = nextBlock;
		nextBlock = GetNextBlock();
		nextBlock.transform.position = nextBlock.GetComponent<TunnelBlock> ().track [0];
        return currentBlock.GetComponent<TunnelBlock>().track;
    }

	private GameObject GetNextBlock(){
		if (blockCounter == -1) {
			blockCounter = 0;
		} else {
			while (lastBlock == blockCounter || lastlastBlock == blockCounter) {
				blockCounter = (int)(Random.value * 10);
			}
		}

		lastlastBlock = lastBlock;
		lastBlock = blockCounter;

		if(blockCounter%10 == 0){
			straight.GetComponent<TunnelBlock>().SetTrack(TrackFactory.GetStraight());
			return straight;
		}
		if(blockCounter%10 == 1){
			xCosObs.GetComponent<TunnelBlock>().SetTrack(TrackFactory.GetFullCos(0f));
			return xCosObs;
		}
		if(blockCounter%10 == 2){
			ySemiCos.GetComponent<TunnelBlock>().SetTrack(TrackFactory.GetSemiCos(1f));
			return ySemiCos;
		}
		if(blockCounter%10 == 3){
			xSemiCos.GetComponent<TunnelBlock>().SetTrack(TrackFactory.GetSemiCos(0f));
			return xSemiCos;
		}
		if(blockCounter%10 == 4){
			yCosObs.GetComponent<TunnelBlock>().SetTrack(TrackFactory.GetFullCos(1f));
			return yCosObs;
		}
		if(blockCounter%10 == 5){
			straightObs.GetComponent<TunnelBlock>().SetTrack(TrackFactory.GetStraight());
			return straightObs;
		}
		if(blockCounter%10 == 6){
			xCos.GetComponent<TunnelBlock>().SetTrack(TrackFactory.GetFullCos(0f));
			return xCos;
		}
		if(blockCounter%10 == 7){
			xSemiCosObs.GetComponent<TunnelBlock>().SetTrack(TrackFactory.GetSemiCos(0f));
			return xSemiCosObs;
		}
		if(blockCounter%10 == 8){
			yCos.GetComponent<TunnelBlock>().SetTrack(TrackFactory.GetFullCos(1f));
			return yCos;
		}
		if(blockCounter%10 == 9){
			ySemiCosObs.GetComponent<TunnelBlock>().SetTrack(TrackFactory.GetSemiCos(1f));
			return ySemiCosObs;
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

		straight = GenerateTunnelBlockWithItem (TrackFactory.GetStraightOnce());
		xCos = GenerateTunnelBlock (TrackFactory.GetFullCosOnce(0f));
		yCos = GenerateTunnelBlock (TrackFactory.GetFullCosOnce(1f));
		xSemiCos = GenerateTunnelBlock (TrackFactory.GetSemiCosOnce(0f));
		ySemiCos = GenerateTunnelBlock (TrackFactory.GetSemiCosOnce(1f));


		straightObs = GenerateTunnelBlockWithRoundObs (TrackFactory.GetStraightOnce());
		xCosObs = GenerateTunnelBlockWithSomeObs (TrackFactory.GetFullCosOnce(0f));
		yCosObs = GenerateTunnelBlockWithSomeObs (TrackFactory.GetFullCosOnce(1f));
		xSemiCosObs = GenerateTunnelBlockWithSomeObs (TrackFactory.GetSemiCosOnce(0f));
		ySemiCosObs = GenerateTunnelBlockWithRoundObs (TrackFactory.GetSemiCosOnce(1f));


		xCos.transform.position = new Vector3 (0, 10, 0);
		yCos.transform.position = new Vector3 (0, 10, 0);
		xSemiCos.transform.position = new Vector3 (0, 10, 0);
		ySemiCos.transform.position = new Vector3 (0, 10, 0);
		straightObs.transform.position = new Vector3 (0, 10, 0);
		xCosObs.transform.position = new Vector3 (0, 10, 0);
		yCosObs.transform.position = new Vector3 (0, 10, 0);
		xSemiCosObs.transform.position = new Vector3 (0, 10, 0);
		ySemiCosObs.transform.position = new Vector3 (0, 10, 0);


		currentBlock = GetNextBlock();
		nextBlock = GetNextBlock();
		nextBlock.transform.position = nextBlock.GetComponent<TunnelBlock> ().track [0];

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
