using System.Collections.Generic;
using UnityEngine;

public class DeviceSettings : MonoBehaviour
{
    [System.Serializable]
    public struct CustomInputContextIcon
    {
        public string customInputContextString;
        public Sprite customInputContextIcon;
    }

    public bool deviceHasContextIcons;

    public Sprite buttonNorthIcon;
    public Sprite buttonSouthIcon;
    public Sprite buttonWestIcon;
    public Sprite buttonEastIcon;

    public Sprite triggerRightFrontIcon;
    public Sprite triggerRightBackIcon;
    public Sprite triggerLeftFrontIcon;
    public Sprite triggerLeftBackIcon;

    public List<CustomInputContextIcon> customContextIcons = new List<CustomInputContextIcon>();
}
