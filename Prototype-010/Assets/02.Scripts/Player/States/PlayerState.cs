using UnityEngine;

public abstract class PlayerState : EntityState
{
    protected Player _player;
    protected readonly float _inputThreshold;

    protected PlayerState(Entity entity, int animationHash) : base(entity, animationHash)
    {
        _player = entity as Player;
        UnityEngine.Debug.Assert(_player != null, message: $"Player state using only in player");
    }
}
