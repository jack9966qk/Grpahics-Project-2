using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TunnelBlock : MonoBehaviour {


    public GameObject tile;
	public GameObject CubeObstacle;

	private List<Vector3> localTrack;
	private Queue<Vector3> localTrackNext;
	private Queue<Vector3> currentTrack;

	public Color odd;
	public Color even;
	private const float RADIUS = 1f;

	public Queue<GameObject> tiles = new Queue<GameObject>();

    public List<Vector3> track {
        get {
            return new List<Vector3>(localTrack.Select(vec =>
                this.transform.TransformDirection(vec)
            ));
        }

    }

	public void GenerateTunnel(List<Vector3> points) {
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

	public void GenerateTunnelWithRoundObs(List<Vector3> points) {
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

					if (degree == (c-10)*30) {
						GameObject obs = Instantiate (CubeObstacle);
						obs.transform.parent = this.gameObject.transform;
						obs.GetComponent<CubeObstacleController> ().PutCube ((Vector3)last, point, degree, RADIUS);
					}

				}
			}
			last = point;
			c++;
		}
	}

	public void GenerateTunnelWithSomeObs(List<Vector3> points) {
		localTrack = points;
		Vector3? last = null;
		int c = 0;
		int obsDegree = 0;
		foreach (Vector3 point in points) {
			if (last != null) {
				for (float degree = 0; degree < 360; degree = degree + 30) {
					GameObject t = Instantiate(tile);
					tiles.Enqueue (t);
					t.transform.parent = this.gameObject.transform;
					t.GetComponent<Tile>().CreateTileMesh((Vector3)last, point, degree, RADIUS,c%2==0?odd:even);

					if (c%5 == 0 && obsDegree == degree) {
						GameObject obs = Instantiate (CubeObstacle);
						obs.transform.parent = this.gameObject.transform;
						obs.GetComponent<CubeObstacleController> ().PutCube ((Vector3)last, point, degree, RADIUS);
						obsDegree += 120;
						if (obsDegree >= 360) {
							obsDegree = 60;
						}
					}

				}
			}
			last = point;
			c++;
		}
	}




	public void SetTrack(List<Vector3> localTrack){
		this.localTrack = localTrack;
	}

}
