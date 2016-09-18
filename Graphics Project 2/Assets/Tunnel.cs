using UnityEngine;
using System.Collections.Generic;

public class Tunnel : MonoBehaviour {


	public Shader shader;

	private const float RADIUS = 1f;


	private List<Vector3> vertices = new List<Vector3>();
	private List<Color> colors = new List<Color>();
	private List<Vector3> normals = new List<Vector3>();

	// Use this for initialization
	void Start () {
		
		MeshFilter tunnelMesh = this.gameObject.AddComponent<MeshFilter>();
		tunnelMesh.mesh = this.CreateTunnelMesh();

		// Add a MeshRenderer component. This component actually renders the mesh that
		// is defined by the MeshFilter component.
		MeshRenderer renderer = this.gameObject.AddComponent<MeshRenderer>();


		renderer.material.shader = shader;

	}
	
	// Update is called once per frame
	void Update () {
	
	}


	private Mesh CreateTunnelMesh(){
		Mesh m = new Mesh();
		m.name = "Tunnel";

		Vector3 frontOrigin;
		Vector3 backOrigin;

		for (int i = 0; i < 20; i++) {
			frontOrigin = new Vector3 (0, 0, i);
			backOrigin = new Vector3 (0, 0, i + 1);
			for(float degree = 0; degree < 360;degree = degree + 30){
				GenerateSideSquare (frontOrigin,backOrigin,degree);
			}

		}

		m.SetVertices(this.vertices);
		m.SetColors(this.colors);
		//m.SetNormals(this.normals);
		// Automatically define the triangles based on the number of vertices
		int[] triangles = new int[m.vertices.Length];
		for (int i = 0; i < m.vertices.Length; i++)
			triangles[i] = i;

		m.triangles = triangles;

		return m;
	}


	public void GenerateSideSquare(Vector3 frontOrigin, Vector3 backOrigin,float degree){

		float radian = degree * Mathf.PI / 180;
		float radian30 = 30 * Mathf.PI / 180;
		// See figure on Widipedia Dodecagon figure
		vertices.Add(new Vector3(Mathf.Sin(radian)*RADIUS+frontOrigin.x,Mathf.Cos(radian)*RADIUS+frontOrigin.y,frontOrigin.z));
		vertices.Add(new Vector3(Mathf.Sin(radian+radian30)*RADIUS+frontOrigin.x, Mathf.Cos(radian+radian30)*RADIUS+frontOrigin.y, frontOrigin.z));
		vertices.Add(new Vector3(Mathf.Sin(radian+radian30)*RADIUS+backOrigin.x, Mathf.Cos(radian+radian30)*RADIUS+backOrigin.y, backOrigin.z));
		vertices.Add(new Vector3(Mathf.Sin(radian)*RADIUS+frontOrigin.x,Mathf.Cos(radian)*RADIUS+frontOrigin.y,frontOrigin.z));
		vertices.Add(new Vector3(Mathf.Sin(radian+radian30)*RADIUS+backOrigin.x, Mathf.Cos(radian+radian30)*RADIUS+backOrigin.y, backOrigin.z));
		vertices.Add(new Vector3(Mathf.Sin(radian)*RADIUS+backOrigin.x,Mathf.Cos(radian)*RADIUS+backOrigin.y,backOrigin.z));

		Color c = new Color (0, (Mathf.Sin (radian) + 1) / 2, 0, 1);
		if (Mathf.Round(frontOrigin.z)% 2 == 1) {
			c = new Color (0, (Mathf.Sin (radian) + 1) / 2, 0, 1);
		} else {
			c = new Color (0, (Mathf.Cos (radian) + 1) / 2, 0, 1);

		}

		colors.Add(c);
		colors.Add(c);
		colors.Add(c);
		colors.Add(c);
		colors.Add(c);
		colors.Add(c);


	}


}
