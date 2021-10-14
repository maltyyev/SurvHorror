using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutController : MonoBehaviour
{
    [SerializeField] private Image _fade;
    [SerializeField] private float _fadingSpeed = 1f;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        for (float i = 1f; i >= 0f; i -= Time.deltaTime * _fadingSpeed)
        {
            _fade.color = new Color(_fade.color.r, _fade.color.g, _fade.color.b, i);
            yield return null;
        }

        _fade.color = new Color(_fade.color.r, _fade.color.g, _fade.color.b, 0f);
    }
}
