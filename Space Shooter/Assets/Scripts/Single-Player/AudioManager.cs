using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip mainBackgroundMusic;
    
    [SerializeField] public AudioClip laserSound;
    [SerializeField] [Range(0f, 1f)] public float laserSoundVolume;
    
    [SerializeField] public AudioClip enemyLaserSound;
    [SerializeField] [Range(0f, 1f)] public float enemyLaserSoundVolume;
    
    [SerializeField] public AudioClip engineDamagedSound;
    [SerializeField] [Range(0f, 1f)] public float engineDamagedSoundVolume;
    
    [SerializeField] public AudioClip explosionSound;
    [SerializeField] [Range(0f, 1f)] public float explosionSoundVoulme;
    
    [SerializeField] public AudioClip collectedPowerupSound;
    [SerializeField] [Range(0f, 1f)] public float collectedPowerupSoundVolume;
    
    [SerializeField] public AudioClip shieldPowerdownSound;
    [SerializeField] [Range(0f, 1f)] public float shieldPowerdownSoundVolume;
    
    AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = mainBackgroundMusic;
        _audioSource.Play();
    }
}
