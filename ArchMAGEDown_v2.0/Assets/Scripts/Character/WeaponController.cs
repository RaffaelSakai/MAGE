using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour
{

    private float beginPos;
    private float range = 15f;

    private void Start()
    {
        beginPos = transform.position.x;
    }
    private void Update()
    {
        if (transform.position.x > beginPos + range || transform.position.x < beginPos - range)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}