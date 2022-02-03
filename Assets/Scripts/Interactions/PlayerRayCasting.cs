using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRayCasting : MonoBehaviour
{
    [System.Serializable]
    public struct Crosshair
    {
        public Sprite StandardCrosshair;
        public Sprite InteractionCrosshair;
        public Sprite WeaponCrosshair;
    }

    [SerializeField] private GameObject _panels;
    [SerializeField] private TextMeshProUGUI _bindingNameDisplayText;
    [SerializeField] private Image _bindingIconDisplayImage;

    [SerializeField] private Crosshair _crosshairSettings;
    [SerializeField] private Image _crosshairImage;

    #region Raycasting variables

    [SerializeField] private float _distance;
    [SerializeField] private LayerMask _layerMask;

    private RaycastHit[] _raycastHits = new RaycastHit[1];
    private int _raycastHitsCount;
    private Sprite _buttonIcon;

    #endregion

    protected InputManager _inputManager;

    private void Start()
    {
        _inputManager = InputManager.Instance;
        _inputManager.Controls.Player.Interaction.started += Interact;
    }

    private void FixedUpdate()
    {
        _raycastHitsCount = Physics.RaycastNonAlloc(Camera.main.transform.position, Camera.main.transform.forward, _raycastHits, _distance, _layerMask);
        
        if (_raycastHitsCount > 0)
        {
            _crosshairImage.sprite = _crosshairSettings.InteractionCrosshair;

            _buttonIcon = _inputManager.GetIconForBinding(_inputManager.Controls.Player.Interaction, out string currentBindingInput);

            if (_buttonIcon)
            {
                _bindingIconDisplayImage.sprite = _buttonIcon;
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
            _crosshairImage.sprite = _crosshairSettings.StandardCrosshair;

            _panels.SetActive(false);
            _bindingIconDisplayImage.gameObject.SetActive(false);
            _bindingNameDisplayText.gameObject.SetActive(false);

            Array.Clear(_raycastHits, 0, _raycastHits.Length);
        }
    }

    private void Interact(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log(Camera.main.transform.forward);

        if (_raycastHitsCount > 0)
        {
            var _controller = _raycastHits[0].transform.gameObject.GetComponent<BaseInteractionController>();
            if (_controller)
                _controller.Interact();
        }
    }
}
