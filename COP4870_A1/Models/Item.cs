
namespace COP4870_A1.Models
{
    internal class Item
    {
        public int ID {  get; set; }
        public string? Name { get; set; }
        public string? Description {  get; set; }
        public double Price {  get; set; }
        public int Quantity {  get; set; }

        public override string ToString()
        {
            return $"[{ID}] {Name} - {Description}, Price: ${Price}, Quantity: {Quantity}";
                
        }
    }
}
