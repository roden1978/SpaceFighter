using Microsoft.Xna.Framework;


public abstract class Enemy : Component, IPositionAdapter, IDamageable
{
    public EnemyData EnemyData {get; set;} = new();
    public Vector2 Position { get => gameObject.Transform.Position; set => gameObject.Transform.Position = value; }

    public abstract void TakeDamage(float damage);

    public virtual void ResetData() => EnemyData.ResetData();

    public override void OnCollisionEnter(BoxCollider2D collider)
    {
        if(collider.gameObject.TryGetComponent(out IDamageable enemy))
            enemy.TakeDamage(EnemyData.Damage);
    }
}
