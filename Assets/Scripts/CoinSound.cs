using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSound : MonoBehaviour
{
public AudioClip musicClipOne;

public AudioClip musicClipTwo;

public AudioSource musicSource;// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
    {
        musicSource.clip = musicClipOne;
        musicSource.Play();
    }
    }
}
