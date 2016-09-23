using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerController : MonoBehaviour {

    public GameplayController gameController;
    public Player player { get; private set; }
    private float velocity = 0.5f;
    private float accleration = 0.005f;
    private float angle = 0f;

    private bool initialised = false;
    private Vector3 last;
    private Vector3 next;
    private Vector3 secNext;
    private Queue<Vector3> track;
    private float dist = 0f;
    private float unitProgress = 0f;
    public float trackRadius = 0.7f;

    // Use this for initialization
    void Start() {
        player = new Player(100);
        player.destroyActions.Add(delegate {
            GameObject.Destroy(this.gameObject);
        });
    }

    // Update is called once per frame
    void Update() {
        if (!initialised) {
            return;
        }

        applyAccleration();
        moveForwardByDist(velocity * Time.fixedDeltaTime);
    }

    public void initialise(List<Vector3> firstTrack) {
        track = new Queue<Vector3>(firstTrack);
        last = track.Dequeue();
        next = track.Dequeue();
        secNext = track.Dequeue();
        initialised = true;
    }

    void stepOne() {
        last = next;
        next = secNext;

        if (track.Count == 0) {
            loadNextTrack();
        }

        secNext = track.Dequeue();
    }

    void loadNextTrack() {
        track = new Queue<Vector3>(gameController.gotoNextBlock());
    }

    void applyAccleration() {
        this.velocity = Math.Max(0f, this.velocity += this.accleration);
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


        Quaternion lastDirection = Quaternion.Euler(next - last);
        Quaternion nextDirection = Quaternion.Euler(secNext - next);

        this.transform.position = Vector3.Lerp(last, next, progress);
        this.transform.rotation = Quaternion.Lerp(lastDirection, nextDirection, progress);

        this.dist += t;
        this.unitProgress = progress;
    }
}
