using System;
using System.Collections.Generic;
using System.Text;
using AdventOfCode;
using Genkit;
using System.Linq;

namespace AdventOfCode._2021
{
    public class _2021_5 : AOCDay
    {
        public override void run()
        {
            int a = 0;

            var lines = readInput().Replace("\r", "").Split('\n');

            Dictionary<(int,int), int> grid = new Dictionary<(int, int), int>();

            foreach( var line in lines.Select(s => s.Replace(" ", "")).Select(s => s.Replace("-", "")) )
            {
                var parts = line.Split(">");
                
                Point2D start = new Point2D( Convert.ToInt32(parts[0].Split(',')[0]), Convert.ToInt32(parts[0].Split(',')[1]));
                Point2D end = new Point2D(Convert.ToInt32(parts[1].Split(',')[0]), Convert.ToInt32(parts[1].Split(',')[1]));

                
                if( start.x == end.x)
                {
                    if (start.y > end.y)
                    {
                        int swap = end.y;
                        end.y = start.y;
                        start.y = swap;
                    }
                    for ( int y=start.y;y<=end.y;y++ )
                    {
                        if( !grid.ContainsKey((start.x,y))) grid.Add((start.x,y), 0);
                        grid[(start.x,y)]++;
                    }
                }
                else
                if( start.y == end.y )
                {
                    if( start.x > end.x)
                    {
                        int swap = end.x;
                        end.x = start.x;
                        start.x = swap;
                    }
                    for (int x = start.x; x <= end.x; x++)
                    {
                        if (!grid.ContainsKey((x, start.y))) grid.Add((x, start.y), 0);
                        grid[(x, start.y)]++;
                    }
                }
                else
                {
                    if (start.x > end.x)
                    {
                        int swap = end.x;
                        end.x = start.x;
                        start.x = swap;

                        swap = end.y;
                        end.y = start.y;
                        start.y = swap;
                    }

                    int delta = (start.y > end.y) ? -1 : 1;

                    int y = start.y;
                    for( int x=start.x; x <= end.x; x++, y+=delta )
                    {
                        if (!grid.ContainsKey((x,y))) grid.Add((x, y), 0);
                        grid[(x,y)]++;
                    }

                }
            }

            write(grid.Where( v => v.Value > 1 ).Count());

            writeLine();
        }
    }
}
