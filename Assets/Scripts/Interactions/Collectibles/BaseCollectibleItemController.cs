using UnityEngine;

public abstract class BaseCollectibleItemController : BaseInteractionController
{
    [SerializeField] private GameObject _yellowArrowPrefab;

    protected GameObject _yellowArrow;

    private void Awake()
    {
        _yellowArrow = Instantiate(_yellowArrowPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1f, gameObject.transform.position.z), Quaternion.Euler(-90f, 0f, 0f));
    }

    private void FixedUpdate()
    {
        _yellowArrow.transform.Rotate(Vector3.forward, 1f);
    }
}
