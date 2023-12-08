namespace _2023.Solutions;

public static class Day7
{
    public static string Part1(List<string> input)
    {
        // RIP

        return "";
    }

    public static string Part2(List<string> input)
    {
        var hands = new List<Hand>();
        foreach (var line in input)
        {
            hands.Add(new Hand(line.Split(' ')[0], line.Split(' ')[1]));
        }

        var allHands = new AllHands(hands);

        return allHands.GetTotalValue().ToString();
    }

    private enum Card
    {
        Joker,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Queen,
        King,
        Ace
    }

    private class AllHands
    {
        private List<Hand> _allHands;

        // Split into hand type
        private List<Hand> _highCardHands;
        private List<Hand> _onePairHands;
        private List<Hand> _twoPairHands;
        private List<Hand> _threeOfAKindHands;
        private List<Hand> _fullHouseHands;
        private List<Hand> _fourOfAKindHands;
        private List<Hand> _fiveOfAKindHands;

        internal AllHands(List<Hand> hands)
        {
            _allHands = hands;
            SplitHandsIntoType();
            JokerProcessing();
            OrderSplitHands();
        }

        private void SplitHandsIntoType()
        {
            _highCardHands = new List<Hand>();
            _onePairHands = new List<Hand>();
            _twoPairHands = new List<Hand>();
            _threeOfAKindHands = new List<Hand>();
            _fullHouseHands = new List<Hand>();
            _fourOfAKindHands = new List<Hand>();
            _fiveOfAKindHands = new List<Hand>();

            // Five of a kind
            foreach (var hand in _allHands)
            {
                if (hand.Cards.All(x => x == hand.Cards[0]))
                {
                    _fiveOfAKindHands.Add(hand);
                }
            }
            _allHands.RemoveAll(hand => _fiveOfAKindHands.Contains(hand));

            // Four of a kind
            foreach (var hand in _allHands)
            {
                var groupedCards = hand.Cards.GroupBy(x => x).ToList();
                if (groupedCards.Any(x => x.Count() == 4))
                {
                    _fourOfAKindHands.Add(hand);
                }
            }
            _allHands.RemoveAll(hand => _fourOfAKindHands.Contains(hand));

            // Full house
            foreach (var hand in _allHands)
            {
                var groupedCards = hand.Cards.GroupBy(x => x).ToList();
                if (groupedCards.Any(x => x.Count() == 3) && groupedCards.Any(x => x.Count() == 2))
                {
                    _fullHouseHands.Add(hand);
                }
            }
            _allHands.RemoveAll(hand => _fullHouseHands.Contains(hand));

            // Three of a kind
            foreach (var hand in _allHands)
            {
                var groupedCards = hand.Cards.GroupBy(x => x).ToList();
                if (groupedCards.Any(x => x.Count() == 3))
                {
                    _threeOfAKindHands.Add(hand);
                }
            }
            _allHands.RemoveAll(hand => _threeOfAKindHands.Contains(hand));

            // Two pair
            foreach (var hand in _allHands)
            {
                var groupedCards = hand.Cards.GroupBy(x => x).ToList();
                if (groupedCards.Count(x => x.Count() == 2) == 2)
                {
                    _twoPairHands.Add(hand);
                }
            }
            _allHands.RemoveAll(hand => _twoPairHands.Contains(hand));

            // One pair
            foreach (var hand in _allHands)
            {
                var groupedCards = hand.Cards.GroupBy(x => x).ToList();
                if (groupedCards.Any(x => x.Count() == 2) && !groupedCards.Any(x => x.Count() == 3))
                {
                    _onePairHands.Add(hand);
                }
            }
            _allHands.RemoveAll(hand => _onePairHands.Contains(hand));

            // High card
            _highCardHands.AddRange(_allHands);
            _allHands.Clear();
        }

