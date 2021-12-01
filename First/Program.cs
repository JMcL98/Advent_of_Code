namespace First
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Initialize Variables
            var strings = File.ReadAllLines("input.txt");
            var depths = new int[strings.Count()];
            
            for (var i = 0; i < strings.Length; i++)
            {
                depths[i] = int.Parse(strings[i]);
            }

            // Part 1
            var count = 0;
            for (var i = 1; i < depths.Length; i++)
            {
                if (depths[i] > depths[i - 1])
                {
                    count++;
                }
            }
            Console.WriteLine("Part 1: " + count);

            // Part 2
            count = 0;
            for (var i = 3; i < depths.Length; i++)
            {
                var sizeA = depths[i - 3] + depths[i - 2] + depths[i - 1];
                var sizeB = depths[i - 2] + depths[i - 1] + depths[i];
                if (sizeB > sizeA)
                {
                    count++;
                }
            }
            Console.WriteLine("Part 2: " + count);
        }
    }
}
