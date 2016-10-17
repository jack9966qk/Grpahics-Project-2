using UnityEngine;
using System.Collections;

public class ShaderScript : MonoBehaviour {

    public float AmbientCoeff = 3f;
    public float DiffuseCoeff = 1f;
    public float SpecularCoeff = 0.25f;
    public float SpecularPower = 5f;


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
		rend.material.SetInt ("_ApplyTransparent", applyTransparent);
        rend.material.SetFloat("_AmbientCoeff", AmbientCoeff);
        rend.material.SetFloat("_DiffuseCoeff", DiffuseCoeff);
        rend.material.SetFloat("_SpecularCoeff", SpecularCoeff);
        rend.material.SetFloat("_SpecularPower", SpecularPower);
    }

    // Update is called once per frame
    void Update () {


	}




}
