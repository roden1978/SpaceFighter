using Microsoft.Xna.Framework;

public class Effect : Component, IPositionAdapter
{
    private Animator _animator;

    public EffectData EffectData {get; private set;} = new();
    public Vector2 Position
    {
        get => gameObject.Transform.Position;
        set => gameObject.Transform.Position = value;
    }

    public override void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _animator.NoLoopAnimationEnd += OnNoLoopAnimationEnd;
    }

    private void OnNoLoopAnimationEnd()
    {
        EffectData.ToPool = true;
    }

    public override void Destroy()
    {
        _animator.NoLoopAnimationEnd -= OnNoLoopAnimationEnd;
    }

    public void ResetData()
    {
        EffectData.ResetData();
    }
}

public class EffectData
{
    public bool ToPool = false;
    public EffectType EffectType = EffectType.Unknown;

    public void ResetData()
    {
        ToPool = false;
        EffectType = EffectType.Unknown;
    }
}