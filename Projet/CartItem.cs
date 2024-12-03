namespace Projet
{
    public class CartItem
    {
        public string Designation { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Photo { get; set; }  

        public CartItem(string designation, double price, int quantity, string photo = "")
        {
            Designation = designation;
            Price = price;
            Quantity = quantity;
            Photo = photo;
        }
    }
}
