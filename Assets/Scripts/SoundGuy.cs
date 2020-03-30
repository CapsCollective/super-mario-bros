using System;
using System.Collections;
using UnityEngine;

public class SoundGuy : MonoBehaviour
{
    public static SoundGuy Instance;
    private static AudioSource _currentLoop;
    
    public AudioSource audioSource;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance)
            return;
        Instance = this;
        _currentLoop = audioSource;
    }
    
    public void PlaySound(string audioClipName, bool looping = false, Action callback = null)
    {
        var audioClip = Resources.Load<AudioClip>("Sounds/" + audioClipName);
        PlaySound(audioClip, looping, callback);
    }

    public void PlaySound(AudioClip audioClip, bool looping = false, Action callback = null)
    {
        if (looping)
        {
            InstanceLoopingSound(audioClip);
        }
        else
        {
            StartCoroutine(InstanceSound(audioClip, callback));
        }
    }

    private IEnumerator InstanceSound(AudioClip audioClip, Action callback)
    {
        var newAudioObject = Instantiate(audioSource);
        newAudioObject.clip = audioClip;
        newAudioObject.Play();

        yield return new WaitForSeconds(audioClip.length);
        callback?.Invoke();
        Destroy(newAudioObject.gameObject);
    }
    
    private static void InstanceLoopingSound(AudioClip audioClip)
    {
        _currentLoop.loop = true;
        _currentLoop.clip = audioClip;
        _currentLoop.Play();
    }
}
