using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunLogic : MonoBehaviour
{
    //DRAG AND DROP THE OBJECTS TO INITIALIZE AT FIRST
    [SerializeField]
    GameObject m_bulletPrefab;
    [SerializeField]
    Transform m_bulletspawnPoint;


    const float MAX_COOLDOWN = 0.2f;
    float m_cooldown = 0f;


    const int MAX_AMMO = 10;
    int ammoCount = MAX_AMMO;

    [SerializeField]
    Text ammoText;



    [SerializeField]
    AudioClip m_gunShot;
    [SerializeField]
    AudioClip m_gunEmpty;
    [SerializeField]
    AudioClip m_reload;

    AudioSource m_audioSource;

    public bool m_isEquipped = false;

    Rigidbody m_rigidBody;

    Collider m_collider;


    private void Start()
    {
        setAmmoText();

        m_audioSource = GetComponent<AudioSource>();

        m_rigidBody = GetComponent<Rigidbody>();
        m_collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_isEquipped)
            return;
        if (m_cooldown > 0f)
        {
            m_cooldown -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Fire1") && m_cooldown <= 0f)
        {
            if (ammoCount > 0)
            {
                // NOTE: Clones an object you want to make copy of
                Instantiate(m_bulletPrefab, m_bulletspawnPoint.position, m_bulletspawnPoint.rotation * m_bulletPrefab.transform.rotation);
                m_cooldown = MAX_COOLDOWN;
                --ammoCount;
                playSound(m_gunShot);
                setAmmoText();
            }
            else
                playSound(m_gunEmpty);

        }
    }

    void playSound(AudioClip audio)
    {
        if (m_audioSource && audio)
        {
            m_audioSource.PlayOneShot(audio);
        }
    }

    void setAmmoText()
    {
        ammoText.text = "Ammo: " + ammoCount;
    }

    public void refillAmmo()
    {
        playSound(m_reload);
        ammoCount = MAX_AMMO;
        setAmmoText();
    }

    public void equipGun()
    {
        m_isEquipped = true;

        m_collider.enabled = false;
        m_rigidBody.useGravity = false;
    }

    public void unequipGun()
    {
        m_rigidBody.AddForce(transform.forward * 200f);
        m_isEquipped = false;

        m_collider.enabled = true;
        m_rigidBody.useGravity = true;
    }
}
