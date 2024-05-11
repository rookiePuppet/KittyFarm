namespace KittyFarm.Harvestable
{
    public interface IHarvestable
    {
        public bool CanBeHarvested { get; }
        public void Harvest();
    }
}