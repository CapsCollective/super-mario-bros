using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mariotransform : MonoBehaviour
{
    private float delay = 0.2f;
    public bool isTransform = false;
    public bool isDamaged = false;
    SpriteRenderer[] sprites;
    private IEnumerator coroutineTransform;
    private IEnumerator coroutineDamaged;

    IEnumerator marioTransform()
    {
        if (isTransform)
        {
            sprites[0].enabled = true;
            Debug.Log("1");
            yield return new WaitForSeconds(delay);
            sprites[0].enabled = false;
            sprites[1].enabled = true;
            Debug.Log("2");
            yield return new WaitForSeconds(delay);
            sprites[1].enabled = false;
            sprites[0].enabled = true;
            Debug.Log("3");
            yield return new WaitForSeconds(delay);
            sprites[0].enabled = false;
            sprites[1].enabled = true;
            Debug.Log("4");
            yield return new WaitForSeconds(delay);
            sprites[1].enabled = false;
            sprites[0].enabled = true;
            Debug.Log("5");
            yield return new WaitForSeconds(delay);
            sprites[0].enabled = false;
            sprites[1].enabled = true;
            Debug.Log("6");
            yield return new WaitForSeconds(delay);
            sprites[1].enabled = false;
            sprites[2].enabled = true;
            Debug.Log("7");
            yield return new WaitForSeconds(delay);
            sprites[2].enabled = false;
            sprites[0].enabled = true;
            Debug.Log("8");
            yield return new WaitForSeconds(delay);
            sprites[0].enabled = false;
            sprites[1].enabled = true;
            Debug.Log("9");
            yield return new WaitForSeconds(delay);
            sprites[1].enabled = false;
            sprites[2].enabled = true;
            Debug.Log("10");
        }

    }

    IEnumerator marioDamaged()
    {
        if (isTransform)
        {
            sprites[2].enabled = false;
            sprites[1].enabled = true;
            Debug.Log("1");
            yield return new WaitForSeconds(delay);
            sprites[1].enabled = false;
            sprites[2].enabled = true;
            Debug.Log("2");
            yield return new WaitForSeconds(delay);
            sprites[2].enabled = false;
            sprites[1].enabled = true;
            Debug.Log("3");
            yield return new WaitForSeconds(delay);
            sprites[1].enabled = false;
            sprites[2].enabled = true;
            Debug.Log("4");
            yield return new WaitForSeconds(delay);
            sprites[2].enabled = false;
            sprites[1].enabled = true;
            Debug.Log("5");
            yield return new WaitForSeconds(delay);
            sprites[1].enabled = false;
            sprites[0].enabled = true;
            Debug.Log("6");
            yield return new WaitForSeconds(delay);
            sprites[0].enabled = false;
            sprites[2].enabled = true;
            Debug.Log("7");
            yield return new WaitForSeconds(delay);
            sprites[2].enabled = false;
            sprites[1].enabled = true;
            Debug.Log("8");
            yield return new WaitForSeconds(delay);
            sprites[1].enabled = false;
            sprites[0].enabled = true;
            Debug.Log("9");
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.enabled = false;
        }

        coroutineTransform = marioTransform();
        coroutineDamaged = marioDamaged();
       
    }

    void Update()
    {
        if (isTransform)
        {
            StartCoroutine(coroutineTransform);
            isTransform = false;
        }

        if (isDamaged)
        {
            StartCoroutine(coroutineDamaged);
            isDamaged = false;
        }
    }
}
