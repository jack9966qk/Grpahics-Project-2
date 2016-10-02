using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerOriginController : MonoBehaviour {

    public GameplayController gameController;
    public GameObject playerObject;
    public PlayerObjectController playerObjectController;
	public float velocity = 2f;
	public float maxVelocity = 5f;
    public float extraVelocity = 0f;
	public float accleration = 0.005f;
	public float angle = 0f;
    public float keyboardSpeed = 5f;


    private bool initialised = false;

    private Vector3 last;
    private Vector3 next;
    private Vector3 secNext;
    private Queue<Vector3> track;
    public float dist { get; private set; }
    private float unitProgress = 0f;
    public float trackRadius = 0.7f;

    // Use this for initialization
    void Start() {
        dist = 0f;
    }

    // Update is called once per frame
    void Update() {
        if (!initialised) {
            return;
        }

        if (playerObject == null) {
            return;
        }

        applyAccleration();
        moveForwardByDist((extraVelocity + velocity) * Time.fixedDeltaTime);

        handleInput();
    }

    void handleInput() {

        // keyboard control
        if (Input.GetKey(KeyCode.A)) {
            this.transform.rotation *= Quaternion.AngleAxis(-keyboardSpeed * Time.deltaTime, Vector3.forward);
        }

        if (Input.GetKey(KeyCode.D)) {
            this.transform.rotation *= Quaternion.AngleAxis(keyboardSpeed * Time.deltaTime, Vector3.forward);
        }

        // acclerometer control
        Vector3 acceleration = AdjustableAcclerometer.getAdjustedAccleration();
        this.transform.rotation *= Quaternion.AngleAxis(
            acceleration.x * GlobalState.instance.settings.acclerometerSensitivity * Time.deltaTime,
            Vector3.forward
        );

    }

    public void initialise(List<Vector3> firstTrack) {
        track = new Queue<Vector3>(firstTrack);
        last = track.Dequeue();
        next = track.Dequeue();
        secNext = track.Dequeue();
        initialised = true;
    }

    public void addAccleration(float amount) {
        this.accleration += amount;
    }

    void stepOne() {
        last = next;
        next = secNext;

		gameController.generateMore ();
		gameController.deleteMore ();

        if (track.Count == 0) {
            loadNextTrack();
        }
        secNext = track.Dequeue();
    }

    void loadNextTrack() {
         track = new Queue<Vector3>(gameController.gotoNextBlock());
         track.Dequeue();

    }

    void applyAccleration() {
        this.velocity = Math.Min(maxVelocity, Math.Max(0f, this.velocity += this.accleration));
    }

    public void translateControlPoints(Vector3 v) {
        last += v;
        next += v;
        secNext += v;

        Queue<Vector3> newTrack = new Queue<Vector3>();
        while (track.Count > 0) {
            newTrack.Enqueue(track.Dequeue() + v);
        }
        track = newTrack;
    }

    // make the player object move forward by t
    void moveForwardByDist(float t) {

        float progress = t + unitProgress;
        while (progress > 1.0f) {
            stepOne();
            progress -= 1.0f;
        }

        Vector3 lastDirection = next - last;
        Vector3 nextDirection = secNext - next;

        this.transform.position = Vector3.Lerp(last, next, progress);
        var lerpedRotation = Vector3.Lerp(lastDirection, nextDirection, progress);
        playerObject.transform.rotation = Quaternion.LookRotation(lerpedRotation, this.transform.position - playerObject.transform.position);

        this.dist += t;
        this.unitProgress = progress;
    }
}
