namespace Products.Core.Domain
{
    public class Product
    {
        private readonly Department _department;
        private readonly string _description;
        private readonly int _id;
        private readonly string _name;
        private readonly decimal _price;

        public Product(int id, string name, string description, decimal price, Department department)
        {
            _id = id;
            _name = name;
            _description = description;
            _price = price;
            _department = department;
        }

        public int Id
        {
            get { return _id; }
        }

        public string Name
        {
            get { return _name; }
        }

        public string Description
        {
            get { return _description; }
        }

        public decimal Price
        {
            get { return _price; }
        }

        public Department Department
        {
            get { return _department; }
        }
    }
}