using KittyFarm.NavigationSystem;
using UnityEngine;

namespace KittyFarm
{
    public class NoneUsable : IUsableItem
    {
        private readonly NavAgent agent;

        public NoneUsable(NavAgent agent)
        {
            this.agent = agent;
        }

        public void Use(Vector3 worldPosition, Vector3Int cellPosition)
        {
            agent.GoToDestination(cellPosition);
        }
    }
}