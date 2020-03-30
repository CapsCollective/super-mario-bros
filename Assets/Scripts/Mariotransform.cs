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

        coroutineTransform = marioTransform();
        coroutineDamaged = marioDamaged();
       
    }

    void Update()
    {
        if (isTransforming && !inTransform)
        {
            StartCoroutine(coroutineTransform);
            isTransforming = false;
            inTransform = true;
            transforms[1].GetComponent<BoxCollider2D>().enabled = false;
            transforms[3].GetComponent<BoxCollider2D>().enabled = true;
        }

        if (isDamaged && inTransform)
        {
            StartCoroutine(coroutineDamaged);
            isDamaged = false;
            inTransform = false;
            transforms[3].GetComponent<BoxCollider2D>().enabled = false;
            transforms[1].GetComponent<BoxCollider2D>().enabled = true;
        }
    }


    IEnumerator marioTransform()
    {
        sprites[0].enabled = true;
        yield return new WaitForSeconds(delay);
        sprites[0].enabled = false;
        sprites[1].enabled = true;
        yield return new WaitForSeconds(delay);
        sprites[1].enabled = false;
        sprites[0].enabled = true;
        yield return new WaitForSeconds(delay);
        sprites[0].enabled = false;
        sprites[1].enabled = true;
        yield return new WaitForSeconds(delay);
        sprites[1].enabled = false;
        sprites[0].enabled = true;
        yield return new WaitForSeconds(delay);
        sprites[0].enabled = false;
        sprites[1].enabled = true;
        yield return new WaitForSeconds(delay);
        sprites[1].enabled = false;
        sprites[2].enabled = true;
        yield return new WaitForSeconds(delay);
        sprites[2].enabled = false;
        sprites[0].enabled = true;
        yield return new WaitForSeconds(delay);
        sprites[0].enabled = false;
        sprites[1].enabled = true;
        yield return new WaitForSeconds(delay);
        sprites[1].enabled = false;
        sprites[2].enabled = true;
        StopCoroutine(coroutineTransform);
    }

    IEnumerator marioDamaged()
    {
        sprites[2].enabled = false;
        sprites[1].enabled = true;
        yield return new WaitForSeconds(delay);
        sprites[1].enabled = false;
        sprites[2].enabled = true;
        yield return new WaitForSeconds(delay);
        sprites[2].enabled = false;
        sprites[1].enabled = true;
        yield return new WaitForSeconds(delay);
        sprites[1].enabled = false;
        sprites[2].enabled = true;
        yield return new WaitForSeconds(delay);
        sprites[2].enabled = false;
        sprites[1].enabled = true;
        yield return new WaitForSeconds(delay);
        sprites[1].enabled = false;
        sprites[0].enabled = true;
        yield return new WaitForSeconds(delay);
        sprites[0].enabled = false;
        sprites[2].enabled = true;
        yield return new WaitForSeconds(delay);
        sprites[2].enabled = false;
        sprites[1].enabled = true;
        yield return new WaitForSeconds(delay);
        sprites[1].enabled = false;
        sprites[0].enabled = true;
        StopCoroutine(coroutineDamaged);
    }
}
