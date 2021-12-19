using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using AdventOfCode;
using Genkit;
using System.Linq;

namespace AdventOfCode._2021
{
    public class _2021_8 : AOCDay
    {
        public override void run()
        {
            var lines = readInput().Replace("\r", "").Split('\n');

            var digituses = new Dictionary<string, string >();
            digituses.Add( "0" , "abcefg"); // 6
            digituses.Add( "1", "cf"); // 2
            digituses.Add( "2", "acdeg"); //5 
            digituses.Add("3", "acdfg"); // 5
            digituses.Add("4", "bcdf"); // 4
            digituses.Add("5", "abdfg"); // 5
            digituses.Add("6", "abdefg"); // 6
            digituses.Add("7", "acf"); // 3
            digituses.Add("8", "abcdefg"); // 7
            digituses.Add("9", "abcdfg"); // 6

            var patterns = new Dictionary<string,string>();
            var alphain = new Dictionary<string,string>();

            foreach( var kv in digituses ) patterns.Add(kv.Value,kv.Key);

            for( char c = 'a'; c <= 'g'; c++ )
            {
                string i = "";
                foreach( var key in digituses.Keys )
                {
                    if( digituses[key].Contains( c.ToString() ) ) i += key;
                }

                alphain.Add(c.ToString(), i);
            }

            int a1 = 0;

            long answer = 0;
            foreach( var line in lines )
            {
                var parts = line.Split(" | ");
                var left = parts[0].Split(" ");
                var right = parts[1].Split(" ");
                
                Dictionary<string, List<string>> map = new Dictionary<string, List<string>>();

                map.Add("a", new List<string> { "a", "b", "c", "d", "e", "f", "g"});
                map.Add("b", new List<string> { "a", "b", "c", "d", "e", "f", "g" });
                map.Add("c", new List<string> { "a", "b", "c", "d", "e", "f", "g" });
                map.Add("d", new List<string> { "a", "b", "c", "d", "e", "f", "g" });
                map.Add("e", new List<string> { "a", "b", "c", "d", "e", "f", "g" });
                map.Add("f", new List<string> { "a", "b", "c", "d", "e", "f", "g" });
                map.Add("g", new List<string> { "a", "b", "c", "d", "e", "f", "g" });

                /*
                foreach( var i in left.Where( l => l.Length == 2 ))
                {
                    foreach( char c in i )
                    {
                        map[c.ToString()].RemoveAll( ch => !digituses["1"].Contains(ch) );
                    }
                }

                foreach (var i in left.Where(l => l.Length == 3))
                {
                    foreach (char c in i)
                    {
                        map[c.ToString()].RemoveAll(ch => !digituses["7"].Contains(ch));
                    }
                }

                foreach (var i in left.Where(l => l.Length == 4 ))
                {
                    foreach (char c in i)
                    {
                        map[c.ToString()].RemoveAll(ch => !digituses["4"].Contains(ch));
                    }
                }

                foreach (var i in left.Where(l => l.Length == 5))
                {
                    foreach (char c in i)
                    {
                        map[c.ToString()].RemoveAll(ch => !digituses["2"].Contains(ch) && !digituses["3"].Contains(ch) && !digituses["5"].Contains(ch));
                    }
                }

                foreach (var i in left.Where(l => l.Length == 6))
                {
                    foreach (char c in i)
                    {
                        map[c.ToString()].RemoveAll(ch => !digituses["0"].Contains(ch) && !digituses["6"].Contains(ch) && !digituses["9"].Contains(ch));
                    }
                }

                foreach (var i in left.Where(l => l.Length == 7))
                {
                    foreach (char c in i)
                    {
                        map[c.ToString()].RemoveAll(ch => !digituses["8"].Contains(ch));
                    }
                }
                  */

                for( int a=0;a<map["a"].Count;a++ )
                {
                    for( int b=0;b<map["b"].Count;b++ )
                    {
                        for( int c=0;c<map["c"].Count;c++ )
                        {
                            for( int d=0;d<map["d"].Count;d++ )
                            {
                                for( int e=0;e<map["e"].Count;e++ )
                                {
                                    for( int f=0;f<map["f"].Count;f++ )
                                    {
                                        for (int g = 0; g < map["g"].Count; g++)
                                        {
                                            string av = map["a"][a];
                                            string bv = map["b"][b];
                                            string cv = map["c"][c];
                                            string dv = map["d"][d];
                                            string ev = map["e"][e];
                                            string fv = map["f"][f];
                                            string gv = map["g"][g];

                                            if( av == bv ) continue;
                                            if (av == cv) continue;
                                            if (av == dv) continue;
                                            if (av == ev) continue;
                                            if (av == fv) continue;
                                            if (av == gv) continue;

                                            if (bv == cv) continue;
                                            if (bv == dv) continue;
                                            if (bv == ev) continue;
                                            if (bv == fv) continue;
                                            if (bv == gv) continue;

                                            if (cv == dv) continue;
                                            if (cv == ev) continue;
                                            if (cv == fv) continue;
                                            if (cv == gv) continue;

                                            if (dv == ev) continue;
                                            if (ev == fv) continue;
                                            if (fv == gv) continue;

                                            if (ev == fv) continue;
                                            if (ev == gv) continue;

                                            if (fv == gv) continue;

                                            bool dotest( string test )
                                            {
                                                string o = "";
                                                foreach( char c in test )
                                                {
                                                    if( c == 'a') o += av;
                                                    if (c == 'b') o += bv;
                                                    if (c == 'c') o += cv;
                                                    if (c == 'd') o += dv;
                                                    if (c == 'e') o += ev;
                                                    if (c == 'f') o += fv;
                                                    if (c == 'g') o += gv;
                                                }
                                                o = String.Concat(o.OrderBy(o => o));
                                                if ( !patterns.ContainsKey(o)) return false;

                                                return true;
                                            }

                                            int mappattern( string test )
                                            {
                                                string o = "";
                                                foreach (char c in test)
                                                {
                                                    if (c == 'a') o += av;
                                                    if (c == 'b') o += bv;
                                                    if (c == 'c') o += cv;
                                                    if (c == 'd') o += dv;
                                                    if (c == 'e') o += ev;
                                                    if (c == 'f') o += fv;
                                                    if (c == 'g') o += gv;
                                                }
                                                o = String.Concat(o.OrderBy(o => o));
                                                return Convert.ToInt32(patterns[o]);
                                            }

                                            foreach( var test in left )
                                            {
                                                if( !dotest(test)) goto nah;
                                            }

                                            foreach ( var test in right )
                                            {
                                                if (!dotest(test)) goto nah;
                                            }

                                            string resconcat = "";
                                            foreach( var ans in right )
                                            {
                                                int result = mappattern(ans);
                                                resconcat += result;
                                            }

                                            answer += Convert.ToInt32(resconcat);
                                            goto next;

                                            nah:;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                next:;
                ;
            }



            write( answer );
            writeLine();
        }
    }
}
