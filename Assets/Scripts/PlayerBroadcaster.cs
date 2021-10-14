using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBroadcaster : MonoBehaviour
{
    private PlayerInput _playerInput;

    private InputManager _inputManager;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        _inputManager = InputManager.Instance;
        SwitchToCurrentDevice();
    }

    public void OnControlsChanged()
    {
        SwitchToCurrentDevice();
    }

    private void SwitchToCurrentDevice()
    {
        _inputManager.CurrentDeviceRaw = _playerInput.devices[0].ToString();
        _inputManager.CurrentControlScheme = _playerInput.currentControlScheme;
    }
}
