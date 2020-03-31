using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mariotransform : MonoBehaviour
{
    private float delay = 0.1f;
    public bool isTransforming = false;
    public bool isDamaged = false;
    SpriteRenderer[] sprites;
    Transform[] transforms;
    private bool inTransform = false;
    private IEnumerator coroutineTransform;
    private IEnumerator coroutineDamaged;


    // Start is called before the first frame update
    void Start()
    {

        sprites = GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.enabled = false;
        }

        transforms = GetComponentsInChildren<Transform>();
        foreach (Transform transform in transforms)
        {
            transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

        sprites[0].enabled = true;
        transforms[0].gameObject.GetComponent<BoxCollider2D>().enabled = true;

        coroutineTransform = marioTransform();
        coroutineDamaged = marioDamaged();
       
    }

    void Update()
    {
        if (isTransforming && !inTransform)
        {
            StartCoroutine(coroutineTransform);
            Time.timeScale = 0;
            isTransforming = false;
            inTransform = true;
            transforms[1].GetComponent<BoxCollider2D>().enabled = false;
            transforms[3].GetComponent<BoxCollider2D>().enabled = true;
        }

        if (isDamaged && inTransform)
        {
            StartCoroutine(coroutineDamaged);
            Time.timeScale = 0;
            isDamaged = false;
            inTransform = false;
            transforms[3].GetComponent<BoxCollider2D>().enabled = false;
            transforms[1].GetComponent<BoxCollider2D>().enabled = true;
        }
    }


    IEnumerator marioTransform()
    {
        sprites[0].enabled = true;
        yield return new WaitForSecondsRealtime(delay);
        sprites[0].enabled = false;
        sprites[1].enabled = true;
        yield return new WaitForSecondsRealtime(delay);
        sprites[1].enabled = false;
        sprites[0].enabled = true;
        yield return new WaitForSecondsRealtime(delay);
        sprites[0].enabled = false;
        sprites[1].enabled = true;
        yield return new WaitForSecondsRealtime(delay);
        sprites[1].enabled = false;
        sprites[0].enabled = true;
        yield return new WaitForSecondsRealtime(delay);
        sprites[0].enabled = false;
        sprites[1].enabled = true;
        yield return new WaitForSecondsRealtime(delay);
        sprites[1].enabled = false;
        sprites[2].enabled = true;
        yield return new WaitForSecondsRealtime(delay);
        sprites[2].enabled = false;
        sprites[0].enabled = true;
        yield return new WaitForSecondsRealtime(delay);
        sprites[0].enabled = false;
        sprites[1].enabled = true;
        yield return new WaitForSecondsRealtime(delay);
        sprites[1].enabled = false;
        sprites[2].enabled = true;
        Time.timeScale = 1;
        StopCoroutine(coroutineTransform);
    }

    IEnumerator marioDamaged()
    {
        sprites[2].enabled = false;
        sprites[1].enabled = true;
        yield return new WaitForSecondsRealtime(delay);
        sprites[1].enabled = false;
        sprites[2].enabled = true;
        yield return new WaitForSecondsRealtime(delay);
        sprites[2].enabled = false;
        sprites[1].enabled = true;
        yield return new WaitForSecondsRealtime(delay);
        sprites[1].enabled = false;
        sprites[2].enabled = true;
        yield return new WaitForSecondsRealtime(delay);
        sprites[2].enabled = false;
        sprites[1].enabled = true;
        yield return new WaitForSecondsRealtime(delay);
        sprites[1].enabled = false;
        sprites[0].enabled = true;
        yield return new WaitForSecondsRealtime(delay);
        sprites[0].enabled = false;
        sprites[2].enabled = true;
        yield return new WaitForSecondsRealtime(delay);
        sprites[2].enabled = false;
        sprites[1].enabled = true;
        yield return new WaitForSecondsRealtime(delay);
        sprites[1].enabled = false;
        sprites[0].enabled = true;
        Time.timeScale = 1;
        StopCoroutine(coroutineDamaged);
    }
}
