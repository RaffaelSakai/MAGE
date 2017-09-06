using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegarCajado : MonoBehaviour
{
    public GameObject CajadoCaido, CajadoProtagonista, Mago;
    public Transform Mao, Rotação;
    PlataformaFlutuante plataforma;
    bool Encima;
    

    void Start ()
    {

    }
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.E) && Encima)
        {
            Instantiate(CajadoProtagonista, Mao.transform.position, Rotação.transform.rotation, Mao);
            Destroy(CajadoCaido);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == Mago)
        {
            Encima = true;
        }
    }
}
