using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }

    private AudioSource cameraSource;

    [Header("Audio Clips")]
    [SerializeField] AudioClip bgm;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        cameraSource = Camera.main.GetComponent<AudioSource>();
        PlayBGM();
    }

    public void PlayBGM()
    {
        cameraSource.clip = bgm;
        cameraSource.loop = true;
        cameraSource.Play();
    }

    public void PlayAudio(AudioClip clip)
    {
        if (cameraSource.isPlaying)
            cameraSource.Stop();
        cameraSource.clip = clip;
        cameraSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        cameraSource.PlayOneShot(clip);
    }

    public void PlaySFXFromSource(AudioClip clip, AudioSource source)
    {
        source.PlayOneShot(clip);
    }
}
