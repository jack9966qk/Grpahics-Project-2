using UnityEngine;
using System.Collections.Generic;

public class TunnelBlock : MonoBehaviour {


	public GameObject tile;
	public GameObject cubeObstacle;
	private const float RADIUS = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void GenerateTunnel(List<Vector3> points){
		Vector3? last = null;
		foreach (Vector3 point in points) {
			if (last != null) {
				for(float degree = 0; degree < 360;degree = degree + 30){
					GameObject t = Instantiate (tile);
					t.transform.parent = this.gameObject.transform;
					t.GetComponent<Tile>().CreateTileMesh( (Vector3) last,point,degree,RADIUS);

					if (degree == 30) {
						GameObject b = Instantiate (cubeObstacle);
						b.GetComponent<CubeObstacle> ().PutCube ((Vector3)last, degree, RADIUS);
					}
				}
			}
			last = point;
		}
	}
		

}
