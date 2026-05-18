public class ShieldData
{
    public float Health = 3;
}
public sealed class Shield : Component, IDamageable
{
    public ShieldData ShieldData {get; private set;} = new();
    public Shield()
    {
    }
    public override void OnCollisionEnter(BoxCollider2D collider)
    {
        if(collider.gameObject.TryGetComponent(out IDamageable damageable))
            damageable.TakeDamage(10);
    }

    public void TakeDamage(float damage = 0)
    {
        ShieldData.Health -= damage;
    }

    public void ResetData()
    {
        ShieldData = new();
    }
}