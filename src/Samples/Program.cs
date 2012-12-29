using System;
using TempusReader;

namespace Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a discount name:");
            var name = Console.ReadLine();

            Console.WriteLine("Discount expires in:");
            var expires = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(expires))
                return;

            TimeSpan expirationDate = new Time(expires);
            var discount = new Discount(name, DateTime.Now.Add(expirationDate));

            Console.WriteLine(discount);

            Main(args);
        }
    }

    public class Discount
    {
        private readonly string _name;
        private readonly DateTime _expirationDate;

        public Discount(string name, DateTime expirationDate)
        {
            _name = name;
            _expirationDate = expirationDate;
        }

        public override string ToString()
        {
            return string.Format("Discount: ( Name = '{0}', Expires = '{1:yyyy-MM-dd HH:mm:ss.iii}' )",
                _name, _expirationDate);
        }
    }
}
