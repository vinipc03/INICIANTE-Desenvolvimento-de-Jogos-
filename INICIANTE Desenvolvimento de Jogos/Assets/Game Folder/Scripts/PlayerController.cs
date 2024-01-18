using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 vel;

    public Transform floorCollider;
    public Transform skin;

    public int comboNum;
    public float comboTime;
    public float dashTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //MORTE
        if(GetComponent<Character>().life <= 0)
        {
            this.enabled = false;
        }

        //DASH
        dashTime = dashTime + Time.deltaTime;
        if (Input.GetButtonDown("Fire2") && dashTime > 1f)
        {
            dashTime = 0;
            skin.GetComponent<Animator>().Play("Player_dash", -1);
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(skin.localScale.x * 800, 0));
        }

        //ATAQUE
        comboTime = comboTime + Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && comboTime > 0.5f)
        {
            comboNum++;
            if(comboNum > 2)
            {
                comboNum = 1;
            }
            comboTime = 0;
            skin.GetComponent<Animator>().Play("Player_attack" + comboNum, -1);
        }

        if(comboTime >= 1)
        {
            comboNum = 0;
        }

        //PULO
        if (Input.GetButtonDown("Jump") && floorCollider.GetComponent<FloorCollider>().canJump == true)
        {
            skin.GetComponent<Animator>().Play("Player_jump", -1);
            rb.velocity = Vector2.zero;
            floorCollider.GetComponent<FloorCollider>().canJump = false;
            //AO TERMINAR O JOGO VOLTAR PARA AULA 6 APLICAR CORREÇÃO NO JUMP
            rb.AddForce(new Vector2(0, 1000));
        }
        vel = new Vector2(Input.GetAxisRaw("Horizontal"), rb.velocity.y);

        //MOVIMENTAÇÃO
        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            skin.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
            vel = new Vector2(Input.GetAxisRaw("Horizontal") * 8, rb.velocity.y);
            skin.GetComponent<Animator>().SetBool("PlayerRun", true);
        }
        else
        {
            skin.GetComponent<Animator>().SetBool("PlayerRun", false);
        }
    }

    private void FixedUpdate()
    {
        if(dashTime > 0.3)
        {
            rb.velocity = vel;
        }
        
    }
}
