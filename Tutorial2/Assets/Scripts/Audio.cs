using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioClip coin;
    void Start()
    {
        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = coin;
    }

    void OnCollisionEnter()  
    {
        GetComponent<AudioSource>().Play();
    }
}
