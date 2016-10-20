using UnityEngine;
using System.Collections;

public class TileShaderScript : MonoBehaviour {

    public float AmbientCoeff = 3f;
    public float DiffuseCoeff = 1f;
    public float SpecularCoeff = 0.25f;
    public float SpecularPower = 5f;

    public Texture [] textures;
	public Texture [] normals;
	public Texture[] transparencies;
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
        rend.material.SetFloat("_AmbientCoeff", AmbientCoeff);
        rend.material.SetFloat("_DiffuseCoeff", DiffuseCoeff);
        rend.material.SetFloat("_SpecularCoeff", SpecularCoeff);
        rend.material.SetFloat("_SpecularPower", SpecularPower);
        changeTexture (0, 0);
	}

	// Update is called once per frame
	void Update () {

		int score = (int)transform.position.z;

		score = score % 160;

		if (score < 40) {
			changeTexture (0, 0);
		} else if (score < 80) {
			changeTexture (1, 1);
		} else if (score < 120) {
			changeTexture (2, 0);
		} else if (score < 160) {
			changeTexture (3, 1);
		}


	}


	void changeTexture(int i,int trans){
		if (curr == i) {
			return;
		}
		rend.material.SetTexture ("_MainTex", textures[i]);
		rend.material.SetTexture ("_NormalTex", normals[i]);
		rend.material.SetTexture ("_TransparencyTex", transparencies [i]);
		rend.material.SetInt ("_ApplyTransparent", trans);
		curr = i;

	}


}
