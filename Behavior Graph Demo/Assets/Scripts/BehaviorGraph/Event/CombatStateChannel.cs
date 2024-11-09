using System;
using Unity.Behavior.GraphFramework;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/Combat State Channel")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "Combat State Channel", message: "[CombatState] has changed", category: "Events", id: "72b6ccb431ffbc19f36da12629aca187")]
public partial class CombatStateChannel : EventChannelBase
{
    public delegate void CombatStateChannelEventHandler(NPCCombatStates CombatState);
    public event CombatStateChannelEventHandler Event; 

    public void SendEventMessage(NPCCombatStates CombatState)
    {
        Event?.Invoke(CombatState);
    }

    public override void SendEventMessage(BlackboardVariable[] messageData)
    {
        BlackboardVariable<NPCCombatStates> CombatStateBlackboardVariable = messageData[0] as BlackboardVariable<NPCCombatStates>;
        var CombatState = CombatStateBlackboardVariable != null ? CombatStateBlackboardVariable.Value : default(NPCCombatStates);

        Event?.Invoke(CombatState);
    }

    public override Delegate CreateEventHandler(BlackboardVariable[] vars, System.Action callback)
    {
        CombatStateChannelEventHandler del = (CombatState) =>
        {
            BlackboardVariable<NPCCombatStates> var0 = vars[0] as BlackboardVariable<NPCCombatStates>;
            if(var0 != null)
                var0.Value = CombatState;

            callback();
        };
        return del;
    }

    public override void RegisterListener(Delegate del)
    {
        Event += del as CombatStateChannelEventHandler;
    }

    public override void UnregisterListener(Delegate del)
    {
        Event -= del as CombatStateChannelEventHandler;
    }
}

