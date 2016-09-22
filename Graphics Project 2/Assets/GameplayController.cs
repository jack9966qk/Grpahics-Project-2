using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class GameplayController : MonoBehaviour {

    private const int BOUND_SIZE = 5000;

    public GameObject player;
    public GameObject tunnelBlockPrefab;

    private GameObject lastBlock;
    private GameObject currentBlock;
    private GameObject nextBlock;
    private GameObject nextNextBlock;


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

        Debug.Log(currentBlock.GetComponent<TunnelBlock>().track);

        return currentBlock.GetComponent<TunnelBlock>().track;

    }

    public List<Vector3> getCurrentTrack() {
        return this.currentBlock.GetComponent<TunnelBlock>().track;
    }

    void resetIfOutOfBound() {
        Vector3 pos = player.transform.position;
        if (pos.x > BOUND_SIZE ||
            pos.x < -BOUND_SIZE ||
            pos.y > BOUND_SIZE ||
            pos.y < -BOUND_SIZE ||
            pos.z > BOUND_SIZE ||
            pos.z < -BOUND_SIZE) {
            resetPositions();
        }
    }

    void resetPositions() {
        var allGameObjects = GameObject.FindObjectsOfType<GameObject>();
        Vector3 playerPos = player.transform.position;
        player.transform.position = Vector3.zero;
        Vector3 playerMovement = Vector3.zero - playerPos;
        foreach (GameObject o in allGameObjects) {
            o.transform.Translate(playerMovement);
        }
        player.GetComponent<PlayerController>().translateControlPoints(playerMovement);
    }

    void displayResultPage() {
        throw new System.NotImplementedException();
    }

    // Use this for initialization
    void Start() {
        player.GetComponent<PlayerController>().player.destroyActions.Add(delegate {
            displayResultPage();
        });

        currentBlock = generateNewTunnelBlock(TrackFactory.instance.getSin());
        nextBlock = generateNewTunnelBlock(TrackFactory.instance.getSin());
        nextNextBlock = generateNewTunnelBlock(TrackFactory.instance.getSin());

        player.GetComponent<PlayerController>().initialise(this.currentBlock.GetComponent<TunnelBlock>().track);
        //		nextBlock = generateNextBlockPoints ();
        //		nextNextBlock = generateNextBlockPoints ();
        //
        //		GenerateTunnel (points);
    }

    // Update is called once per frame
    void Update() {

    }



}
