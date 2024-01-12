using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 vel;

    public Transform floorCollider;
    public Transform skin;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && floorCollider.GetComponent<FloorCollider>().canJump == true)
        {
            skin.GetComponent<Animator>().Play("Player_jump", -1);
            rb.velocity = Vector2.zero;
            floorCollider.GetComponent<FloorCollider>().canJump = false;
            //AO TERMINAR O JOGO VOLTAR PARA AULA 6 APLICAR CORREÇÃO NO JUMP
            rb.AddForce(new Vector2(0, 150));
        }
        vel = new Vector2(Input.GetAxisRaw("Horizontal"), rb.velocity.y);

        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            skin.GetComponent<Animator>().SetBool("PlayerRun", true);
        }
        else
        {
            skin.GetComponent<Animator>().SetBool("PlayerRun", false);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = vel;
    }
}
