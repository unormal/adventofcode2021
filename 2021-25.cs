using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using AdventOfCode;
using Genkit;
using System.Linq;

namespace AdventOfCode._2021
{
    public class _2021_25 : AOCDay
    {
        public override void run()
        {
            var lines = readInput().Replace("\r", "").Split('\n').ToList();

            Dictionary<(int,int), char> map = new Dictionary<(int, int), char> ();

            int w = lines[0].Length;
            int h = lines.Count;

            for( int x=0;x<w;x++ )
            {
                for( int y=0;y<h;y++ )
                {
                    if( lines[y][x] != '.' ) map.Add( (x,y), lines[y][x] );
                }
            }


            Dictionary<(int, int), char> next = new Dictionary<(int, int), char>();
            
            int steps = 0;
            while( true )
            {
                steps++;

                Dictionary<(int,int), char> o = new Dictionary<(int, int), char>(map);

                // east
                foreach (var pair in map)
                {
                    if (pair.Value == '>')
                    {
                        (int, int) east = (pair.Key.Item1 + 1, pair.Key.Item2);

                        if (east.Item1 >= w) east = (0, pair.Key.Item2);

                        if (map.ContainsKey(east))
                        {
                            next.Add(pair.Key, pair.Value);
                        }
                        else
                        {
                            next.Add(east, pair.Value);
                        }
                    }
                    else
                    {
                        next.Add(pair.Key, pair.Value);
                    }
                }
                /*
                Console.SetCursorPosition(0, 0);
                for (int x = 0; x < w; x++)
                {
                    for (int y = 0; y < h; y++)
                    {
                        Console.SetCursorPosition(x, y);
                        if (next.ContainsKey((x, y)))
                        {
                            Console.Write(next[(x, y)]);
                        }
                        else
                        {
                            Console.Write(".");
                        }
                    }
                }
                Console.ReadLine();
                */
                map = next;
                next = new Dictionary<(int, int), char>();

                foreach (var pair in map)
                {
                    if (pair.Value == 'v')
                    {
                        (int, int) south = (pair.Key.Item1, pair.Key.Item2 + 1);

                        if (south.Item2 >= h) south = (pair.Key.Item1, 0);

                        if (map.ContainsKey(south))
                        {
                            next.Add(pair.Key, pair.Value);
                        }
                        else
                        {
                            next.Add(south, pair.Value);
                        }
                    }
                    else
                    {
                        next.Add(pair.Key, pair.Value);
                    }
                }
                /*
                Console.SetCursorPosition(0, 0);
                for (int x = 0; x < w; x++)
                {
                    for (int y = 0; y < h; y++)
                    {
                        Console.SetCursorPosition(x, y);
                        if (next.ContainsKey((x, y)))
                        {
                            Console.Write(next[(x, y)]);
                        }
                        else
                        {
                            Console.Write(".");
                        }
                    }
                }
                Console.ReadLine();
                */
                if (next.All(n => o.ContainsKey(n.Key) && o[n.Key] == o[n.Key]))
                {
                    Console.WriteLine("same after " + steps);
                    Console.ReadLine();
                    break;
                }

                map = next;
                next = new Dictionary<(int, int), char>();


            }
        }

    }
}
