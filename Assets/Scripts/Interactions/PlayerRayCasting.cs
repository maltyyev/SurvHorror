using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRayCasting : MonoBehaviour
{
    [SerializeField] private GameObject _panels;
    [SerializeField] private TextMeshProUGUI _bindingNameDisplayText;
    [SerializeField] private Image _bindingIconDisplayImage;

    #region Raycasting variables

    [SerializeField] private float _distance;
    [SerializeField] private LayerMask _layerMask;

    private RaycastHit[] _raycastHits = new RaycastHit[1];
    private int _raycastHitsCount;
    private BaseInteractionController _controller;

    #endregion

    protected InputManager _inputManager;

    private void Start()
    {
        _inputManager = InputManager.Instance;
        _inputManager.Controls.Player.Interaction.started += Interact;
    }

    private void FixedUpdate()
    {
        _raycastHitsCount = Physics.RaycastNonAlloc(transform.position, transform.TransformDirection(Vector3.forward), _raycastHits, _distance, _layerMask);
        if (_raycastHitsCount > 0)
        {
            Sprite buttonIcon = _inputManager.GetIconForBinding(_inputManager.Controls.Player.Interaction, out string currentBindingInput);

            if (buttonIcon)
            {
                _bindingIconDisplayImage.sprite = buttonIcon;
                _bindingIconDisplayImage.gameObject.SetActive(true);
                _bindingNameDisplayText.gameObject.SetActive(false);
            }
            else
            {
                _bindingNameDisplayText.SetText(currentBindingInput);
                _bindingNameDisplayText.gameObject.SetActive(true);
                _bindingIconDisplayImage.gameObject.SetActive(false);
            }

            _panels.SetActive(true);
        }
        else
        {
            _panels.SetActive(false);
            _bindingIconDisplayImage.gameObject.SetActive(false);
            _bindingNameDisplayText.gameObject.SetActive(false);
        }
    }

    private void Interact(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (_raycastHitsCount > 0)
        {
            var _controller = _raycastHits[0].transform.gameObject.GetComponent<BaseInteractionController>();
            if (_controller)
                _controller.Interact();
        }
    }
}
