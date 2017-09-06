using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

	// Use this for initialization
	Vector3 PosInicial;
	public GameObject Personagem;
	void Start () {
		PosInicial = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = Personagem.transform.position + PosInicial;
	}
}
