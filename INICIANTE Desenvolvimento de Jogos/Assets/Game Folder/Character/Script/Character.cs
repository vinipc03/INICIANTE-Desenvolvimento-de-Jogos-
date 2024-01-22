using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public int life;
    public Transform skin;
    public Transform cam;
    public Transform takeDamage;

    public AudioSource audioSource;
    public AudioClip addHeart;

    public Text heartCountText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(life <= 0)
        {
            skin.GetComponent<Animator>().Play("Die", -1);
        }

        if (transform.CompareTag("Player"))
        {
            heartCountText.text = life.ToString();
        }
        
    }

    public void PlayerDamage(int value)
    {
        life = life - value;
        skin.GetComponent<Animator>().Play("TakeDamage", 1);
        cam.GetComponent<Animator>().Play("CameraPlayerDamage", -1);
        GetComponent<PlayerController>().audioSource.PlayOneShot(GetComponent<PlayerController>().damageSound, 0.05f);
    }

    public void PlayerAddLife(int value)
    {
        life = life + value;
        skin.GetComponent<Animator>().Play("PlayerAddLife", 1);
        audioSource.PlayOneShot(addHeart, 0.2f);
    }
}
