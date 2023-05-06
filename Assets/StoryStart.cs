using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryStart : MonoBehaviour
{
    [SerializeField] AudioSource StoryLine;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StoryLine.Play();
    }
}
