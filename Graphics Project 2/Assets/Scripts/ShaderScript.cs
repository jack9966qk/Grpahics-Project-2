using UnityEngine;
using System.Collections;

public class ShaderScript : MonoBehaviour {

	public Texture texture;
	public Texture normal;
	// Use this for initialization
	public Shader shader;
	private Color fog;
	private MeshRenderer rend;

	void Start() {
		fog = Camera.main.backgroundColor;
		rend = this.gameObject.AddComponent<MeshRenderer>();
		rend.material.shader = shader;
		rend.material.SetTexture ("_MainTex", texture);
		rend.material.SetTexture ("_NormalTex", normal);
		rend.material.SetColor("_FogColor", fog);
	}
	
	// Update is called once per frame
	void Update () {


	}

}
