using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/AttributeValueChangedChannel")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "AttributeValueChangedChannel", message: "[Hunger] value changed", category: "Events", id: "4c536c7de60cc26ba38eef72ce24c2c2")]
public partial class AttributeValueChangedChannel : EventChannelBase
{
    public delegate void HungerChangedChannelEventHandler(float Hunger);
    public event HungerChangedChannelEventHandler Event; 

    public void SendEventMessage(float Hunger)
    {
        Event?.Invoke(Hunger);
    }

    public override void SendEventMessage(BlackboardVariable[] messageData)
    {
        BlackboardVariable<float> HungerBlackboardVariable = messageData[0] as BlackboardVariable<float>;
        var Hunger = HungerBlackboardVariable != null ? HungerBlackboardVariable.Value : default(float);

        Event?.Invoke(Hunger);
    }

    public override Delegate CreateEventHandler(BlackboardVariable[] vars, System.Action callback)
    {
        HungerChangedChannelEventHandler del = (Hunger) =>
        {
            BlackboardVariable<float> var0 = vars[0] as BlackboardVariable<float>;
            if(var0 != null)
                var0.Value = Hunger;

            callback();
        };
        return del;
    }

    public override void RegisterListener(Delegate del)
    {
        Event += del as HungerChangedChannelEventHandler;
    }

    public override void UnregisterListener(Delegate del)
    {
        Event -= del as HungerChangedChannelEventHandler;
    }
}