        private void JokerProcessing()
        {
            var buffer = new List<Hand>();
            foreach (var hand in _fourOfAKindHands)
            {
                var groupedCards = hand.Cards.GroupBy(x => x).ToList();
                foreach (var gr in groupedCards)
                {
                    if (gr.Key == Card.Joker)
                    {
                        _fiveOfAKindHands.Add(hand);
                        buffer.Add(hand);
                        break;
                    }
                }
            }

            _fourOfAKindHands.RemoveAll(hand => buffer.Contains(hand));
            buffer.Clear();

            foreach (var hand in _fullHouseHands)
            {
                var groupedCards = hand.Cards.GroupBy(x => x).ToList();
                foreach (var gr in groupedCards)
                {
                    if (gr.Key == Card.Joker)
                    {
                        _fiveOfAKindHands.Add(hand);
                        buffer.Add(hand);
                        break;
                    }
                }
            }

            _fullHouseHands.RemoveAll(hand => buffer.Contains(hand));
            buffer.Clear();

            foreach (var hand in _threeOfAKindHands)
            {
                var groupedCards = hand.Cards.GroupBy(x => x).ToList();
                foreach (var gr in groupedCards)
                {
                    if (gr.Key == Card.Joker)
                    {
                        _fourOfAKindHands.Add(hand);
                        buffer.Add(hand);
                        break;
                    }
                }
            }

            _threeOfAKindHands.RemoveAll(hand => buffer.Contains(hand));
            buffer.Clear();

            foreach (var hand in _twoPairHands)
            {
                var groupedCards = hand.Cards.GroupBy(x => x).ToList();

                var foundFourOfAKind = false;

                if (groupedCards.Any(x => x.Count() == 2))
                {
                    foreach (var gr in groupedCards.FindAll(x => x.Count() == 2))
                    {
                        if (gr.Key == Card.Joker)
                        {
                            _fourOfAKindHands.Add(hand);
                            buffer.Add(hand);
                            foundFourOfAKind = true;
                            break;
                        }
                    }
                }

                if (foundFourOfAKind)
                {
                    continue;
                }

                if (groupedCards.Any(x => x.Count() == 1))
                {
                    foreach (var gr in groupedCards.FindAll(x => x.Count() == 1))
                    {
                        if (gr.Key == Card.Joker)
                        {
                            _fullHouseHands.Add(hand);
                            buffer.Add(hand);
                            break;
                        }
                    }
                }
            }

            _twoPairHands.RemoveAll(hand => buffer.Contains(hand));
            buffer.Clear();

            foreach (var hand in _onePairHands)
            {
                var groupedCards = hand.Cards.GroupBy(x => x).ToList();
                foreach (var gr in groupedCards)
                {
                    if (gr.Key == Card.Joker)
                    {
                        _threeOfAKindHands.Add(hand);
                        buffer.Add(hand);
                        break;
                    }
                }
            }

            _onePairHands.RemoveAll(hand => buffer.Contains(hand));
            buffer.Clear();

            foreach (var hand in _highCardHands)
            {
                var groupedCards = hand.Cards.GroupBy(x => x).ToList();
                foreach (var gr in groupedCards)
                {
                    if (gr.Key == Card.Joker)
                    {
                        _onePairHands.Add(hand);
                        buffer.Add(hand);
                        break;
                    }
                }
            }

            _highCardHands.RemoveAll(hand => buffer.Contains(hand));
            buffer.Clear();
        }

        private void OrderSplitHands()
        {
            _allHands.AddRange(_highCardHands.OrderBy(hand => hand.Cards[0])
                .ThenBy(hand => hand.Cards[1]).ThenBy(hand => hand.Cards[2])
                .ThenBy(hand => hand.Cards[3]).ThenBy(hand => hand.Cards[4]).ToList());

            _allHands.AddRange(_onePairHands.OrderBy(hand => hand.Cards[0])
                .ThenBy(hand => hand.Cards[1]).ThenBy(hand => hand.Cards[2])
                .ThenBy(hand => hand.Cards[3]).ThenBy(hand => hand.Cards[4]).ToList());

            _allHands.AddRange(_twoPairHands.OrderBy(hand => hand.Cards[0])
                .ThenBy(hand => hand.Cards[1]).ThenBy(hand => hand.Cards[2])
                .ThenBy(hand => hand.Cards[3]).ThenBy(hand => hand.Cards[4]).ToList());

            _allHands.AddRange(_threeOfAKindHands.OrderBy(hand => hand.Cards[0])
                .ThenBy(hand => hand.Cards[1]).ThenBy(hand => hand.Cards[2])
                .ThenBy(hand => hand.Cards[3]).ThenBy(hand => hand.Cards[4]).ToList());

            _allHands.AddRange(_fullHouseHands.OrderBy(hand => hand.Cards[0])
                .ThenBy(hand => hand.Cards[1]).ThenBy(hand => hand.Cards[2])
                .ThenBy(hand => hand.Cards[3]).ThenBy(hand => hand.Cards[4]).ToList());

            _allHands.AddRange(_fourOfAKindHands.OrderBy(hand => hand.Cards[0])
                .ThenBy(hand => hand.Cards[1]).ThenBy(hand => hand.Cards[2])
                .ThenBy(hand => hand.Cards[3]).ThenBy(hand => hand.Cards[4]).ToList());

            _allHands.AddRange(_fiveOfAKindHands.OrderBy(hand => hand.Cards[0])
                .ThenBy(hand => hand.Cards[1]).ThenBy(hand => hand.Cards[2])
                .ThenBy(hand => hand.Cards[3]).ThenBy(hand => hand.Cards[4]).ToList());
        }

        internal long GetTotalValue()
        {
            return _allHands.Select((t, i) => t.GetBidValue(i + 1)).Sum();
        }
    }

    private class Hand
    {
        internal List<Card> Cards;
        private int _bid;

        internal Hand(string hand, string bid)
        {
            _bid = int.Parse(bid);
            Cards = new List<Card>();

            for (var i = 0; i < 5; i++)
            {
                Cards.Add(GetCardVal(hand[i]));
            }
        }

        internal long GetBidValue(int multiple)
        {
            return _bid * multiple;
        }

        private static Card GetCardVal(char card)
        {
            return card switch
            {
                '2' => Card.Two,
                '3' => Card.Three,
                '4' => Card.Four,
                '5' => Card.Five,
                '6' => Card.Six,
                '7' => Card.Seven,
                '8' => Card.Eight,
                '9' => Card.Nine,
                'T' => Card.Ten,
                'J' => Card.Joker,
                'Q' => Card.Queen,
                'K' => Card.King,
                'A' => Card.Ace,
                _ => throw new Exception()
            };
        }
    }
}