using System;
using UnityEngine;

[Serializable]
public class MouseLook
{
    public float XSensitivity = 2f;
    public float YSensitivity = 2f;
    public bool clampVerticalRotation = true;
    public float MinimumX = -90F;
    public float MaximumX = 90F;
    public bool smooth;
    public float smoothTime = 5f;
    public bool lockCursor = true;

    private Quaternion m_CharacterTargetRot;
    private Quaternion m_CameraTargetRot;
    private InputManager m_inputManager;

    #region Rotation variables

    Vector2 _cameraMovement;
    float _xRot, _yRot;

    #endregion

    #region Clamp rotation variables

    float _angleX;

    #endregion

    public void Init(Transform character, Transform camera)
    {
        m_CharacterTargetRot = character.localRotation;
        m_CameraTargetRot = camera.localRotation;

        m_inputManager = InputManager.Instance;
    }

    public void LookRotation(Transform character, Transform camera)
    {
        _cameraMovement = m_inputManager.GetPlayerLook();

        _yRot = _cameraMovement.x * XSensitivity;
        _xRot = _cameraMovement.y * YSensitivity;

        m_CharacterTargetRot *= Quaternion.Euler(0f, _yRot, 0f);
        m_CameraTargetRot *= Quaternion.Euler(-_xRot, 0f, 0f);

        if (clampVerticalRotation)
            m_CameraTargetRot = ClampRotationAroundXAxis(m_CameraTargetRot);

        if (smooth)
        {
            character.localRotation = Quaternion.Slerp(character.localRotation, m_CharacterTargetRot,
                smoothTime * Time.deltaTime);
            camera.localRotation = Quaternion.Slerp(camera.localRotation, m_CameraTargetRot,
                smoothTime * Time.deltaTime);
        }
        else
        {
            character.localRotation = m_CharacterTargetRot;
            camera.localRotation = m_CameraTargetRot;
        }
    }

    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        _angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        _angleX = Mathf.Clamp(_angleX, MinimumX, MaximumX);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * _angleX);

        return q;
    }

}
