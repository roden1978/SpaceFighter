public class Pickeable : Component
{
    public PickeableData PickeableData { get; private set; } = new();
    public override void OnCollisionEnter(BoxCollider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out IPickeableCollector pickeableCollector))
        {
            PickeableData.IsPickedUp = true;
            pickeableCollector.PickUp(PickeableData);
            PickeableData.ToPool = true;
        }
        else
        {
            PickeableData.ToPool = true;
            PickeableData.IsDestroed = true;
        }
    }

    public void ResetData() => PickeableData.ResetData();
}
