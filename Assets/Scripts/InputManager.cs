using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private DeviceConfiguration _deviceConfiguration;

    private Controls _controls;
    private bool _isRunning;

    private static InputManager s_instance;

    public string CurrentDeviceRaw { get; set; }
    public string CurrentControlScheme { get; set; }

    public static InputManager Instance
    {
        get
        {
            return s_instance;
        }
    }

    public Controls Controls
    {
        get
        {
            return _controls;
        }
    }

    private void Awake()
    {
        if (s_instance != null && s_instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            s_instance = this;
        }
        
        Cursor.visible = false;

        _controls = new Controls();
        _controls.Player.Run.started += OnRun;
        _controls.Player.Run.canceled += OnRun;
    }

    private void OnRun(InputAction.CallbackContext obj)
    {
        switch (obj.phase)
        {
            case InputActionPhase.Started:
                _isRunning = true;
                break;
            case InputActionPhase.Canceled:
                _isRunning = false;
                break;
            default:
                break;
        }
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    public Sprite GetIconForBinding(InputAction inputAction, out string currentBindingInput)
    {
        int controlBindingIndex = inputAction.GetBindingIndex(InputBinding.MaskByGroup(CurrentControlScheme));
        currentBindingInput = InputControlPath.ToHumanReadableString(inputAction.bindings[controlBindingIndex].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);

        return _deviceConfiguration.GetIconForBinding(CurrentDeviceRaw, currentBindingInput);
    }

    public Vector2 GetPlayerMovement() => _controls.Player.Movement.ReadValue<Vector2>();

    public Vector2 GetPlayerLook() => _controls.Player.Look.ReadValue<Vector2>();

    public bool PlayerJumpedThisFrame() => _controls.Player.Jump.triggered;

    public bool PlayerRunning() => _isRunning;
}
