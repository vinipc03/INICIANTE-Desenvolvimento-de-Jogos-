using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{

    public Transform player;

    public float attackTime;

    // Start is called before the first frame update
    void Start()
    {
        attackTime = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (GetComponent<Character>().life <= 0)
        {
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 1;
            this.enabled = false;
        }

        if (Vector2.Distance(transform.position, player.position) > 0.3f)
        {
            attackTime = 0;
            transform.position = Vector2.MoveTowards(transform.position, player.position, 0.5f * Time.deltaTime);
        }
        else
        {
            attackTime = attackTime + Time.deltaTime;
            if(attackTime >= 1)
            {
                attackTime = 0;
                player.GetComponent<Character>().life--;
            }
        }
        
    }
}
