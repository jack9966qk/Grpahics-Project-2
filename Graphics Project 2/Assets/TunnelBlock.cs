using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TunnelBlock : MonoBehaviour {


    public GameObject tile;
    public List<Vector3> localTrack;
    public List<Vector3> track {
        get {
            return new List<Vector3>(localTrack.Select(vec =>
                this.transform.TransformDirection(vec)
            ));
        }
    }
    private const float RADIUS = 1f;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }


    public void GenerateTunnel(List<Vector3> points) {
        localTrack = points;
        Vector3? last = null;
        foreach (Vector3 point in points) {
            if (last != null) {
                for (float degree = 0; degree < 360; degree = degree + 30) {
                    GameObject t = Instantiate(tile);
                    t.transform.parent = this.gameObject.transform;
                    t.GetComponent<Tile>().CreateTileMesh((Vector3)last, point, degree, RADIUS);
                }
            }
            last = point;
        }
    }


}
