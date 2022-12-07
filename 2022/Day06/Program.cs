namespace Day06;

public static class Program
{
    public static void Main()
    {
        var signal = File.ReadAllLines("input.txt")[0];

        var packetBuffer = new char[4];
        for (var i = 3; i < signal.Length; i++)
        {
            packetBuffer[0] = signal[i - 3];
            packetBuffer[1] = signal[i - 2];
            packetBuffer[2] = signal[i - 1];
            packetBuffer[3] = signal[i];
            
            if (packetBuffer.Distinct().Count() == 4)
            {
                Console.WriteLine("Part 1: {0}", ++i);
                break;
            }
        }

        for (var i = 0; i < signal.Length - 13; i++)
        {
            var messageBuffer = signal.Substring(i, 14);
            if (messageBuffer.Distinct().Count() == 14)
            {
                i += 14;
                Console.WriteLine("Part 2: {0}", i);
                break;
            }
        }
    }
}