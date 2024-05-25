namespace KittyFarm.InteractiveObject
{
    public interface IChopped
    {
        public void OnChopped();
        public bool CanBeChopped { get; }
    }
}