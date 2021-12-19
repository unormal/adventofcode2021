using System;
using System.Collections.Generic;
using System.Text;
using AdventOfCode;
using Genkit;
using System.Linq;

namespace AdventOfCode._2021
{
    public class _2021_4 : AOCDay
    {
        public override void run()
        {
            int a = 0;

            var lines = readInput().Replace("\r", "").Split('\n');
            var length = readInput().Replace("\r", "").Split('\n').Length;

            var nums = lines[0].Split(",").Select( s => Convert.ToInt32(s));
            List<Dictionary<(int,int),int>> grids = new List<Dictionary<(int, int), int>>();
            List<Dictionary<(int, int), bool>> marks = new List<Dictionary<(int, int), bool>>();

            for ( int g=2;g<length;g+=6 )
            {
                Dictionary<(int, int), int> newgrid = new Dictionary<(int, int), int>();
                Dictionary<(int, int), bool> newmark = new Dictionary<(int, int), bool>();

                for ( int y=g;y<g+5;y++ )
                {
                    lines[y] = lines[y].Trim();
                    while( lines[y].Contains("  ") )lines[y] = lines[y].Replace("  ", " ");
                    var line = lines[y].Split(" ");

                    for( int x=0;x<line.Length;x++ )
                    {
                        newgrid.Add( (x,y-g), Convert.ToInt32(line[x]));
                        newmark.Add((x,y-g), false);
                    }
                }

                grids.Add(newgrid);
                marks.Add(newmark);
            }

            Dictionary<(int,int),int> grid = null;
            Dictionary<(int, int),bool> mark = null;

            again:;

            int lastnum = -1;
            foreach( var num in nums )
            {
                lastnum = num;
                for( int g=0;g<grids.Count;g++ )
                {
                    grid = grids[g];
                    mark = marks[g];

                    for( int x=0;x<5;x++ )
                    {
                        for( int y=0;y<5;y++ )
                        {
                            if( grid[(x,y)] == num )
                            {
                                mark[(x,y)] = true;
                            }
                        }
                    }

                    for( int x=0;x<5;x++ )
                    {
                        bool col = true;
                        for( int y=0;y<5;y++ )
                        {
                            if(mark[(x,y)] == false)
                            {
                                col = false;
                                break;
                            }
                        }

                        if( col )
                        {
                            goto done;
                        }
                    }

                    for (int x = 0; x < 5; x++)
                    {
                        bool col = true;
                        for (int y = 0; y < 5; y++)
                        {
                            if (mark[(y, x)] == false)
                            {
                                col = false;
                                break;
                            }
                        }

                        if (col)
                        {
                            goto done;
                        }
                    }

                }
            }

            done:;
            if( grids.Count > 1 )
            {
                grids.Remove(grid);
                marks.Remove(mark);
                goto again;
            }

            int sum = 0;
            for( int x=0;x<5;x++ )
            {
                for( int y=0;y<5;y++ )
                {
                    if( !mark[(x,y)]) sum += grid[(x,y)];
                }
            }

            writeLine();
            write(lastnum * sum);
            writeLine();
        }
    }
}
