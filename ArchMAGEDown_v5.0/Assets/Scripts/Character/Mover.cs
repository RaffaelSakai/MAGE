using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField]
    float speed = 0.03F;
    [SerializeField]
    float jumpSpeed = 8.0F;
    [SerializeField]
    float gravity = 20.0F;
    bool pulando = false;
    bool Petrificado = false;
    public float Horizontal;
    bool teste = false;
    public int count = 2;
    private Vector3 moveDirection = Vector3.zero;
    CharacterController controller;
    Rigidbody body;
    bool Equipado;
    public Animator ControlAnim;

    GameObject Cajado;
    public bool esquerda;

    private void Awake()
    {
        Cajado = null;
        controller = GetComponent<CharacterController>();
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Cajado = GameObject.FindGameObjectWithTag("Cajado");

        if (controller.isGrounded && !Petrificado)
        {
            ControlAnim.SetBool("NoAr", false);
            //Reseta pulo duplo enquanto tiver no chão
            if (count < 2)
            {
                count++;
            }
            //anda
            if (Horizontal != 0)
            {
                moveDirection = new Vector3(Horizontal, 0, 0);
                //moveDirection = new Vector3(0, 0, Horizontal);
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= speed;
                if (Horizontal < 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    esquerda = true;
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    esquerda = false;
                }
                ControlAnim.SetBool("Andar", true);
            }
            //para
            else
            {
                ControlAnim.SetBool("Andar", false);
            }
            //pula
            if (Input.GetButtonDown("Jump") && Horizontal == 0)
            {
                Pular();
                moveDirection.y = jumpSpeed;
                pulando = true;
                count--;

            }
            //pula andando
            if (Input.GetButtonDown("Jump") && Horizontal != 0)
            {
                Pular();
                moveDirection.x = moveDirection.x * 4;
                moveDirection.y = jumpSpeed;
                pulando = true;
                count--;
            }
            gravity = 20;
        }
        else
        {
            //Pulo duplo
            if (Cajado != null && count > 0 && Input.GetButtonDown("Jump"))
            {
                Pular();
                moveDirection.y = jumpSpeed;
                pulando = false;
                count--;
            }
        }

        //Petrificar
        if (Input.GetKey(KeyCode.LeftShift))
        {
            gravity = 400;
            moveDirection.Normalize();
            moveDirection.y -= gravity * Time.deltaTime;
            Petrificado = true;
        }
        else
            Petrificado = false;

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    private void Pular()
    {
        ControlAnim.SetBool("Pular", true);
        StartCoroutine("EsperarPular");
        ControlAnim.SetBool("NoAr", true);
    }
    IEnumerator EsperarPular()
    {
        yield return new WaitForSeconds(0.2f);
        ControlAnim.SetBool("Pular", false);
    }
}
