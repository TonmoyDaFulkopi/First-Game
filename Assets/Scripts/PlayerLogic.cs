using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerLogic : MonoBehaviour
{


    CharacterController m_charactercontroller;


    //returns speed
    float m_movementSpeed = 5f;

    //FOR MOVEMENT IN 2D
    float m_horizontalInput;
    float m_verticalInput;
    Vector3 m_position;

    //THIS IS TO GET THE TOTAL AXIS INPUT
    Vector3 m_movementInput;

    //Jumping stuff

    [SerializeField]
    float m_jumpHeight = 9f;

    [SerializeField]
    float m_gravity = 0.5f;

    bool m_jump = false;

    GameObject m_interactiveObject = null;
    GameObject m_equippedObject = null;

    [SerializeField]
    Transform weaponEquipPosition;


    [SerializeField]
    Text m_healthText;

    int m_playerHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        m_charactercontroller = GetComponent<CharacterController>();

        setHealthText();
    }

    // Update is called once per frame
    void Update()
    {
        m_horizontalInput = Input.GetAxis("Horizontal");
        m_verticalInput = Input.GetAxis("Vertical");

        //THIS BASICALLY CONTAINS ALL THE INFO ABOUT THE INPUT AXIS
        m_movementInput = new Vector3(m_horizontalInput, 0, m_verticalInput);

        if (!m_jump && Input.GetButtonDown("Jump"))
        {
            m_jump = true;
        }

        // if (m_charactercontroller.isGrounded) Debug.Log("Character is grounded");

        if (m_interactiveObject && Input.GetButtonDown("Fire2"))
        {

            if (!m_equippedObject)
            {
                GunLogic gunLogic = m_interactiveObject.GetComponent<GunLogic>();

                if (gunLogic)
                {
                    //sets position of found object
                    m_interactiveObject.transform.position = weaponEquipPosition.position;
                    m_interactiveObject.transform.rotation = weaponEquipPosition.rotation;

                    m_interactiveObject.transform.parent = gameObject.transform;


                    gunLogic.equipGun();

                    m_equippedObject = m_interactiveObject;
                }
            }
            else if (m_equippedObject)
            {
                GunLogic gunLogic = m_equippedObject.GetComponent<GunLogic>();

                if (gunLogic)
                {
                    //sets position of found object
                    m_equippedObject.transform.parent = null;

                    gunLogic.unequipGun();

                    m_equippedObject = null;
                }
            }
        }
    }

    void setHealthText()
    {
        m_healthText.text = "Health: " + m_playerHealth;
    }

    public void takeDamage(int dmg)
    {
        m_playerHealth -= dmg;

        setHealthText();
    }


    void rotateTowardsCursor()
    {
        Vector3 mousePos = Input.mousePosition;

        // transform.position -> world space pos of player
        // WorldToScreenPoint -> converts whole shit to pixel stuff
        Vector3 playerPos = Camera.main.WorldToScreenPoint(transform.position);

        Vector3 distance = mousePos - playerPos;

        // atan2-> radian
        float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;

        // Vector3.up -> +Vector2 of y AXIS
        // rotates around y axis
        // -angle deyar lagbe noile ulta axis dhore kor
        transform.rotation = Quaternion.AngleAxis(+angle - 30, Vector3.down);
    }


    //for moving the whole object to the direction
    private void FixedUpdate()
    {
        m_position = m_movementInput * m_movementSpeed * Time.deltaTime;


        rotateTowardsCursor();

        //for facing that direction
        // if (Vector3.zero != m_movementInput)
        // {
        //     //rotates face
        //     transform.forward = m_position.normalized;

        //     // TooltipAttribute rotate while moving
        //     //         transform.forward = Quaternion.Euler(0, -90, 0) *m_position.normalized;
        // }



        if (!m_charactercontroller.isGrounded)
        {
            // NOTE: * Time.deltaTime wasnt the original plan, but IT BECOMES SUPER SMOOTH
            m_position.y -= m_gravity * Time.deltaTime;
        }

        else
        {
            // NOTICE: WILL SNAP THE EDGE PRECISELY TO GROUND IDK HOW
            m_position.y = 0f;
        }

        if (m_jump)
        {
            // NOTE: * Time.deltaTime wasnt the original plan, but IT BECOMES SUPER SMOOTH
            m_position.y += m_jumpHeight * Time.deltaTime;
            m_jump = false;
        }

        //add an if to be safe
        m_charactercontroller.Move(m_position);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Gun")
        {
            m_interactiveObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Gun" && m_interactiveObject == other.gameObject)
        {
            m_interactiveObject = null;
        }
    }
}
