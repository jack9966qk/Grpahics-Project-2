using UnityEngine;
using System.Collections.Generic;

public class Tunnel : MonoBehaviour {


	public GameObject tile;

	private const float RADIUS = 1f;


	// Use this for initialization
	void Start () {
		GenerateTunnel ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	private void GenerateTunnel(){
		
		Vector3 frontOrigin = new Vector3 (0, 0, 0);
		Vector3 backOrigin = new Vector3 (0, 0, 0);

		for (int i = 0; i < 20; i++) {

			//backOrigin = new Vector3(0f,Mathf.Sin(i*Mathf.PI/10),i);
			backOrigin = new Vector3(0f,0f,i);

			for(float degree = 0; degree < 360;degree = degree + 30){
				GameObject t = Instantiate (tile);
				t.GetComponent<Tile>().CreateTileMesh(frontOrigin,backOrigin,degree,RADIUS);

			}

			frontOrigin = backOrigin;

		}

	}



}
