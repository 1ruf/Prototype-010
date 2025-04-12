using System;
using UnityEngine;

public class EntityAnimationTrigger : MonoBehaviour, IEntityComponent
{
    public Action OnAnimationEndTrigger;
    public event Action<bool> OnRollingStatusChange;
    public event Action OnAttackVFXTrigger;

    private Entity _entity;

    public void Initialize(Entity entity)
    {
        _entity = entity;
    }

    private void AnimationEnd()
    {
        OnAnimationEndTrigger?.Invoke();
    }

    private void RollingStart() => OnRollingStatusChange?.Invoke(true);
    private void RollingEnd() => OnRollingStatusChange?.Invoke(false);
    private void PlayAttackVFX() => OnAttackVFXTrigger?.Invoke();
}
