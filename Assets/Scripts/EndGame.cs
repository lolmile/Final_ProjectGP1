using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] GameObject EndMenu;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EndMenu.SetActive(true);
    }
}
