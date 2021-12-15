using DailyCode.Base;
using System.Text.RegularExpressions;

namespace DailyCode.Year2021.Days
{

    public class FastObject
    {
        public string MatchPair { get; set; }
        public long Total { get; set; }
    }

    public class Day14 : BaseDay
    {
        private readonly Regex _inputRgx = new(@"(\w{2}) -> (\w{1})");
        private List<DoSomething> _fileInput = new();

       private List<SecondTwo> SecondTwos = new();

        private string _polymerTemplate = "";

        private readonly List<(string matchPair, string insertChar, Action<string, string, int> action)> Test = new();

        private readonly List<(string matchPair, string insertChar, List<string> createdPairs)> Test2 = new();

        public Day14(string sessionId) : base(sessionId)
        {
        }

        protected override void SetupData(List<string> fileInput)
        {
            _polymerTemplate = fileInput[0];
            var input = fileInput.Skip(2).Select(line =>
            {
                var match = _inputRgx.Match(line);
                return (matchPair: match.Groups[1].Value, insertChar: match.Groups[2].Value);
            });

           var c =  input.Select(x => new DoSomething(x.matchPair, x.insertChar)).ToList();


            //
            //var test = new List<(string matchPair, Action<string, string, int> action)>();


            foreach(var (matchPair, insertChar) in input)
            {
                Test2.Add((matchPair, insertChar, new List<string> { matchPair[0] + insertChar, insertChar + matchPair[1] }));
                    /*  var something = c.Single(x => x.Matchpairs.Equals(matchPair));
                var res = something.CreatePairs(insertChar);
                something.CreatedPairs.AddRange(c.Where(x => res.Contains(x.Matchpairs)));

                Test.Add((matchPair, insertChar, (m, i, total) =>
                {
                    if(total > 0)
                    {
                        --total;
                        ++SecondTwos.Single(x => x.Character == i[0]).Total;
                        var l = Test.Single(x => x.matchPair == $"{m[0]}{i}");
                        l.action(m, l.insertChar, total);

                        l = Test.Single(x => x.matchPair == $"{i}{m[1]}");
                        l.action(m, l.insertChar, total);
                    }        
                }
                ));*/
            }

            SecondTwos = input.Select(x => x.insertChar).Distinct().Select(x => new SecondTwo { Character = x[0] }).ToList();
            _fileInput = c.ToList();
        }

        private void DoSomething3(long total, List<FastObject> ll)
        {
            if (total > 0)
            {
                --total;

                var l = new List<FastObject>();
                var pairs = ll.Where(x => x.Total > 0);
               

                foreach(var pair in pairs)
                {
                   var t2 = Test2.Single(x => x.matchPair == pair.MatchPair);
                    var pair1 = t2.matchPair[0] + t2.insertChar;
                    var pair2 = t2.insertChar + t2.matchPair[1];
                    var found1 = l.FirstOrDefault(x => x.MatchPair == pair1);
                    var found2 = l.FirstOrDefault(x => x.MatchPair == pair2);
                    if (found1 == null)
                    {
                        l.Add(new FastObject { MatchPair = pair1, Total = pair.Total });
                    }
                    else
                    {
                        found1.Total += pair.Total;
                    }
                    if (found2 == null)
                    {
                        l.Add(new FastObject { MatchPair = pair2, Total = pair.Total });
                    }
                    else
                    {
                        found2.Total += pair.Total;
                    }

                    //   t2.createdPairs
                    SecondTwos.Single(x => x.Character == t2.insertChar[0]).Total += pair.Total;
                }

                DoSomething3(total, l);
                    }
                }

        protected override string RunPart1()
        {
            var l = new List<FastObject>();
            for (int i = 0; i < _polymerTemplate.Length -1 ; i++)
            {
                var matchPair = $"{_polymerTemplate[i]}{ _polymerTemplate[i + 1]}";
                var found = l.FirstOrDefault(x => x.MatchPair == matchPair);
                if (found == null)
                {
                    l.Add(new FastObject { MatchPair = matchPair, Total = 1 });
                }
                else
                {
                    found.Total += 1;
                }
                
            }

            DoSomething3(40, l);


            foreach(var p in SecondTwos)
            {
                p.Total += _polymerTemplate.Count(x => x == p.Character);
              //  p.Total += b.SingleOrDefault(x => x.Character == p.Character)?.Totals ?? 0;
            }
            //_polymerTemplate.ToList().ForEach(x => b.Single(c => c.Character == x).Totals += 1);
            //return (b.Max(x => x.Totals) - b.Min(x => x.Totals)).ToString();
            return (SecondTwos.Max(x => x.Total) - SecondTwos.Min(x => x.Total)).ToString();
        }

        protected override string RunPart2()
        {
            for (int i = 0; i < _polymerTemplate.Length - 1; i++)
            {

                var matchPair = $"{_polymerTemplate[i]}{ _polymerTemplate[i + 1]}";
                var l = Test.Single(x => matchPair.Equals(x.matchPair));
                l.action(matchPair, l.insertChar, 10);
            }


            foreach (var p in SecondTwos)
            {
                p.Total += _polymerTemplate.Count(x => x == p.Character);
               // p.Total += b.SingleOrDefault(x => x.Character == p.Character)?.Totals ?? 0;
            }
            //_polymerTemplate.ToList().ForEach(x => b.Single(c => c.Character == x).Totals += 1);
            //return (b.Max(x => x.Totals) - b.Min(x => x.Totals)).ToString();
            return (SecondTwos.Max(x => x.Total) - SecondTwos.Min(x => x.Total)).ToString();
        }
    }

    public class DoSomething
    {
        public string Matchpairs { get; set; } = string.Empty;

        public string InsertChar { get; set; } = string.Empty;

        public List<DoSomething> CreatedPairs { get; set; } = new();

        public long Total { get; set; }

        public DoSomething(string matchpairs, string insertChar)
        {
            Matchpairs = matchpairs;
            InsertChar = insertChar;
        }

        public List<string> CreatePairs(string character)
        {
            var l = new List<string>
            {
                Matchpairs[0] + character,
                character + Matchpairs[1]
            };

            return l;
        }

        public void SetTotal(long i)
        {
            if(i > 0)
            {
                --i;
                 ++Total;
                 CreatedPairs.ForEach(x => x.SetTotal(i));
            }    
        }

        public (char character, long total) GetNeeded()
        {
            return (InsertChar[0], Total);
        }
    }

    public class SecondTwo
    {
        public char Character { get; set; }

        public long Total { get; set; }
    }
}