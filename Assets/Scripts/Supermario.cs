using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supermario : MonoBehaviour
{
    public bool isSuper = false;
    private bool super = false;
    private float duration;
    private float delay = 0.1f;
    private Animator anim;
    private IEnumerator coroutineTransform;

    // Start is called before the first frame update
    void Start()
    {
        duration = 0;
        anim = GetComponent<Animator>();
        coroutineTransform = superTransform();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSuper && super == false)
        {
            super = true;
            StartCoroutine(coroutineTransform);
            isSuper = false;
            
        }

        if (super == true)
        {
            duration += 1 * Time.deltaTime;
        }

        if (duration > 12)
        {
            super = false;
            duration = 0;
        }
    }

    IEnumerator superTransform()
    {

        while (super)
        {
            anim.SetLayerWeight(1, 1);
            yield return new WaitForSecondsRealtime(delay);
            anim.SetLayerWeight(2, 1);
            yield return new WaitForSecondsRealtime(delay);
            anim.SetLayerWeight(3, 1);
            yield return new WaitForSecondsRealtime(delay);
            anim.SetLayerWeight(4, 1);
            yield return new WaitForSecondsRealtime(delay);
            anim.SetLayerWeight(4, 0);
            yield return new WaitForSecondsRealtime(delay);
            anim.SetLayerWeight(3, 0);
            yield return new WaitForSecondsRealtime(delay);
            anim.SetLayerWeight(2, 0);
            yield return new WaitForSecondsRealtime(delay);
        }

        anim.SetLayerWeight(1, 0);
        StopCoroutine(coroutineTransform);
    }

    
}
