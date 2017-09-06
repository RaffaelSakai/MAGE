using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaFlutuante : MonoBehaviour
{

    public Vector3 Direção;
    public Vector3 PosiçãoInicial;
    public GameObject player, cajado, plataforma;
    public float Limite;
    public float Velocidade;

    public bool Encima, Subindo, Equipado, PontoFinal;

    void Start()
    {
        cajado = null;
        Encima = false;
        Subindo = false;
        Equipado = false;
        PontoFinal = false;
        Direção.Normalize();
        PosiçãoInicial.Normalize();
        PosiçãoInicial = transform.position;
    }


    void Update()
    {
        if (Encima && Equipado && Subindo)
        {
            if (plataforma.transform.position.y < PosiçãoInicial.y + Limite)
            {
                plataforma.transform.position += Direção * Velocidade * Time.deltaTime;
            }
            else if (plataforma.transform.position.y >= PosiçãoInicial.y + Limite)
            {
                PontoFinal = true;
                
            }
        }
        if (Input.GetKey(KeyCode.F))
        {
            Subindo = true;
        }
        if(Input.GetKeyUp(KeyCode.F))
        {
            Subindo = false;
        }
        cajado = GameObject.FindGameObjectWithTag("Cajado");
        if(cajado != null)
        {
            Equipado = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject == player)
        {
            Encima = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            Encima = false;
            if(PontoFinal)
            {
                plataforma.gameObject.GetComponent<Rigidbody>().useGravity = true;
                StartCoroutine("Tempo");
            }
        }
    }
    IEnumerator Tempo()
    {
        yield return new WaitForSeconds(1.5f);
        PontoFinal = false;
        plataforma.gameObject.GetComponent<Rigidbody>().useGravity = false;
    }
}

