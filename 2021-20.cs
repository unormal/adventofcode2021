using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using AdventOfCode;
using Genkit;
using System.Linq;

namespace AdventOfCode._2021
{
    public class _2021_20 : AOCDay
    {
        public override void run()
        {
            var lines = readInput().Replace("\r", "").Split('\n').ToList();

            var algo = lines[0];
            lines.RemoveAt(0);
            lines.RemoveAt(0);

            int w = lines[0].Length;
            int h = lines.Count;

            Dictionary<(int,int), char> map = new System.Collections.Generic.Dictionary<(int, int), char>();
            Dictionary<(int, int), char> map2 = new System.Collections.Generic.Dictionary<(int, int), char>();

            int minx = 0;
            int miny = 0;
            int maxx = w-1;
            int maxy = h-1;
            for( int x=0;x<w;x++ )
            {
                for( int y=0;y<h;y++ )
                { 
                    map.Add((x,y), lines[y][x]);
                }
            }

            int i = 0;
            char get(int x, int y)
            {
                if( !map.ContainsKey((x,y)) && i % 2 == 0 ) return '.';
                if (!map.ContainsKey((x, y)) && i % 2 == 1) return '#';
                return map[(x,y)];
            }

            int nminx = 0;
            int nminy = 0;
            int nmaxy = h-1;
            int nmaxx = w-1;
            void set(int x, int y, char c)
            {
                if( !map2.ContainsKey((x,y)))
                {
                    map2.Add((x,y), c);
                }
                else
                {
                    map2[(x,y)] = c;
                }

                if (x < nminx) nminx = x;
                if (x > nmaxx) nmaxx = x;
                if (y < nminy) nminy = y;
                if (y > nmaxy) nmaxy = y;

            }

            for( i=0;i<50;i++ )
            {
                nmaxx = maxx;
                nmaxy = maxy;
                int border = i+3;
                writeLine(i);
                for( int x=minx- border; x<=maxx+ border; x++ )
                {
                    for( int y=miny- border; y<=maxy+ border; y++ )
                    {
                        StringBuilder bit = new StringBuilder();
                        bit.Append( get(x-1,y-1));
                        bit.Append(get(x, y - 1));
                        bit.Append(get(x + 1, y - 1));
                        bit.Append(get(x - 1, y));
                        bit.Append(get(x , y));
                        bit.Append(get(x + 1, y));
                        bit.Append(get(x - 1, y + 1));
                        bit.Append(get(x, y + 1));
                        bit.Append(get(x + 1, y + 1));
                        bit = bit.Replace(".", "0");
                        bit = bit.Replace("#", "1");

                        int offset = Convert.ToInt32(bit.ToString(),2);

                        set( x,y, algo[offset]);
                        ;
                    }
                }

                maxx = nmaxy;
                maxy = nmaxy;

                map = map2;
                map2 = new Dictionary<(int, int), char>();
            }

            // 5819
            writeLine( "#C=" + map.Values.Where( v=>v=='#').Count() );
        }
    }
}
