using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class EnemySensor : MonoBehaviour
{
    [SerializeField] float attackRange = 1f;
    public float GetEnemySensorRange()
    {
        Debug.Log("Enemy Sensor Range" + attackRange);
        return attackRange;

    }

}
