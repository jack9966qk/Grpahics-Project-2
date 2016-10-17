using UnityEngine;
using System.Collections;

public class ShaderScript : MonoBehaviour {

	public Texture texture;
	public Texture normal;
	// Use this for initialization
	public Shader shader;
	public Color fog;
	public int applyTransparent = 0;
	private MeshRenderer rend;

	void Start() {
		fog = Camera.main.backgroundColor;
		rend = this.gameObject.AddComponent<MeshRenderer>();
		rend.material.shader = shader;
		rend.material.SetTexture ("_MainTex", texture);
		rend.material.SetTexture ("_NormalTex", normal);
		rend.material.SetColor("_FogColor", fog);
		rend.material.SetInt ("_applyTransparent", applyTransparent);
	}
	
	// Update is called once per frame
	void Update () {


	}




}
