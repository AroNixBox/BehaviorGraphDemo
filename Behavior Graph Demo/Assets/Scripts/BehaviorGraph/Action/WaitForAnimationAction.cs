using System;
using System.Threading.Tasks;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Wait for Animation", story: "Wait for Current Animation on [Animator]", category: "Action/Animation", id: "32e87b387a3b28dd2253ec4999039ba1")]
public partial class WaitForAnimationAction : Action
{
    [SerializeReference] public BlackboardVariable<Animator> Animator;
    float _waitTime;
    protected override Status OnStart()
    {
        if (ReferenceEquals(Animator, null)) {
            Debug.LogError("Animator is not set");
            return Status.Failure;
        }

        _waitTime = GetCurrentOrNextClipLength(0);

        if (_waitTime > 0) {
            return Status.Running;
        }

        Debug.LogWarning("No clip found or Clip has no keyframes.");
        return Status.Failure;
    }

    float GetCurrentOrNextClipLength(int layerIndex) {
        if (Animator.Value.IsInTransition(layerIndex)) {
            // If Blendtree, returns average length of all clips in the blendtree
            var nextStateInfo = Animator.Value.GetNextAnimatorStateInfo(layerIndex);
            return nextStateInfo.length;
        }

        var currentClipInfo = Animator.Value.GetCurrentAnimatorClipInfo(layerIndex);
        return currentClipInfo.Length > 0 ? currentClipInfo[0].clip.length : 0f;
    }


    protected override Status OnUpdate() {
        _waitTime -= Time.deltaTime;
        return _waitTime <= 0 ? Status.Success : Status.Running;
    } 

    protected override void OnEnd()
    {
    }
}

