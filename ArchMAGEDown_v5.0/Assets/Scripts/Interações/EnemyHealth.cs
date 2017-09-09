using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{

    public GameObject player, goblin;
    public bool playerInRange;
    NavMeshAgent nav;
    public Animator anim;

    private void Awake()
    {
        nav = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
    private void Update()
    {

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
            anim.SetBool("Andar", false);
            nav.enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
            nav.enabled = true;
            anim.SetBool("Andar", true);
            nav.SetDestination(player.transform.position);
            //if (nav.remainingDistance <= nav.stoppingDistance)
            //{
            //    playerInRange = false;
            //    anim.SetBool("Andar", false);
            //    nav.enabled = false;
            //}
        }

        if (other.CompareTag ("Fireball"))
        {
            Destroy(gameObject,1);
        }
    }
}
