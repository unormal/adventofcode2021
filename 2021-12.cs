using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using AdventOfCode;
using Genkit;
using System.Linq;

namespace AdventOfCode._2021
{
    public class _2021_12 : AOCDay
    {
        public override void run()
        {
            var lines = readInput().Replace("\r", "").Split('\n');


            Dictionary<string,List<string>> routes = new Dictionary<string, List<string>>();
            
            foreach( var line in lines )
            {
                var parts = line.Split('-');

                if( !routes.ContainsKey(parts[0])) routes.Add(parts[0], new List<string>());
                if( !routes.ContainsKey(parts[1])) routes.Add(parts[1], new List<string>());
                if( !routes[parts[0]].Contains(parts[1])) routes[parts[0]].Add(parts[1]);
                if( !routes[parts[1]].Contains(parts[0])) routes[parts[1]].Add(parts[0]);
            }

            int len = routes.Keys.Count;

            Queue<(string,string,int,string)> frontier = new Queue<(string,string,int,string)>();
            frontier.Enqueue( ("start","start",0, null));

            List<string> usedFull = new List<string>();
            HashSet<(string,string,int,string)> used = new HashSet<(string, string, int, string)>();
            
            List<string> solves = new List<string>();

            int countOccurences(string needle, string haystack)
            {
                return (haystack.Length - haystack.Replace(needle, "").Length) / needle.Length;
            }

            while ( frontier.Count > 0 )
            {
                var next = frontier.Dequeue();

                foreach( var step in routes[next.Item1])
                {
                    string n = next.Item2 + "," + step;

                    if ( step != "start" )
                    {
                        string filter = next.Item4;
                        if( step != "end")
                        {
                            if( step.All(c => char.IsLower(c)) )
                            {
                                if (step.All(c => char.IsLower(c)) && countOccurences("," + step + ",", next.Item2) > 0)
                                {
                                    if (next.Item4 != null) continue;
                                    filter = step;
                                }
                            }
                        }


                        if( used.Contains((step, n, next.Item3 + 1, filter))) continue;
                        used.Add((step, n, next.Item3 + 1, filter));
                        if (step == "end")
                        {
                            solves.Add( n );
                        }
                        else
                        {
                            frontier.Enqueue((step, n, next.Item3+1, filter));
                        }
                    }
                }
            }

            solves.Sort();
            write( solves.Count );
        }

    }
}
