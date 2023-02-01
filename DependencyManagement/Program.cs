namespace DependencyManager
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("<-- Dependency Management -->\n");

                var input = new string[,]
                {
                    { "t-shirt", "dress shirt" },  //dependency, item
                    { "dress shirt", "pants" },
                    { "dress shirt", "suit jacket" },
                    { "tie", "suit jacket" },
                    { "pants", "suit jacket" },
                    { "belt", "suit jacket" },
                    { "suit jacket", "overcoat" },
                    { "dress shirt", "tie" },
                    { "suit jacket", "sun glasses" },
                    { "sun glasses", "overcoat" },
                    { "left sock", "pants" },
                    { "pants", "belt" },
                    { "suit jacket", "left shoe" },
                    { "suit jacket", "right shoe" },
                    { "left shoe", "overcoat" },
                    { "right sock", "pants" },
                    { "right shoe", "overcoat" },
                    { "t-shirt", "suit jacket" }
                };

                var result = DependencyManagement.ResolveDependencies(input);

                Console.WriteLine(result);

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred. {ex.Message}");
            }
        }
    }
}