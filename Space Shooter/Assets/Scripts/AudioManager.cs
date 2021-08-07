using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioClip mainBackgroundMusic;

    [SerializeField] public AudioClip laserSound;
    [SerializeField] [Range(0f, 1f)] public float laserSoundVolume;

    AudioSource _audioSource;

    // Start is called before the first frame update
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        _audioSource.clip = mainBackgroundMusic;
        _audioSource.Play();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
