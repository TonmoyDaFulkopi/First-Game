﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxLogic : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Reached here");
        if (other.tag == "Player")
        {
            GunLogic gunLogic = other.GetComponentInChildren<GunLogic>();
            gunLogic.refillAmmo();

            Destroy(gameObject);
        }
    }
}