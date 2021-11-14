namespace Practice5
{   
    public class Program
    {
        public static void Main(string[] args)
        {
            string[] strings = File.ReadAllLines("input.txt");

            List<Seat> seats = new List<Seat>();

            foreach (string s in strings)
            {
                seats.Add(new Seat(s));
            }

            // Part 1
            int max = 0;
            int val;
            foreach (Seat seat in seats)
            {
                val = seat.GetSeatID();
                if (val > max) {  max = val; }
            }
            Console.WriteLine("Part 1: Max ID = " + max);

            // Part 2
            List<Seat> orderedSeats = SortSeats(seats);
            int startingID = orderedSeats[0].GetSeatID();
            int nextID = 0; // Must be assigned to base value so it can compile
            for (int i = startingID; i < orderedSeats.Count - startingID; i++)
            {
                nextID = orderedSeats[i].GetSeatID() + 1;
                if (orderedSeats[i + 1].GetSeatID() != nextID)
                {
                    break;
                }
            }
            Console.WriteLine("Part 2: Your seat ID is " + nextID);
        }

        public static List<Seat> SortSeats(List<Seat> unorderedSeats)
        {
            return unorderedSeats.OrderBy(o => o.GetSeatID()).ToList();
        }
    }

    public class Seat
    {
        public string passString { get; set; }
        public int row { get; set; }
        public int column { get; set; }

        public Seat(string passString)
        {
            this.passString = passString;
            GetRow();
            GetColumn();
        }

        private void GetRow()
        {
            string rowBinary = "";
            for (int i = 0; i < 7; i++)
            {
                switch (passString[i])
                {
                    case 'F':
                        rowBinary += '0';
                        break;
                    case 'B':
                        rowBinary += '1';
                        break;
                }
            }
            row = Convert.ToInt32(rowBinary, 2);
        }

        private void GetColumn()
        {
            string colBinary = "";
            for (int i = 7; i < passString.Length; i++)
            {
                switch (passString[i])
                {
                    case 'L':
                        colBinary += '0';
                        break;
                    case 'R':
                        colBinary += '1';
                        break;
                }
            }
            column = Convert.ToInt32(colBinary, 2);
        }

        public int GetSeatID()
        {
            return (row * 8) + column;
        }
    }
}