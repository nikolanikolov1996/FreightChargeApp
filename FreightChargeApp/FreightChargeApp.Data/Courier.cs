namespace FreightChargeApp.Data
{
    public class Courier
    {
        public Courier(string name)
            => Name = name;

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
