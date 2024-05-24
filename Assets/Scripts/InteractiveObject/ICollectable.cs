namespace KittyFarm.InteractiveObject
{
    public interface ICollectable
    {
        public bool CanBeCollected { get; }
        public void Collect();
    }
}