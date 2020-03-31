using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum MarioState
{
    Small,
    Big,
}

public class Mariotransform : MonoBehaviour
{
    private float delay = 0.1f;
    public bool isTransforming = false;
    public bool isDamaged = false;
    SpriteRenderer[] sprites;
    Transform[] transforms;
    Animator[] animators;
    private bool inTransform = false;
    private IEnumerator coroutineTransform;
    private IEnumerator coroutineDamaged;

    public SpriteRenderer CurrentSpriteRenderer;
    public Animator CurrentAnimator;

    public Action<SpriteRenderer, Animator, MarioState> OnTransform;

    // Start is called before the first frame update
    void Start()
    {

        sprites = GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.enabled = false;
        }

        transforms = GetComponentsInChildren<Transform>();
        animators = GetComponentsInChildren<Animator>();

        sprites[0].enabled = true;
        CurrentSpriteRenderer = sprites[0];
        //transforms[0].gameObject.GetComponent<BoxCollider2D>().enabled = true;

        coroutineTransform = marioTransform();
        coroutineDamaged = marioDamaged();
        OnTransform?.Invoke(sprites[0], animators[0], MarioState.Small);

        PlayerMovementController.OnPowerupPickup += (p, state) =>
        {
            if(p == Power.Mushroom && state == MarioState.Small)
                isTransforming = true;
        };
    }

    void Update()
    {
        if (isTransforming && !inTransform)
        {
            StartCoroutine(coroutineTransform);
            Time.timeScale = 0;
            isTransforming = false;
            inTransform = true;
            //transforms[1].GetComponent<BoxCollider2D>().enabled = false;
            //transforms[3].GetComponent<BoxCollider2D>().enabled = true;
        }

        if (isDamaged && inTransform)
        {
            StartCoroutine(coroutineDamaged);
            Time.timeScale = 0;
            isDamaged = false;
            inTransform = false;
            //transforms[3].GetComponent<BoxCollider2D>().enabled = false;
            //transforms[1].GetComponent<BoxCollider2D>().enabled = true;
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
        CurrentSpriteRenderer = sprites[2];
        OnTransform?.Invoke(CurrentSpriteRenderer, animators[1], MarioState.Big);
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
        CurrentSpriteRenderer = sprites[0];
        OnTransform?.Invoke(CurrentSpriteRenderer, animators[0], MarioState.Small);

        Time.timeScale = 1;
        StopCoroutine(coroutineDamaged);
    }
}
