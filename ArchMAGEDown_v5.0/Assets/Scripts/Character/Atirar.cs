using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atirar : MonoBehaviour {
    public GameObject weaponPrefab, cajado;
    public Transform pointWeapon;
    public float weaponSpeed = 300;
    Mover move;
    public Animator anime;
    // Use this for initialization
    void Start ()
    {
        move = GetComponent<Mover>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
        cajado = GameObject.FindGameObjectWithTag("Cajado");
        if (Input.GetKeyDown(KeyCode.RightShift) && cajado != false)
        {
            Atacar();
            GameObject goWeapon = (GameObject)Instantiate(weaponPrefab, pointWeapon.position, Quaternion.identity);

            if (move.esquerda)
            {
                goWeapon.transform.localScale = new Vector3(-1, 1, 1);
                goWeapon.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.left * weaponSpeed);
            }
            else
            {
                goWeapon.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.right * weaponSpeed);
            }
        }
    }

    private void Atacar()
    {
        anime.SetBool("Atacar", true);
        StartCoroutine("EsperarAtirar");
    }
    IEnumerator EsperarAtirar()
    {
        yield return new WaitForSeconds(0.5f);
        //anime.SetBool("Atacar", false);
    }
}
