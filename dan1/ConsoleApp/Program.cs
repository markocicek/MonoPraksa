namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Secretary secretary = new Secretary("Marko", "Cicek");
            Program checker = new Program();

            Console.WriteLine("Work:");
            secretary.Work();
            Console.WriteLine();

            secretary.AddCustomer(new Customer("Mirko", "Mirkovic"));
            secretary.AddCustomer(new Customer("Mario", "Maric"));
            secretary.AddCustomer(new Customer("Matej", "Matic"));
            secretary.AddCustomer(new Customer("Luka", "Lukic"));

            string input="";
            String[] inputLine = new String[] {};
            Console.WriteLine("For the list of commands use 'help'");
            while (input != "exit")
            {
                Console.Write("Query: ");
                input = Console.ReadLine();
                inputLine = input.Split(" ");

                switch(inputLine[0]) {
                    case "add":
                        if (checker.CheckInput(inputLine.Count()))
                        {
                            secretary.AddCustomer(new Customer(inputLine[1], inputLine[2]));
                        }
                        else
                        {
                            Console.WriteLine("Incorrect input");
                        }
                        break;
                    case "remove":
                        if (checker.CheckInput(inputLine.Count()))
                        {
                            secretary.RemoveCustomer(new Customer(inputLine[1], inputLine[2]));
                        }
                        else
                        {
                            Console.WriteLine("Incorrect input");
                        }
                        break;
                    case "search":
                        if (checker.CheckInput(inputLine.Count()))
                        {
                            secretary.Search(new Customer(inputLine[1], inputLine[2]));
                        }
                        else
                        {
                            Console.WriteLine("Incorrect input");
                        }
                        break;
                    case "customers":
                        {
                            secretary.ListCustomers();
                        }
                        break;
                    case "help":
                        Console.WriteLine("To add customers use add fname lname");
                        Console.WriteLine("To remove customers use remove fname lname");
                        Console.WriteLine("To search for customer use search fname lname");
                        Console.WriteLine("To list all customers use customers");
                        Console.WriteLine("In order to quit use exit");
                        Console.WriteLine("For the list of commands use 'help'");
                        break;
                    default: Console.WriteLine("nothing"); break;
                }
                Console.WriteLine();
            }
        }
        public bool CheckInput(int inputLine)
        {
            if (inputLine == 3)
            {
                return true;
            }
            return false;
        }
    }
}