public class TestCollectibleItem : BaseCollectibleItemController
{
    public override void Interact()
    {
        Destroy(_yellowArrow);
        gameObject.SetActive(false);
    }
}
