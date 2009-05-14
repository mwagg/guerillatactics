namespace Spike.Models
{
    public class Person
    {
        private readonly string _name;

        public Person(string name)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }
    }
}