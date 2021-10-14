using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeviceConfiguration : MonoBehaviour
{
    [Serializable]
    private struct DeviceSet
    {
        public string deviceRawPath;
        public DeviceSettings deviceSettings;
    }

    [SerializeField] private List<DeviceSet> _devices;

    public Sprite GetIconForBinding(string currentDeviceRawPath, string currentBindingInput)
    {
        return _devices.Where(x => x.deviceRawPath == currentDeviceRawPath && x.deviceSettings.deviceHasContextIcons)
            .Select(device => GetDeviceIcon(device, currentBindingInput))
            .FirstOrDefault();
    }

    private Sprite GetDeviceIcon(DeviceSet device, string currentBindingInput)
    {
        Sprite icon = null;

        switch (currentBindingInput)
        {
            case "Button North":
                icon = device.deviceSettings.buttonNorthIcon;
                break;

            case "Button South":
                icon = device.deviceSettings.buttonSouthIcon;
                break;

            case "Button West":
                icon = device.deviceSettings.buttonWestIcon;
                break;

            case "Button East":
                icon = device.deviceSettings.buttonEastIcon;
                break;

            case "Right Shoulder":
                icon = device.deviceSettings.triggerRightFrontIcon;
                break;

            case "Right Trigger":
                icon = device.deviceSettings.triggerRightBackIcon;
                break;

            case "rightTriggerButton":
                icon = device.deviceSettings.triggerRightBackIcon;
                break;

            case "Left Shoulder":
                icon = device.deviceSettings.triggerLeftFrontIcon;
                break;

            case "Left Trigger":
                icon = device.deviceSettings.triggerLeftBackIcon;
                break;

            case "leftTriggerButton":
                icon = device.deviceSettings.triggerLeftBackIcon;
                break;

            default:

                for (int i = 0; i < device.deviceSettings.customContextIcons.Count; i++)
                {
                    if (device.deviceSettings.customContextIcons[i].customInputContextString == currentBindingInput)
                    {
                        if (device.deviceSettings.customContextIcons[i].customInputContextIcon != null)
                        {
                            icon = device.deviceSettings.customContextIcons[i].customInputContextIcon;
                        }
                    }
                }


                break;

        }

        return icon;
    }
}
