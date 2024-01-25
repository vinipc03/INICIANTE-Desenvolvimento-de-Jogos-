using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    Transform boss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            boss = collision.transform;
            collision.transform.parent = transform;
            collision.transform.localPosition = Vector3.zero;
        }
    }

    public void ReleaseBoss()
    {
        boss.parent = null;
    }
}
