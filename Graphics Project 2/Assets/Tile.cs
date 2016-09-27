using UnityEngine;
using System.Collections.Generic;

public class Tile : MonoBehaviour {
	
	private const int MAX_LIGHTS = 10;

	public Texture texture;

	public Shader shader;

	public PointLight[] pointLights;


	void Update()
	{
		MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();

		// Set blend uniform parameter in an oscillating fashion to demonstrate 
		// blending shader (challenge question)
		renderer.material.SetFloat("_BlendFct", (Mathf.Sin(Time.time) + 1.0f) / 4.0f);

	}



	public void CreateTileMesh(Vector3 frontOrigin, Vector3 backOrigin,float degree,float radius,Color r){
		
		Mesh m = new Mesh();
		m.name = "Tile";


		float radian = degree * Mathf.PI / 180;
		float radian30 = 30 * Mathf.PI / 180;
		// See figure on Widipedia Dodecagon figure

		List<Vector3> vertices = new List<Vector3>();
		List<Color> colors = new List<Color>();
		List<Vector3> uvs = new List<Vector3>();


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

		// Define the UV coordinates
		uvs.Add(new Vector2(0.0f, 0.666f));// Top
		uvs.Add(new Vector2(0.333f, 1.0f));
		uvs.Add (new Vector2 (0.0f, 1.0f));
		uvs.Add(new Vector2(0.0f, 0.666f));
		uvs.Add(new Vector2(0.333f, 0.666f));
		uvs.Add(new Vector2(0.333f, 1.0f));

		m.SetUVs (1,uvs);
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
		renderer.material.mainTexture = texture;
	}


	private void Destroy(){


	}

}
