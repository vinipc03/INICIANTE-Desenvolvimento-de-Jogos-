using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 vel;
    public AudioSource audioSource;
    public AudioClip attack1Sound;
    public AudioClip attack2Sound;
    public AudioClip damageSound;
    public AudioClip dashSound;

    public Transform floorCollider;
    public Transform skin;

    public Transform gameOverScreen;
    public Transform pauseScreen;

    public int comboNum;
    public float comboTime;
    public float dashTime;

    public string currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        currentLevel = SceneManager.GetActiveScene().name;
        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!currentLevel.Equals(SceneManager.GetActiveScene().name))
        {
            currentLevel = SceneManager.GetActiveScene().name;
            transform.position = GameObject.Find("Spawn").transform.position;
        }

        //MORTE
        if(GetComponent<Character>().life <= 0)
        {
            gameOverScreen.GetComponent<GameOver>().enabled = true;
            rb.simulated = false;
            this.enabled = false;
        }

        if (Input.GetButtonDown("Cancel"))
        {
            pauseScreen.GetComponent<Pause>().enabled = !pauseScreen.GetComponent<Pause>().enabled;
        }

        //DASH
        dashTime = dashTime + Time.deltaTime;
        if (Input.GetButtonDown("Fire2") && dashTime > 1f)
        {
            audioSource.PlayOneShot(dashSound, 0.2f);
            dashTime = 0;
            skin.GetComponent<Animator>().Play("Player_dash", -1);
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(skin.localScale.x * 600, 0));
            rb.gravityScale = 0;
            Invoke("RestoreGravityScale", 0.3f);
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

            if(comboNum == 1)
            {
                audioSource.PlayOneShot(attack1Sound, 0.3f);
            }

            if (comboNum == 2)
            {
                audioSource.PlayOneShot(attack2Sound, 0.2f);
            }
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

    public void DestroyPlayer()
    {
        Destroy(transform.gameObject);
    }

    void RestoreGravityScale()
    {
        rb.gravityScale = 6;
    }
}
