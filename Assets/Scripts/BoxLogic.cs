using System.Collections;
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


    private void OnTriggerStay(Collider other)
    {
        GunLogic gunLogic = other.GetComponentInChildren<GunLogic>();

        //NOTE: The BEST WAY to AVOID NULLPOINTEREXCEPTION IS TO CONDITIONAL WITH OBJECT like here gunLogic!!!!!!!!!!!!!!!!

        if (other.tag == "Player" && gunLogic)
        {
            // Debug.Log("Reached here");

            gunLogic.refillAmmo();

            Destroy(gameObject);
        }
    }
}
