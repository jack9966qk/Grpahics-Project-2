using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TunnelBlock : MonoBehaviour {


    public GameObject tile;
	private List<Vector3> localTrack;
	private Queue<Vector3> localTrackNext;
	private Queue<Vector3> currentTrack;
	private Vector3? last = null;

	public Color odd;
	public Color even;

	private int count = 0;

    public List<Vector3> track {
        get {
            return new List<Vector3>(localTrack.Select(vec =>
                this.transform.TransformDirection(vec)
            ));
        }
    }
    private const float RADIUS = 1f;


	private bool finished;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

		if (!finished) {
			Vector3 last = currentTrack.Dequeue ();
			Vector3 point = localTrackNext.Dequeue ();

			for (float degree = 0; degree < 360; degree = degree + 30) {
				GameObject t = Instantiate(tile);
				t.transform.parent = this.gameObject.transform;
				t.GetComponent<Tile>().CreateTileMesh((Vector3)last, point, degree, RADIUS,count%2==0?odd:even);
			}
			count++;
			if (localTrackNext.Count == 0) {
				finished = true;
			}

		}

    }


    public void GenerateTunnel(List<Vector3> points) {
		localTrack = points;
		currentTrack = new Queue<Vector3>(points);
		localTrackNext = new Queue<Vector3>(points);

		finished = false;
		localTrackNext.Dequeue ();

    }


}
