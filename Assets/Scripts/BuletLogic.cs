using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuletLogic : MonoBehaviour
{
    Rigidbody m_rigidbody;

    [SerializeField]
    float m_bulletSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();

        if (m_rigidbody)
        {
            m_rigidbody.velocity = transform.up * m_bulletSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Target")
        {
            Destroy(other.gameObject);

            Destroy(gameObject);
        }
    }
}
