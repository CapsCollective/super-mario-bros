using System.Collections;
using UnityEngine;

public class SoundGuy : MonoBehaviour
{
    public static SoundGuy Instance;
    private static AudioSource _currentLoop;
    
    public AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
    }
    
    public void PlaySound(string audioClipName, bool looping = false)
    {
        var audioClip = Resources.Load<AudioClip>("Sounds/" + audioClipName);
        PlaySound(audioClip, looping);
    }
    
    public void PlaySound(AudioClip audioClip, bool looping = false)
    {
        if (looping)
        {
            InstanceLoopingSound(audioClip);
        }
        else
        {
            StartCoroutine(InstanceSound(audioClip));
        }
    }

    private IEnumerator InstanceSound(AudioClip audioClip)
    {
        var newAudioObject = Instantiate(audioSource);
        newAudioObject.clip = audioClip;
        newAudioObject.Play();

        yield return new WaitForSeconds(audioClip.length);
        Destroy(newAudioObject.gameObject);
    }
    
    private void InstanceLoopingSound(AudioClip audioClip)
    {
        audioSource.loop = true;
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
