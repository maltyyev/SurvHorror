using System.Collections;
using UnityEngine;

public class FlameAnimationController : MonoBehaviour
{
    private Animator _animator;
    private AnimationClip[] _animationClips;

    #region PlayRandomAnimation() Coroutine variables

    private int _randomAnimationNumber;
    private AnimationClip _currentRandomAnimation;

    #endregion

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animationClips = _animator.runtimeAnimatorController.animationClips;

        StartCoroutine(PlayRandomAnimation());
    }

    private IEnumerator PlayRandomAnimation()
    {
        while (true)
        {
            _randomAnimationNumber = Random.Range(0, _animationClips.Length);
            _currentRandomAnimation = _animationClips[_randomAnimationNumber];
            _animator.Play(_currentRandomAnimation.name);

            yield return new WaitForSeconds(_currentRandomAnimation.length);
        }
    }
}
