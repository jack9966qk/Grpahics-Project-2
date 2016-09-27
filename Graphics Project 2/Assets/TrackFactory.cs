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

	public static int LENGTH = 50;

	public static TrackFactory instance {
		get {
			if (_instance == null) {
				_instance = new TrackFactory ();
			}
			return TrackFactory._instance;
		}
	}

	public static float DIST = 0.8f;


	public List<Vector3> getBlock(){

		float random = Random.value;


		if (random < 1.0f/3) {
			//Almost Straight
			return getStraight();
		} else if (random < 2.0f/3) {
			//random full cos arc
			return getFullCos ();
		} else {
			//random semi cos arc
			return getSemiCos ();
		} 

		return getStraight ();

	}

	private List<Vector3> getStraight(){
		//return a linear track according to last point
		List<Vector3> points = new List<Vector3> ();
		for (int i = 0; i < LENGTH ; i++) {
			points.Add (last + (new Vector3(0f,0f,i*DIST)));
			if(i==LENGTH -1){
				last = last + (new Vector3 (0f,0f,i*DIST));
			}
		}
		return points;
	}

	private List<Vector3> getFullCos(){
		//return a linear track according to last point
		List<Vector3> points = new List<Vector3> ();

		float XorY = Random.value;
		float degree = Random.value;
		Vector3 currPoint = last;
		points.Add (last);

		float previousIncline = 0.0f;
		float incline;
		for (int i = 1; i < LENGTH ; i++) {
			incline = 1 - Mathf.Cos (2 * i * Mathf.PI / LENGTH);
			float forward = Mathf.Sqrt(DIST-(incline-previousIncline)*(incline-previousIncline));

			if (XorY < 0.5) {
				currPoint.x = incline * DIST + last.x;
			}
			if (XorY > 0.5) {
				currPoint.y = incline * DIST + last.y;
			}
			currPoint.z += forward;
			points.Add (currPoint);
			if(i==LENGTH-1){
				last = currPoint;
			}
			previousIncline = incline;
		}
		return points;
	}


	private List<Vector3> getSemiCos(){
		List<Vector3> points = new List<Vector3> ();
		float XorY = Random.value;
		float degree = Random.value;
		Vector3 currPoint = last;
		points.Add (last);

		float previousIncline = 0.0f;
		float incline;
		for (int i = 1; i < LENGTH ; i++) {
			incline = 1 - Mathf.Cos (i * Mathf.PI / LENGTH);
			float forward = Mathf.Sqrt(DIST-(incline-previousIncline)*(incline-previousIncline));
			if (XorY < 0.5) {
				currPoint.x = incline * DIST + last.x;
			}
			if (XorY > 0.5) {
				currPoint.y = incline * DIST + last.y;
			}
			currPoint.z += forward;
			points.Add (currPoint);
			if(i==LENGTH-1){
				last = currPoint;
			}
			previousIncline = incline;
		}
		return points;
	}


}
