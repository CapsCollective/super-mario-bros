using UnityEngine;

public class BrickBroken : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(!(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f))
            Destroy(gameObject);
    }
}
