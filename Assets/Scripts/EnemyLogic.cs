using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyState
{
    Idle,
    Patrol,
    Chase,
    Attack
}

public class EnemyLogic : MonoBehaviour
{
    [SerializeField]
    EnemyState m_enemyCurrState = EnemyState.Idle;

    private void Start()
    {

    }

    private void Update()
    {

    }

}
