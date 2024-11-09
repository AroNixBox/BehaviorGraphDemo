using System;
using UnityEngine;
using UnityEngine.UI;

public class AttributeBar : MonoBehaviour, IRequireAttributeEventChannel
{   
    [SerializeField] Slider attributeBar;
    bool _isInitialized;
    public void SetAttributeEventChannel(AttributeValueChangedChannel channel) {
        channel.Event += SetAttributeBar;
    }
    void SetAttributeBar(float value) {
        if(!_isInitialized) {
            attributeBar.maxValue = value;
            _isInitialized = true;
        }
        attributeBar.value = value;
    }
}
