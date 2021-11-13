namespace Practice3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string[] rows = File.ReadAllLines("input.txt");

            // Part 1
            int treeCount = CountTrees(rows, 3, 1);
            Console.WriteLine("Part 1: Number of trees hit: " + treeCount);

            // Part 2
            int[] treeCounts = new int[5];
            treeCounts[0] = CountTrees(rows, 1, 1);
            treeCounts[1] = CountTrees(rows, 3, 1);
            treeCounts[2] = CountTrees(rows, 5, 1);
            treeCounts[3] = CountTrees(rows, 7, 1);
            treeCounts[4] = CountTrees(rows, 1, 2);

            long total = 1;
            foreach (int count in treeCounts)
            {
                total *= count;
            }

            Console.WriteLine("Part 2: Number of trees hit: " + total);
        }

        public static int CountTrees(string[] rows, int across, int down)
        {
            int col = 0;
            int finalCol = rows[0].Length - 1;

            int treeCount = 0;
            for (int i = 0; i < rows.Length; i += down)
            {
                if (rows[i][col] == '#') { treeCount++; }

                col += across;
                if (col > finalCol)
                {
                    col = col - finalCol - 1;
                }
            }

            return treeCount;
        }
    }
}