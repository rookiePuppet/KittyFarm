namespace KittyFarm.CropSystem
{
    public interface IHarvestable
    {
        public bool CanBeHarvested { get; }
        public void Harvest();
    }
}