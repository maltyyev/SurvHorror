using System.Collections;
using UnityEngine;

public class DoorInteractionController : BaseInteractionController
{
    [SerializeField] private GameObject _hinge;

    private bool _isOpen, _coroutineFinished = true;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public override void Interact()
    {
        if (_coroutineFinished)
        {
            _coroutineFinished = false;
            float angle = _isOpen ? -90f : 90f;
            StartCoroutine(Rotate(Vector3.up, angle));
        }
    }

    private IEnumerator Rotate(Vector3 axis, float angle, float duration = 1f)
    {
        _audioSource.Play();

        Quaternion from = _hinge.transform.rotation;
        Quaternion to = _hinge.transform.rotation * Quaternion.Euler(axis * angle);

        float elapsed = 0f;
        while (elapsed < duration)
        {
            _hinge.transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        _hinge.transform.rotation = to;
        _isOpen = !_isOpen;
        _coroutineFinished = true;
    }
}
