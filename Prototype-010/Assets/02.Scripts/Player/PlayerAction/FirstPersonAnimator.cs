using UnityEngine;

public class FirstPersonAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetParam(int hash, float value) => _animator.SetFloat(hash, value);
    public void SetParam(int hash, bool value) => _animator.SetBool(hash, value);
    public void SetParam(int hash, int value) => _animator.SetInteger(hash, value);
    public void SetParam(int hash) => _animator.SetTrigger(hash);


    public void SetParam(string name, float value) => _animator.SetFloat(name, value);
    public void SetParam(string name, bool value) => _animator.SetBool(name, value);
    public void SetParam(string name, int value) => _animator.SetInteger(name, value);
    public void SetParam(string name) => _animator.SetTrigger(name);
}
