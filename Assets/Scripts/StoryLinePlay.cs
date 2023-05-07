using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryLinePlay : MonoBehaviour
{
    [SerializeField] AudioSource Story;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Story.Play();
    }
}
