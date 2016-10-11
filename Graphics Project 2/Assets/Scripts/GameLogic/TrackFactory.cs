using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// Track factory. Static class
/// 
/// </summary>
public static class TrackFactory {

	private static Vector3 last = Vector3.zero;

	public static int LENGTH = 30;

	public static float DIST = 0.8f;

	public static List<Vector3> GetStraight(){
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


	public static List<Vector3> GetFullCos(float XorY){
		//return a linear track according to last point
		List<Vector3> points = new List<Vector3> ();

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


	public static List<Vector3> GetSemiCos(float XorY){
		List<Vector3> points = new List<Vector3> ();

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



	public static List<Vector3> GetFullCosOnce(float XorY){
		//return a linear track according to last point
		last = Vector3.zero;
		List<Vector3> points = GetFullCos(XorY);
		last = Vector3.zero;
		return points;
	}


	public static List<Vector3> GetSemiCosOnce(float XorY){
		last = Vector3.zero;
		List<Vector3> points = GetSemiCos(XorY);
		last = Vector3.zero;
		return points;
	}


	public static List<Vector3> GetStraightOnce(){
		//return a linear track according to last point
		last = Vector3.zero;
		List<Vector3> points = GetStraight();
		last = Vector3.zero;
		return points;
	}


}
