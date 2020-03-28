using System.Collections;
using UnityEngine;

public class DummyScript : MonoBehaviour
{
    void Start()
    {
        SoundGuy.Instance.PlaySound("main_theme", true);
        StartCoroutine(PlayStarSound());
    }
    
    private IEnumerator PlayStarSound()
    {
        yield return new WaitForSeconds(5);
        SoundGuy.Instance.PlaySound("star_theme", true);
        yield return new WaitForSeconds(3);
        SoundGuy.Instance.PlaySound("main_theme", true);
        SoundGuy.Instance.PlaySound("Player damage 2", false, OnSoundEnd);
        yield return new WaitForSeconds(3);
        SoundGuy.Instance.PlaySound("Player damage 2", false, OnSoundEnd);
    }

    private void OnSoundEnd()
    {
        print("I got called!");
    }
}
