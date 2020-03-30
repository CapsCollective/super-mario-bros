using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mariotransformtest : MonoBehaviour
{
    private float delay = 0.2f;
    public bool isTransform = false;
    public bool isDamaged = false;
    GameObject[] gm;
    private IEnumerator coroutineTransform;
    private IEnumerator coroutineDamaged;

    IEnumerator MarioTransformtest()
    {
        if (isTransform)
        {
            gm[0].SetActive(true);
            Debug.Log("1");
            yield return new WaitForSeconds(delay);
            gm[0].SetActive(false);
            gm[1].SetActive(true);
            Debug.Log("2");
            yield return new WaitForSeconds(delay);
            gm[1].SetActive(false);
            gm[0].SetActive(true);
            Debug.Log("3");
            yield return new WaitForSeconds(delay);
            gm[0].SetActive(false);
            gm[1].SetActive(true);
            Debug.Log("4");
            yield return new WaitForSeconds(delay);
            gm[1].SetActive(false);
            gm[0].SetActive(true);
            Debug.Log("5");
            yield return new WaitForSeconds(delay);
            gm[0].SetActive(false);
            gm[1].SetActive(true);
            Debug.Log("6");
            yield return new WaitForSeconds(delay);
            gm[1].SetActive(false);
            gm[2].SetActive(true);
            Debug.Log("7");
            yield return new WaitForSeconds(delay);
            gm[2].SetActive(false);
            gm[0].SetActive(true);
            Debug.Log("8");
            yield return new WaitForSeconds(delay);
            gm[0].SetActive(false);
            gm[1].SetActive(true);
            Debug.Log("9");
            yield return new WaitForSeconds(delay);
            gm[1].SetActive(false);
            gm[2].SetActive(true);
            Debug.Log("10");
        }

    }

    IEnumerator marioDamaged()
    {
        if (isTransform)
        {
            gm[2].SetActive(false);
            gm[1].SetActive(true);
            Debug.Log("1");
            yield return new WaitForSeconds(delay);
            gm[1].SetActive(false);
            gm[2].SetActive(true);
            Debug.Log("2");
            yield return new WaitForSeconds(delay);
            gm[2].SetActive(false);
            gm[1].SetActive(true);
            Debug.Log("3");
            yield return new WaitForSeconds(delay);
            gm[1].SetActive(false);
            gm[2].SetActive(true);
            Debug.Log("4");
            yield return new WaitForSeconds(delay);
            gm[2].SetActive(false);
            gm[1].SetActive(true);
            Debug.Log("5");
            yield return new WaitForSeconds(delay);
            gm[1].SetActive(false);
            gm[0].SetActive(true);
            Debug.Log("6");
            yield return new WaitForSeconds(delay);
            gm[0].SetActive(false);
            gm[2].SetActive(true);
            Debug.Log("7");
            yield return new WaitForSeconds(delay);
            gm[2].SetActive(false);
            gm[1].SetActive(true);
            Debug.Log("8");
            yield return new WaitForSeconds(delay);
            gm[1].SetActive(false);
            gm[0].SetActive(true);
            Debug.Log("9");
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        gm = GetComponentsInChildren<GameObject>();

        foreach (GameObject game in gm)
        {
            game.SetActive(false);
        }

        coroutineTransform = MarioTransformtest();
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
