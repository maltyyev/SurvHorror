using System.Collections;
using UnityEngine;

public class FadeOutController : MonoBehaviour
{
    [SerializeField] private float _waitForSeconds;

    private Animation _animation;

    private void Start()
    {
        _animation = GetComponent<Animation>();
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(_waitForSeconds);
        _animation.Play();
    }
}
