using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using AdventOfCode;
using Genkit;
using System.Linq;

namespace AdventOfCode._2021
{
    public class _2021_13 : AOCDay
    {
        public override void run()
        {
            var lines = readInput().Replace("\r", "").Split('\n');

            int w = lines[0].Length;
            int h = lines.Length;

            List<(int,int)> map = new List<(int, int)>();

            int x = 0;
            for( x=0;x<lines.Length;x++ )
            {
                var line = lines[x];
                if( line == "") break;

                var parts = lines[x].Split(',');
                map.Add( (Convert.ToInt32(parts[0]), Convert.ToInt32(parts[1])));
            }
            x++;
            for(;x<lines.Length;x++ )
            {
                var line = lines[x].Replace("fold along ", "");
                var parts = line.Split('=');

                int fold = Convert.ToInt32(parts[1]);

                var newMap = new List<(int, int)>();
                if ( parts[0] == "x")
                {
                    foreach( var p in map )
                    {
                        var next = (0,0);
                        if( p.Item1 > fold )
                        {
                            next = (fold - (p.Item1-fold), p.Item2);
                        }
                        else
                        if( p.Item1 == fold )
                        {
                            ;
                        }
                        else
                        {
                            next = p;
                        }

                        if( !newMap.Contains(next)) newMap.Add(next);
                    }
                }
                else
                if( parts[0] == "y")
                {
                    foreach (var p in map)
                    {
                        var next = (0, 0);
                        if (p.Item2 > fold)
                        {
                            next = (p.Item1, fold - (p.Item2 - fold));
                        }
                        else
                        if (p.Item2 == fold)
                        {
                            ;
                        }
                        else
                        {
                            next = p;
                        }

                        if (!newMap.Contains(next)) newMap.Add(next);
                    }
                }

                map = newMap;
            }

            Console.Clear();
            for (int m = 0; m < map.Count; m++)
            {
                Console.SetCursorPosition(map[m].Item1, map[m].Item2);
                Console.Write('#');
            }

            //write("X");
            //write(map.Count);
        }

    }
}
