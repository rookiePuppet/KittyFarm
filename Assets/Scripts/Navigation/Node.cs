namespace KittyFarm.NavigationSystem
{
    public class Node
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public float Priority { get; set; }

        public Node(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}