using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using AdventOfCode;
using Genkit;
using System.Linq;

namespace AdventOfCode._2021
{
    public class _2021_15 : AOCDay
    {
        public override void run()
        {
            var lines = readInput().Replace("\r", "").Split('\n').ToList();


            int iw = lines[0].Length;
            int ih = lines.Count;

            int w = iw * 5;
            int h = ih * 5;

            Dictionary<(int,int), int> distance = new Dictionary<(int, int), int>();
            List<(int, int)> frontier = new List<(int, int)>();
            HashSet<(int,int)> visited = new HashSet<(int, int)>();

            var map = new Dictionary<(int, int), int>();
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {

                    if( x == 49 )
                    {
                        ;
                    }
                    int d = (((Convert.ToInt32((lines[y % ih][x % iw].ToString()) + (x / iw) + (y / ih)) - 1) % 9) + 1);
                    map.Add( (x, y),  d);
                    distance.Add( (x,y), int.MaxValue );
                }
            }

            /*
                        for (int y = 0; y < h * 5; y++)
            {
                for (int x = 0; x < w * 5; x++)
                {
                    map.Add( (x, y), (Convert.ToInt32( (lines[y%ih][x%iw].ToString()) + (x/w) + (y/h)) % 10) );
                    distance.Add( (x,y), int.MaxValue );
                }
            }*/

            distance[(0,0)] = 0;
            frontier.Add((0, 0));

            var neighbors = new List<(int, int)>();

            while ( frontier.Count > 0 )
            {
                if( frontier.Count % 100 == 0 ) Console.WriteLine(frontier.Count);
                frontier.Sort( (a,b) => distance[b] - distance[a] );
                var next = frontier[frontier.Count-1];
                frontier.RemoveAt(frontier.Count-1);
                visited.Add(next);

                neighbors.Clear();
                int x = next.Item1;
                int y= next.Item2;
                if ( x < w-1 && !visited.Contains((x + 1, y))) neighbors.Add((x + 1, y));
                if ( x > 0 && !visited.Contains((x - 1, y))) neighbors.Add((x - 1, y));
                if ( y < h-1 && !visited.Contains((x, y + 1))) neighbors.Add((x, y + 1));
                if ( y > 0 && !visited.Contains((x, y - 1))) neighbors.Add((x, y - 1));

                foreach( var neighbor in neighbors )
                {
                    if (!frontier.Contains(neighbor)) frontier.Add(neighbor);
                    int dist = distance[next] + map[neighbor];
                    if( dist < distance[neighbor])
                    {
                        distance[neighbor] = dist;
                    }
                }
            }

            write( "distance:"+distance[(w-1,h-1)]);
        }
    }
}
