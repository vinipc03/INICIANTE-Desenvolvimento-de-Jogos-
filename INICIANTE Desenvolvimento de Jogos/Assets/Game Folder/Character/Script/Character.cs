﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public int life;
    public Transform skin;
    public Transform cam;

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

        heartCountText.text = "x" + life.ToString();
    }

    public void PlayerDamage(int value)
    {
        life = life - value;
        skin.GetComponent<Animator>().Play("PlayerDamage", 1);
        cam.GetComponent<Animator>().Play("CameraPlayerDamage", -1);
    }

    public void PlayerAddLife(int value)
    {
        life = life + value;
        skin.GetComponent<Animator>().Play("PlayerAddLife", 1);
    }
}
