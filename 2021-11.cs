using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using AdventOfCode;
using Genkit;
using System.Linq;

namespace AdventOfCode._2021
{
    public class _2021_11 : AOCDay
    {
        public override void run()
        {
            var lines = readInput().Replace("\r", "").Split('\n');

            Dictionary<(int,int), int> map = new Dictionary<(int, int), int>();

            int w = lines[0].Length;
            int h = lines.Length;

            for( int x=0;x<w;x++ )
            {
                for( int y=0;y<h;y++ )
                {
                    map.Add((x,y), Convert.ToInt32(lines[y][x].ToString()));
                }
            }

            int flashes = 0;
            for( int s=1;;s++ )
            {
                foreach( var kv in map ) map[kv.Key]++;

                HashSet<(int,int)> flashed = new HashSet<(int, int)>();
                List<(int,int)> flashedlist = new List<(int, int)>();

                int roundflashes = 0;
                again:;
                bool again = false;
                foreach (var kv in map) 
                {
                    if( kv.Value > 9 && !flashed.Contains(kv.Key) )
                    {
                        flashed.Add(kv.Key);
                        flashedlist.Add(kv.Key);
                        again = true;
                        flashes++;
                        roundflashes++;

                        for( int x=-1;x<=1;x++ )
                        {
                            for( int y=-1;y<=1;y++ )
                            {
                                if( map.ContainsKey( (kv.Key.Item1+x, kv.Key.Item2+y) ) ) map[(kv.Key.Item1 + x, kv.Key.Item2 + y)] += 1;
                            }
                        }
                    }
                }
                if( again ) goto again;

                foreach( var f in flashedlist ) map[f] = 0;

                /*
                for( int y=0;y<h;y++ )
                {
                    for( int x=0;x<w;x++ )
                    {
                        write(map[(x,y)]);
                    }
                    writeLine();
                } */
                //writeLine();

                //Console.ReadKey();

                if( roundflashes == w*h )
                {
                    writeLine(s);
                    Console.ReadKey();
                }
            }

            write(flashes);
        }

    }
}
