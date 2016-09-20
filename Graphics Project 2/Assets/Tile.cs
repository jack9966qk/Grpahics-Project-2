using UnityEngine;
using System.Collections.Generic;

public class Tile : MonoBehaviour {


	public Shader shader;

	// Update is called once per frame
	void Update () {



	}

	public void CreateTileMesh(Vector3 frontOrigin, Vector3 backOrigin,float degree,float radius){
		
		Mesh m = new Mesh();
		m.name = "Tile";


		float radian = degree * Mathf.PI / 180;
		float radian30 = 30 * Mathf.PI / 180;
		// See figure on Widipedia Dodecagon figure

		List<Vector3> vertices = new List<Vector3>();
		List<Color> colors = new List<Color>();
		Color r = new Color(Random.value,Random.value,Random.value,1f);

		vertices.Add(new Vector3(Mathf.Sin(radian)*radius+frontOrigin.x,Mathf.Cos(radian)*radius+frontOrigin.y,frontOrigin.z));
		vertices.Add(new Vector3(Mathf.Sin(radian+radian30)*radius+frontOrigin.x, Mathf.Cos(radian+radian30)*radius+frontOrigin.y, frontOrigin.z));
		vertices.Add(new Vector3(Mathf.Sin(radian+radian30)*radius+backOrigin.x, Mathf.Cos(radian+radian30)*radius+backOrigin.y, backOrigin.z));
		vertices.Add(new Vector3(Mathf.Sin(radian)*radius+frontOrigin.x,Mathf.Cos(radian)*radius+frontOrigin.y,frontOrigin.z));
		vertices.Add(new Vector3(Mathf.Sin(radian+radian30)*radius+backOrigin.x, Mathf.Cos(radian+radian30)*radius+backOrigin.y, backOrigin.z));
		vertices.Add(new Vector3(Mathf.Sin(radian)*radius+backOrigin.x,Mathf.Cos(radian)*radius+backOrigin.y,backOrigin.z));


		colors.Add(r);
		colors.Add(r);
		colors.Add(r);
		colors.Add(r);
		colors.Add(r);
		colors.Add(r);


		m.SetVertices (vertices);
		m.SetColors (colors);
		//m.SetNormals(this.normals);
		// Automatically define the triangles based on the number of vertices
		int[] triangles = new int[m.vertices.Length];
		for (int i = 0; i < m.vertices.Length; i++)
			triangles[i] = i;

		m.triangles = triangles;



		MeshFilter tileMesh = this.gameObject.AddComponent<MeshFilter>();
		tileMesh.mesh = m;

		// Add a MeshRenderer component. This component actually renders the mesh that
		// is defined by the MeshFilter component.
		MeshRenderer renderer = this.gameObject.AddComponent<MeshRenderer>();
		renderer.material.shader = shader;
	}


	private void Destroy(){


	}

}
