using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSonsPrincipal : MonoBehaviour
{
    public AudioClip musique;
    public float coefSound = 1;

    public AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source.clip = musique;
        source.volume = coefSound;
        source.loop = true;
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
