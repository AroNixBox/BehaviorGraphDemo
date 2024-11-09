using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;

namespace BehaviorGraph {
    [RequireComponent(typeof(BehaviorGraphAgent))]
    public class NPCBootstrapper : MonoBehaviour{
        BehaviorGraphAgent _agent;
        [SerializeField] string hungerChannelName = "HungerChangedChannel";

        [RequireInterface(typeof(IRequireAttributeEventChannel))] [SerializeField]
        MonoBehaviour[] hungerRelatedChannels;
        
        void Awake() {
            _agent = GetComponent<BehaviorGraphAgent>();
        }

        void Start() {
            if (!_agent.BlackboardReference.GetVariable(hungerChannelName, out var hungerChannel)) {
                Debug.LogError($"{hungerChannelName} not found in blackboard");
                return;
            }
            if (hungerChannel.ObjectValue is not AttributeValueChangedChannel channel) {
                Debug.LogError($"{hungerChannelName} is not of type AttributeValueChangedChannel");
                return;
            }
            foreach (var channelComponent in hungerRelatedChannels) {
                if (channelComponent is not IRequireAttributeEventChannel requireAttributeEventChannel) {
                    Debug.LogError($"{channelComponent} does not implement IRequireAttributeEventChannel");
                    continue;
                }
                requireAttributeEventChannel.SetAttributeEventChannel(channel);
            } 
        }
    }
}