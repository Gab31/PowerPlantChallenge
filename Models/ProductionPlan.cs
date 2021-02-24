namespace PowerPlantChallenge
{
    public class ProductionPlan
    {
        public ProductionPlan(string name, int power)
        {
            Name = name;
            Power = power;
        }

        public int Power { get; set; }
        public string Name { get; set; }
    }
}
