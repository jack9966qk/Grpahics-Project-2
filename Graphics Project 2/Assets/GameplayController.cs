using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class GameplayController : MonoBehaviour {

    private const int BOUND_SIZE = 50;

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

    public List<Vector3> gotoNextBlock() {
        // destroy lastBlock
        if (lastBlock != null) {
            GameObject.Destroy(lastBlock);
        }

        // pass over
        lastBlock = currentBlock;
        currentBlock = nextBlock;
        nextBlock = nextNextBlock;

        // generate nextnextBlock
        List<Vector3> newTrack = TrackFactory.instance.getBlock();
        nextNextBlock = generateNewTunnelBlock(newTrack);

        return currentBlock.GetComponent<TunnelBlock>().track;

    }

    void resetIfOutOfBound() {
        Vector3 pos = player.transform.position;
        if (pos.x > BOUND_SIZE ||
            pos.x < -BOUND_SIZE ||
            pos.y > BOUND_SIZE ||
            pos.y < -BOUND_SIZE ||
            pos.z > BOUND_SIZE ||
            pos.z < -BOUND_SIZE) {
            Debug.Log("Will Reset");
            resetPositions();
            Debug.Log("Did Reset");
        }
    }

    void resetPositions() {
        var allGameObjects = GameObject.FindObjectsOfType<GameObject>();
        Vector3 playerPos = player.transform.position;
        Vector3 displacement = Vector3.zero - playerPos;
        foreach (GameObject o in allGameObjects) {
            o.transform.Translate(displacement);
        }
        player.transform.position = Vector3.zero;
        player.GetComponent<PlayerController>().translateControlPoints(displacement);
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

        currentBlock = generateNewTunnelBlock(TrackFactory.instance.getBlock());
		nextBlock = generateNewTunnelBlock(TrackFactory.instance.getBlock());
		nextNextBlock = generateNewTunnelBlock(TrackFactory.instance.getBlock());

        player.GetComponent<PlayerController>().initialise(this.currentBlock.GetComponent<TunnelBlock>().track);
        //		nextBlock = generateNextBlockPoints ();
        //		nextNextBlock = generateNextBlockPoints ();
        //
        //		GenerateTunnel (points);
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
