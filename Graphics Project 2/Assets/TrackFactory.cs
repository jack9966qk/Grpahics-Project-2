using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// Track factory. Singleton class
/// 
/// It can generate a list of vertices which represent a track by calling
/// getBlock() function.
/// 
/// Sample Usage: 
///     List<Vector3> track = TrackFactory.instance.getBlock();
/// 
/// </summary>
public class TrackFactory {

	private static TrackFactory _instance = null;

	private Vector3 last = Vector3.zero;

	public static int LENGTH = 40;

	public static TrackFactory instance {
		get {
			if (_instance == null) {
				_instance = new TrackFactory ();
			}
			return TrackFactory._instance;
		}
	}


	public List<Vector3> getBlock(){

		float random = Random.value;


		if (random < 0.25f) {
			//Almost Straight
			return getStraight();
		} else if (random < 0.5f) {
			//random sin arc

			return getSin ();
		} else if (random < 0.75f) {
			//random circle arc
			return getStraight ();
		} else if (random < 1f) {
			//random S shape
			return getStraight ();


		}

		return getStraight ();


	}

	public List<Vector3> getStraight(){
		//return a linear track according to last point
		List<Vector3> points = new List<Vector3> ();
		for (int i = 0; i < LENGTH ; i++) {
			points.Add (last + (new Vector3(0f,0f,i)));
			if(i==LENGTH -1){
				last = last + (new Vector3 (0f,0f,i));
			}
		}
		return points;
	}

	public List<Vector3> getSin(){
		//return a linear track according to last point
		List<Vector3> points = new List<Vector3> ();
		float randomX = Random.value;
		float randomY = Random.value;
		float randomDegree = Random.value;
		for (int i = 0; i < LENGTH ; i++) {
			float degree = i * Mathf.PI / (40 + 90 * randomDegree);
			points.Add (last + (new Vector3 (40*randomX * Mathf.Sin (degree), randomY * Mathf.Sin (degree), i)));
			if(i==LENGTH -1){
				last = last + (new Vector3 (40*randomX * Mathf.Sin (degree), randomY * Mathf.Sin (degree), i));
			}
		}
		return points;
	}

}
