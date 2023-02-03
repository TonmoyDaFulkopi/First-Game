using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinLogic : MonoBehaviour
{
    AudioSource m_audioSource;


    //IT WONT PLAY SOUND WITHOUT MAKING THE AUDIOCOIN PUBLIC/SERIALIZEFIELD
    [SerializeField]
    AudioClip m_audioCoin;

    MeshRenderer m_renderer;
    Collider m_collider;


    // Start is called before the first frame update
    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();

        m_renderer = GetComponent<MeshRenderer>();
        m_collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.back, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // Destroy(gameObject);
            if (m_audioSource && m_audioCoin)
                m_audioSource.PlayOneShot(m_audioCoin);

            m_renderer.enabled = false;
            m_collider.enabled = false;
        }
    }
}
