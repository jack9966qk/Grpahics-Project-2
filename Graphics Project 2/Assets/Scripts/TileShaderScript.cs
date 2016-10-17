using UnityEngine;
using System.Collections;

public class TileShaderScript : MonoBehaviour {

	public Texture [] textures;
	public Texture [] normals;
	private MeshRenderer rend;
	// Use this for initialization
	public Shader shader;
	public Color fog;
	public int applyTransparent = 0;


	private int curr=-1;

	void Start() {
		rend = this.gameObject.AddComponent<MeshRenderer>();
		rend.material.shader = shader;
		rend.material.SetColor("_FogColor", fog);
		changeTexture (0, 0);
	}

	// Update is called once per frame
	void Update () {

		int score = (int)transform.position.z;

		score = score % 120;

		if (score < 40) {
			changeTexture (0,0);
		} else if (score < 80) {
			changeTexture (1,1);
		} else {
			changeTexture (2,0);
		}


	}


	void changeTexture(int i,int trans){
		if (curr == i) {
			return;
		}
		rend.material.SetTexture ("_MainTex", textures[i]);
		rend.material.SetTexture ("_NormalTex", normals[i]);
		rend.material.SetInt ("_applyTransparent", trans);
		curr = i;

	}


}
