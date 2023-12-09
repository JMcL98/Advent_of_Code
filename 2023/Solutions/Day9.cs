namespace _2023.Solutions;

public static class Day9
{
    public static string Part1(List<string> input)
    {
        var sequences = input.Select(line => new Sequence(line.Split(' ').Select(num => int.Parse(num)).ToList())).ToList();
        return sequences.Sum(seq => seq.GetNextValueInSequence()).ToString();
    }

    public static string Part2(List<string> input)
    {
        var sequences = input.Select(line => new Sequence(line.Split(' ').Select(num => int.Parse(num)).ToList())).ToList();
        return sequences.Sum(seq => seq.GetReverseNextValueInSequence()).ToString();
    }

    private class Sequence
    {
        private Sequence? _derivedSequence;
        private List<int> _sequence;

        internal Sequence(List<int> sequence)
        {
            _sequence = sequence;
            PopulateDerivedSequence();
        }

        private void PopulateDerivedSequence()
        {
            if (_sequence.Any(x => x != 0))
            {
                var newSeq = new List<int>();
                for (var i = 1; i < _sequence.Count; i++)
                {
                    newSeq.Add(_sequence[i] - _sequence[i - 1]);
                }

                _derivedSequence = new Sequence(newSeq);
            }
            else
            {
                _derivedSequence = null;
            }
        }

        internal int GetNextValueInSequence()
        {
            if (_derivedSequence is null)
            {
                return _sequence[^1];
            }

            return _sequence[^1] + _derivedSequence.GetNextValueInSequence();
        }

        internal int GetReverseNextValueInSequence()
        {
            if (_derivedSequence is null)
            {
                return _sequence[0];
            }

            return _sequence[0] - _derivedSequence.GetReverseNextValueInSequence();
        }
    }
}