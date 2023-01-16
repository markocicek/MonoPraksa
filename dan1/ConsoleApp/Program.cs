namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Secretary secretary = new Secretary("Marko", "Cicek");

            Console.WriteLine("Work:");
            secretary.Work();
            Console.WriteLine();

            secretary.AddCustomer("Mirko", "Mirkovic");
            secretary.AddCustomer("Mario", "Maric");
            secretary.AddCustomer("Matej", "Matic");
            secretary.AddCustomer("Luka", "Lukic");

            string input="";
            String[] inputLine = new String[] {};
            Console.WriteLine("For the list of commands use 'help'");
            while (input != "exit")
            {
                Console.Write("Add query: ");
                input = Console.ReadLine();
                inputLine = input.Split(" ");

                switch(inputLine[0]) {
                    case "add":
                        if (inputLine.Count()==3)
                        {
                            secretary.AddCustomer(inputLine[1], inputLine[2]);
                        }
                        else
                        {
                            Console.WriteLine("Incorrect input");
                        }
                        break;
                    case "remove":
                        if (inputLine.Count()==3)
                        {
                            secretary.RemoveCustomer(inputLine[1], inputLine[2]);
                        }
                        else
                        {
                            Console.WriteLine("Incorrect input");
                        }
                        break;
                    case "search":
                        if (inputLine.Count()==3)
                        {
                            secretary.Search(inputLine[1], inputLine[2]);
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
    }
}