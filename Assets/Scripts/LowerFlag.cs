using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerFlag : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;
    public Transform endTrans;

    public float timeTaken = 1f;
    private LevelCompleteManager LMC;

    private void Awake()
    {
        startPosition = gameObject.transform.position;
        endPosition = endTrans.position;
        LMC = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelCompleteManager>();
    }

    public void Lower()
    {
        SoundGuy.Instance.PlaySound("", true);
        SoundGuy.Instance.PlaySound("smb_flagpole");

        StartCoroutine(LerpFlag());
    }

    public IEnumerator LerpFlag()
    {
        float fraction = 0;
        while(fraction<1)
        {
            fraction += Time.deltaTime/ timeTaken;
            transform.position = Vector3.Lerp(startPosition, endPosition, fraction);
            
            yield return null;
        }
        LMC.CastleEnter();
    }
}
