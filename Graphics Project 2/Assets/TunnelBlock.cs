using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TunnelBlock : MonoBehaviour {


    public GameObject tile;
	private List<Vector3> localTrack;
	private Queue<Vector3> localTrackNext;
	private Queue<Vector3> currentTrack;

	public Color odd;
	public Color even;
	private const float RADIUS = 1f;

	public Queue<GameObject> tiles = new Queue<GameObject>();

	private int count = 0;

    public List<Vector3> track {
        get {
            return new List<Vector3>(localTrack.Select(vec =>
                this.transform.TransformDirection(vec)
            ));
        }
    }

    public void GenerateTunnel(List<Vector3> points) {
		localTrack = points;
		currentTrack = new Queue<Vector3>(points);
		localTrackNext = new Queue<Vector3>(points);
		localTrackNext.Dequeue ();
    }

	public void InstantGenerateTunnel(List<Vector3> points) {
		localTrack = points;
		Vector3? last = null;
		int c = 0;
		foreach (Vector3 point in points) {
			if (last != null) {
				for (float degree = 0; degree < 360; degree = degree + 30) {
					GameObject t = Instantiate(tile);
					tiles.Enqueue (t);
					t.transform.parent = this.gameObject.transform;
					t.GetComponent<Tile>().CreateTileMesh((Vector3)last, point, degree, RADIUS,c%2==0?odd:even);
				}
			}
			last = point;
			c++;
		}
	}

	public void GenerateOneCircle(){
		if (localTrackNext.Count == 0) {
			return;
		}
		Vector3 last = currentTrack.Dequeue ();
		Vector3 point = localTrackNext.Dequeue ();
		for (float degree = 0; degree < 360; degree = degree + 30) {
			GameObject t = Instantiate(tile);
			tiles.Enqueue (t);
			t.transform.parent = this.gameObject.transform;
			t.GetComponent<Tile>().CreateTileMesh((Vector3)last, point, degree, RADIUS,count%2==0?odd:even);
		}
		count++;

	}

	public void DeleteOneCirle(){
		if(tiles.Count == 0){
			return;
		}
		for (float degree = 0; degree < 360; degree = degree + 30) {
			GameObject.Destroy (tiles.Dequeue());
		}
	}

}
