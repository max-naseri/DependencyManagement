namespace DependencyManager
{
    public class DependencyManagement
    {
        public static string ResolveDependencies(string[,] input)
        {
            if (input == null)
                throw new ArgumentNullException();

            //dictionary<dependent_item, its_dependencies>
            //hashset<all_items>
            (Dictionary<string, List<string>> dependencies, HashSet<string> items) = GetDependenciesAndItems(input);

            var processedItems = new HashSet<string>();
            var result = new List<string>();

            while (items.Count > 0)
            {
                var independentItems = GetIndependentItems(dependencies, items, processedItems);

                if (!independentItems.Any())
                    throw new Exception("Cyclic Dependency!!!");

                result.Add(string.Join(",", independentItems));

                foreach (var item in independentItems)
                {
                    items.Remove(item);
                    processedItems.Add(item);
                }
            }

            return string.Join("\n", result);
        }

        private static (Dictionary<string, List<string>>, HashSet<string>) GetDependenciesAndItems(string[,] input)
        {
            var dependencies = new Dictionary<string, List<string>>();
            var items = new HashSet<string>();

            for (int i = 0; i < input.GetLength(0); i++)
            {
                var independentItem = input[i, 0];
                var dependentItem = input[i, 1];

                dependencies[dependentItem] = dependencies.GetValueOrDefault(dependentItem, new List<string>());
                dependencies[dependentItem].Add(independentItem);

                items.Add(independentItem);
                items.Add(dependentItem);
            }

            return (dependencies, items);
        }

        private static List<string> GetIndependentItems(
          Dictionary<string, List<string>> dependencies,
          HashSet<string> items,
          HashSet<string> processedItems
          )
        {
            /* 
            is item independent (0 dependency)?
            Or
            have all the dependencies of the item been already processed?
            */
            return items
                    .Where(item => !dependencies.ContainsKey(item) || dependencies[item].All(x => processedItems.Contains(x)))
                    .OrderBy(item => item)
                    .ToList();
        }
    }
}