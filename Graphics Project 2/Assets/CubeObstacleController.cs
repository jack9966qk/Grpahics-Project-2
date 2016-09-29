using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeObstacleController : MonoBehaviour
{
	public Shader shader;
	public PointLight[] pointLights;

	public Texture texture;
	public Texture normal;

	private const int MAX_LIGHTS = 10;

	private NormalObstacle obstacleModel;

	void Start() {
		obstacleModel.destroyActions.Add( delegate {
			GameObject.Destroy(this.gameObject);
		});
	}

	// Called each frame
	void Update() {
		// Get renderer component (in order to pass params to shader)
		MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();

		// Pass updated light positions to shader
		Vector4[] lightPositions = new Vector4[this.pointLights.Length];
		Color[] lightColors = new Color[this.pointLights.Length];

		if (this.pointLights.Length > MAX_LIGHTS) {
			Debug.LogError("Number of lights exceeds the maximum shader limit");
		}
		else {
			// Pass the actual number of lights to the shader
			renderer.material.SetInt("_NumPointLights", this.pointLights.Length);

			renderer.material.SetVectorArray( "_PointLightPositions", lightPositions);
			renderer.material.SetColorArray( "_PointLightColors", lightColors);
		}
	}


	public void PutCube(Vector3 origin, float degree, float radius) {

		obstacleModel = new NormalObstacle (10);

		MeshFilter cubeMesh = this.gameObject.AddComponent<MeshFilter>();
		cubeMesh.mesh = this.CreateCubeMesh();

		// Add a MeshRenderer component. This component actually renders the mesh that
		// is defined by the MeshFilter component.
		MeshRenderer renderer = this.gameObject.AddComponent<MeshRenderer>();
		renderer.material.shader = shader;
		renderer.material.SetTexture ("_MainTex", texture);
		renderer.material.SetTexture ("_NormalTex", normal);

		float radian = (degree+30) * Mathf.PI / 180;
		this.transform.eulerAngles =  new Vector3(0,0,180-15-degree);
		this.transform.localPosition = new Vector3 (origin.x + radius * Mathf.Sin (radian), origin.y + radius * Mathf.Cos (radian),
			origin.z);
		
		this.transform.localScale = new Vector3 (2*Mathf.Tan(Mathf.PI/12),2*Mathf.Tan(Mathf.PI/12),2*Mathf.Tan(Mathf.PI/12));


	}

	public void OnCollisionEnter(Collision collision) {
		obstacleModel.onCollisionWithPlayer (collision.gameObject.GetComponent<PlayerOriginController>().player);
	}



	// Method to create a cube mesh with coloured vertices
	private Mesh CreateCubeMesh() {
		Mesh m = new Mesh();
		m.name = "Cube";

		m.vertices = new[] {
			new Vector3(0.0f, 1.0f, 0.0f), // Top
			new Vector3(0.0f, 1.0f, 1.0f),
			new Vector3(1.0f, 1.0f, 1.0f),
			new Vector3(0.0f, 1.0f, 0.0f),
			new Vector3(1.0f, 1.0f, 1.0f),
			new Vector3(1.0f, 1.0f, 0.0f),

			new Vector3(0.0f, 0.0f, 0.0f), // Bottom
			new Vector3(1.0f, 0.0f, 1.0f),
			new Vector3(0.0f, 0.0f, 1.0f),
			new Vector3(0.0f, 0.0f, 0.0f),
			new Vector3(1.0f, 0.0f, 0.0f),
			new Vector3(1.0f, 0.0f, 1.0f),

			new Vector3(0.0f, 0.0f, 0.0f), // Left
			new Vector3(0.0f, 0.0f, 1.0f),
			new Vector3(0.0f, 1.0f, 1.0f),
			new Vector3(0.0f, 0.0f, 0.0f),
			new Vector3(0.0f, 1.0f, 1.0f),
			new Vector3(0.0f, 1.0f, 0.0f),

			new Vector3(1.0f, 0.0f, 0.0f), // Right
			new Vector3(1.0f, 1.0f, 1.0f),
			new Vector3(1.0f, 0.0f, 1.0f),
			new Vector3(1.0f, 0.0f, 0.0f),
			new Vector3(1.0f, 1.0f, 0.0f),
			new Vector3(1.0f, 1.0f, 1.0f),

			new Vector3(0.0f, 1.0f, 1.0f), // Front
			new Vector3(1.0f, 0.0f, 1.0f),
			new Vector3(1.0f, 1.0f, 1.0f),
			new Vector3(0.0f, 1.0f, 1.0f),
			new Vector3(0.0f, 0.0f, 1.0f),
			new Vector3(1.0f, 0.0f, 1.0f),

			new Vector3(0.0f, 1.0f, 0.0f), // Back
			new Vector3(1.0f, 1.0f, 0.0f),
			new Vector3(1.0f, 0.0f, 0.0f),
			new Vector3(0.0f, 0.0f, 0.0f),
			new Vector3(0.0f, 1.0f, 0.0f),
			new Vector3(1.0f, 0.0f, 0.0f)
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
			new Vector2(0.0f, 0.0f), // Top
			new Vector2(0.0f, 1.0f),
			new Vector2(1.0f, 1.0f),
			new Vector2(0.0f, 0.0f),
			new Vector2(1.0f, 1.0f),
			new Vector2(1.0f, 0.0f),

			new Vector2(0.0f, 1.0f), // Bottom
			new Vector2(1.0f, 0.0f),
			new Vector2(0.0f, 0.0f),
			new Vector2(0.0f, 1.0f),
			new Vector2(1.0f, 1.0f),
			new Vector2(1.0f, 0.0f),

			new Vector2(1.0f, 0.0f), // Left
			new Vector2(0.0f, 0.0f),
			new Vector2(0.0f, 1.0f),
			new Vector2(1.0f, 0.0f),
			new Vector2(0.0f, 1.0f),
			new Vector2(1.0f, 1.0f),

			new Vector2(0.0f, 0.0f), // Right
			new Vector2(1.0f, 1.0f),
			new Vector2(1.0f, 0.0f),
			new Vector2(0.0f, 0.0f),
			new Vector2(0.0f, 1.0f),
			new Vector2(1.0f, 1.0f),

			new Vector2(1.0f, 1.0f), // Front
			new Vector2(0.0f, 0.0f),
			new Vector2(0.0f, 1.0f),
			new Vector2(1.0f, 1.0f),
			new Vector2(1.0f, 0.0f),
			new Vector2(0.0f, 0.0f),

			new Vector2(0.0f, 1.0f), // Back
			new Vector2(1.0f, 1.0f),
			new Vector2(1.0f, 0.0f),
			new Vector2(0.0f, 0.0f),
			new Vector2(0.0f, 1.0f),
			new Vector2(1.0f, 0.0f)
		};

		// Define the normals
		Vector3 topNormal = new Vector3(0.0f, 1.0f, 0.0f);
		Vector3 bottomNormal = new Vector3(0.0f, -1.0f, 0.0f);
		Vector3 leftNormal = new Vector3(-1.0f, 0.0f, 0.0f);
		Vector3 rightNormal = new Vector3(1.0f, 0.0f, 0.0f);
		Vector3 frontNormal = new Vector3(0.0f, 0.0f, 1.0f);
		Vector3 backNormal = new Vector3(0.0f, 0.0f, -1.0f);

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
		Vector4 topTangent = new Vector3(1.0f, 0.0f, 0.0f);
		Vector4 bottomTangent = new Vector3(1.0f, 0.0f, 0.0f);
		Vector4 leftTangent = new Vector3(0.0f, 0.0f, -1.0f);
		Vector4 rightTangent = new Vector3(0.0f, 0.0f, 1.0f);
		Vector4 frontTangent = new Vector3(-1.0f, 0.0f, 0.0f);
		Vector4 backTangent = new Vector3(1.0f, 0.0f, 0.0f);

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
			triangles[i] = i;

		m.triangles = triangles;

		return m;
	}
}
