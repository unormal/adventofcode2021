using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using AdventOfCode;
using Genkit;
using System.Linq;

namespace AdventOfCode._2021
{
    public class _2021_9 : AOCDay
    {
        public override void run()
        {
            var lines = readInput().Replace("\r", "").Split('\n');

            Dictionary<(int,int), int> map = new Dictionary<(int, int), int>();

            int w = 0;
            int h = 0;

            for( int y=0;y<lines.Length;y++ )
            {
                var line = lines[y];
                for( int x=0;x<line.Length;x++ )
                {
                    map.Add( (x,y), Convert.ToInt32(line[x].ToString()) );
                }
            }

            h = lines.Length;
            w = lines[0].Length;

            int get( int x, int y )
            {
                if( !map.ContainsKey((x,y)) ) return -1;
                return map[(x,y)];
            }

            HashSet<(int,int)> minima = new HashSet<(int, int)>();

            int sum = 0;
            for( int x=0;x<w;x++ )
            {
                for( int y=0;y<h;y++ )
                {
                    int m = map[(x,y)];
                    bool lowest = true;
                    if( get(x-1,y) != -1 && get(x-1,y) <= m ) lowest = false;
                    if (get(x + 1, y) != -1 && get(x + 1, y) <= m) lowest = false;
                    if (get(x , y - 1) != -1 && get(x, y - 1) <= m) lowest = false;
                    if (get(x , y + 1) != -1 && get(x, y + 1) <= m) lowest = false;

                    if( lowest ) 
                    {
                        sum += (map[(x,y)] + 1);                
                        minima.Add( (x,y));
                    }
                }
            }

            Dictionary<(int,int),int> basins = new Dictionary<(int, int), int>();

            List<int> sizes = new List<int>();
            foreach( var m in minima )
            {
                int size = 0;
                HashSet<(int,int)> visited = new HashSet<(int, int)>();
                Queue<(int,int)> frontier = new Queue<(int, int)>();
                frontier.Enqueue( m );

                while( frontier.Count > 0 )
                {
                    var next = frontier.Dequeue();
                    if( !visited.Contains(next))
                    {
                        visited.Add(next);
                        size++;

                        int v = get(next.Item1-1,next.Item2);
                        if( v != -1 && v != 9 && !visited.Contains((next.Item1 - 1, next.Item2))) frontier.Enqueue((next.Item1 - 1, next.Item2));

                        v = get(next.Item1 + 1, next.Item2);
                        if (v != -1 && v != 9 && !visited.Contains((next.Item1 + 1, next.Item2))) frontier.Enqueue((next.Item1 + 1, next.Item2));

                        v = get(next.Item1, next.Item2 - 1);
                        if (v != -1 && v != 9 && !visited.Contains((next.Item1, next.Item2 - 1))) frontier.Enqueue((next.Item1, next.Item2 - 1));

                        v = get(next.Item1, next.Item2 + 1);
                        if (v != -1 && v != 9 && !visited.Contains((next.Item1, next.Item2 + 1))) frontier.Enqueue((next.Item1, next.Item2 + 1));
                    }
                }

                sizes.Add(size);
            }

            sizes.Sort( (a,b) => b-a );

            writeLine( (sizes[0] * sizes[1] * sizes[2]) );

             writeLine();
        }
    }
}
