using UnityEngine;
using System.Collections;

public class ShaderScript : MonoBehaviour {

	public Texture texture;

	public Shader shader;

	public PointLight[] pointLights;

	// Use this for initialization


	public void initialise() {
		MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();

		// Set blend uniform parameter in an oscillating fashion to demonstrate 
		// blending shader (challenge question)
		renderer.material.SetFloat("_BlendFct", (Mathf.Sin(Time.time) + 1.0f) / 4.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void changeShader(string shadername){

	}
}
