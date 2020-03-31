using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerPlayer : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;
    public Transform endTrans;

    public float timeTaken = 1f;

    public void Lower()
    {
        startPosition = gameObject.transform.position;
        endPosition = new Vector3(startPosition.x, endTrans.position.y, startPosition.z);
        StartCoroutine(LerpPlayer());
    }

    public IEnumerator LerpPlayer()
    {
        float fraction = 0;
        while (fraction < 1)
        {
            fraction += Time.deltaTime / timeTaken;
            transform.position = Vector3.Lerp(startPosition, endPosition, fraction);

            yield return null;
        }
    }
}
