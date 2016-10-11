using UnityEngine;
using System.Collections;

public class ShaderScript : MonoBehaviour {

	public Texture texture;
	public Texture normal;

	public Shader shader;


	// Use this for initialization


	public void initialise() {
		MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();

		renderer.material.shader = shader;
		renderer.material.SetTexture ("_MainTex", texture);
		renderer.material.SetTexture ("_NormalTex", normal);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void changeShader(string shadername){

	}
}
