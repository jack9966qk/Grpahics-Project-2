using UnityEngine;
using System.Collections;

public class ItemBoxMeshGenerator : MonoBehaviour {

	// Method to create a cube mesh with coloured vertices
	private Mesh CreateCubeMesh() {
		Mesh m = new Mesh ();
		m.name = "Cube";

		m.vertices = new[] {
			new Vector3 (0.0f, 1.0f, 0.0f), // Top
			new Vector3 (0.0f, 1.0f, 1.0f),
			new Vector3 (1.0f, 1.0f, 1.0f),
			new Vector3 (0.0f, 1.0f, 0.0f),
			new Vector3 (1.0f, 1.0f, 1.0f),
			new Vector3 (1.0f, 1.0f, 0.0f),

			new Vector3 (0.0f, 0.0f, 0.0f), // Bottom
			new Vector3 (1.0f, 0.0f, 1.0f),
			new Vector3 (0.0f, 0.0f, 1.0f),
			new Vector3 (0.0f, 0.0f, 0.0f),
			new Vector3 (1.0f, 0.0f, 0.0f),
			new Vector3 (1.0f, 0.0f, 1.0f),

			new Vector3 (0.0f, 0.0f, 0.0f), // Left
			new Vector3 (0.0f, 0.0f, 1.0f),
			new Vector3 (0.0f, 1.0f, 1.0f),
			new Vector3 (0.0f, 0.0f, 0.0f),
			new Vector3 (0.0f, 1.0f, 1.0f),
			new Vector3 (0.0f, 1.0f, 0.0f),

			new Vector3 (1.0f, 0.0f, 0.0f), // Right
			new Vector3 (1.0f, 1.0f, 1.0f),
			new Vector3 (1.0f, 0.0f, 1.0f),
			new Vector3 (1.0f, 0.0f, 0.0f),
			new Vector3 (1.0f, 1.0f, 0.0f),
			new Vector3 (1.0f, 1.0f, 1.0f),

			new Vector3 (0.0f, 1.0f, 1.0f), // Front
			new Vector3 (1.0f, 0.0f, 1.0f),
			new Vector3 (1.0f, 1.0f, 1.0f),
			new Vector3 (0.0f, 1.0f, 1.0f),
			new Vector3 (0.0f, 0.0f, 1.0f),
			new Vector3 (1.0f, 0.0f, 1.0f),

			new Vector3 (0.0f, 1.0f, 0.0f), // Back
			new Vector3 (1.0f, 1.0f, 0.0f),
			new Vector3 (1.0f, 0.0f, 0.0f),
			new Vector3 (0.0f, 0.0f, 0.0f),
			new Vector3 (0.0f, 1.0f, 0.0f),
			new Vector3 (1.0f, 0.0f, 0.0f)
		};

		// Define the vertex colours
		m.colors = new[] {
			Color.red, // Top
			Color.red,
			Color.red,
			Color.red,
			Color.red,
			Color.red,

			Color.red, // Bottom
			Color.red,
			Color.red,
			Color.red,
			Color.red,
			Color.red,

			Color.yellow, // Left
			Color.yellow,
			Color.yellow,
			Color.yellow,
			Color.yellow,
			Color.yellow,

			Color.yellow, // Right
			Color.yellow,
			Color.yellow,
			Color.yellow,
			Color.yellow,
			Color.yellow,

			Color.cyan, // Front
			Color.cyan,
			Color.cyan,
			Color.cyan,
			Color.cyan,
			Color.cyan,

			Color.cyan, // Back
			Color.cyan,
			Color.cyan,
			Color.cyan,
			Color.cyan,
			Color.cyan
		};

		// Define the UV coordinates
		m.uv = new[] {
			new Vector2 (0.0f, 0.0f), // Top
			new Vector2 (0.0f, 1.0f),
			new Vector2 (1.0f, 1.0f),
			new Vector2 (0.0f, 0.0f),
			new Vector2 (1.0f, 1.0f),
			new Vector2 (1.0f, 0.0f),

			new Vector2 (0.0f, 1.0f), // Bottom
			new Vector2 (1.0f, 0.0f),
			new Vector2 (0.0f, 0.0f),
			new Vector2 (0.0f, 1.0f),
			new Vector2 (1.0f, 1.0f),
			new Vector2 (1.0f, 0.0f),

			new Vector2 (1.0f, 0.0f), // Left
			new Vector2 (0.0f, 0.0f),
			new Vector2 (0.0f, 1.0f),
			new Vector2 (1.0f, 0.0f),
			new Vector2 (0.0f, 1.0f),
			new Vector2 (1.0f, 1.0f),

			new Vector2 (0.0f, 0.0f), // Right
			new Vector2 (1.0f, 1.0f),
			new Vector2 (1.0f, 0.0f),
			new Vector2 (0.0f, 0.0f),
			new Vector2 (0.0f, 1.0f),
			new Vector2 (1.0f, 1.0f),

			new Vector2 (1.0f, 1.0f), // Front
			new Vector2 (0.0f, 0.0f),
			new Vector2 (0.0f, 1.0f),
			new Vector2 (1.0f, 1.0f),
			new Vector2 (1.0f, 0.0f),
			new Vector2 (0.0f, 0.0f),

			new Vector2 (0.0f, 1.0f), // Back
			new Vector2 (1.0f, 1.0f),
			new Vector2 (1.0f, 0.0f),
			new Vector2 (0.0f, 0.0f),
			new Vector2 (0.0f, 1.0f),
			new Vector2 (1.0f, 0.0f)
		};

		// Define the normals
		Vector3 topNormal = new Vector3 (0.0f, 1.0f, 0.0f);
		Vector3 bottomNormal = new Vector3 (0.0f, -1.0f, 0.0f);
		Vector3 leftNormal = new Vector3 (-1.0f, 0.0f, 0.0f);
		Vector3 rightNormal = new Vector3 (1.0f, 0.0f, 0.0f);
		Vector3 frontNormal = new Vector3 (0.0f, 0.0f, 1.0f);
		Vector3 backNormal = new Vector3 (0.0f, 0.0f, -1.0f);

		m.normals = new[] {
			topNormal, // Top
			topNormal,
			topNormal,
			topNormal,
			topNormal,
			topNormal,

			bottomNormal, // Bottom
			bottomNormal,
			bottomNormal,
			bottomNormal,
			bottomNormal,
			bottomNormal,

			leftNormal, // Left
			leftNormal,
			leftNormal,
			leftNormal,
			leftNormal,
			leftNormal,

			rightNormal, // Right
			rightNormal,
			rightNormal,
			rightNormal,
			rightNormal,
			rightNormal,

			frontNormal, // Front
			frontNormal,
			frontNormal,
			frontNormal,
			frontNormal,
			frontNormal,

			backNormal, // Back
			backNormal,
			backNormal,
			backNormal,
			backNormal,
			backNormal
		};

		// Define mesh tangents
		Vector4 topTangent = new Vector3 (1.0f, 0.0f, 0.0f);
		Vector4 bottomTangent = new Vector3 (1.0f, 0.0f, 0.0f);
		Vector4 leftTangent = new Vector3 (0.0f, 0.0f, -1.0f);
		Vector4 rightTangent = new Vector3 (0.0f, 0.0f, 1.0f);
		Vector4 frontTangent = new Vector3 (-1.0f, 0.0f, 0.0f);
		Vector4 backTangent = new Vector3 (1.0f, 0.0f, 0.0f);

		m.tangents = new[] {
			topTangent, // Top
			topTangent,
			topTangent,
			topTangent,
			topTangent,
			topTangent,

			bottomTangent, // Bottom
			bottomTangent,
			bottomTangent,
			bottomTangent,
			bottomTangent,
			bottomTangent,

			leftTangent, // Left
			leftTangent,
			leftTangent,
			leftTangent,
			leftTangent,
			leftTangent,

			rightTangent, // Right
			rightTangent,
			rightTangent,
			rightTangent,
			rightTangent,
			rightTangent,

			frontTangent, // Front
			frontTangent,
			frontTangent,
			frontTangent,
			frontTangent,
			frontTangent,

			backTangent, // Back
			backTangent,
			backTangent,
			backTangent,
			backTangent,
			backTangent
		};

		// Automatically define the triangles based on the number of vertices
		int[] triangles = new int[m.vertices.Length];
		for (int i = 0; i < m.vertices.Length; i++)
			triangles [i] = i;

		m.triangles = triangles;

		return m;
	}

	// Use this for initialization
	void Start () {
		MeshFilter cubeMesh = this.gameObject.AddComponent<MeshFilter>();
		cubeMesh.mesh = this.CreateCubeMesh();
	}

}
