using UnityEngine;

public class Hunger : MonoBehaviour, IAttribute, IRequireAttributeEventChannel {
    [SerializeField] float maxHunger = 100;
    [SerializeField] float hungerRate = 0.1f;

    AttributeValueChangedChannel _attributeValueChangedChannel;
    float _hunger;

    void Awake() {
        _hunger = maxHunger;
    }
    public void SetAttributeEventChannel(AttributeValueChangedChannel channel) {
        _attributeValueChangedChannel = channel;
        
        _attributeValueChangedChannel.Event += AdjustAttribute;
    }

    void Update() {
        float hungerLoss = hungerRate * Time.deltaTime;
        _attributeValueChangedChannel.SendEventMessage(_hunger - hungerLoss);
    }

    public void AdjustAttribute(float newAmount) {
        _hunger = Mathf.Clamp(newAmount, 0, maxHunger);
    }
}