using UnityEngine;
using System.Collections;

public class TileShaderScript : MonoBehaviour {

	public Texture [] textures;
	public Texture [] normals;
	private MeshRenderer rend;
	// Use this for initialization
	public Shader [] shaders;
	private Color fog;

	private int curr;
	void Start() {

		fog = Camera.main.backgroundColor;

		rend = this.gameObject.AddComponent<MeshRenderer>();
		rend.material.shader = shaders[0];
		rend.material.SetTexture ("_MainTex", textures[0]);
		rend.material.SetTexture ("_NormalTex", normals[0]);
		rend.material.SetColor("_FogColor", fog);
	}

	// Update is called once per frame
	void Update () {

		int score = (int) transform.position.z;
		score = score % 60;

		if (score < 20) {
			changeShader (0);
		} else if (score < 40) {
			changeShader (1);
		} else if (score < 60) {
			changeShader (2);
		} 

	}


	void changeShader(int i){
		if (curr == i) {
			return;
		}

		rend.material.shader = shaders[i];
		rend.material.SetTexture ("_MainTex", textures[i]);
		rend.material.SetTexture ("_NormalTex", normals[i]);
		curr = i;

	}


}
