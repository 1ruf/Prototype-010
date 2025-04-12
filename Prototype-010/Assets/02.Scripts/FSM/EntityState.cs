using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class EntityState
{
    protected Entity _entity;
    protected int _animatorHash;
    protected EntityAnimator _entityAnimator;
    protected EntityAnimationTrigger _animationTrigger;
    protected bool _isTriggerCall;

    public EntityState(Entity entity, int animationHash)
    {
        _entity = entity;
        _animatorHash = animationHash;
        _entityAnimator = entity.GetCompo<EntityAnimator>();
        _animationTrigger = entity.GetCompo<EntityAnimationTrigger>();
    }

    public virtual void Enter()
    {
        _entityAnimator.SetParam(_animatorHash, true);
        _isTriggerCall = false;
        _animationTrigger.OnAnimationEndTrigger += AnimationEndTrigger;
    }

    public virtual void Update() { }

    public virtual void Exit()
    {
        _entityAnimator.SetParam(_animatorHash, false);
        _animationTrigger.OnAnimationEndTrigger -= AnimationEndTrigger;
    }
    public virtual void AnimationEndTrigger() => _isTriggerCall = true;
}