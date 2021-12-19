using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using AdventOfCode;
using Genkit;
using System.Linq;

namespace AdventOfCode._2021
{
    public class _2021_7 : AOCDay
    {
        public override void run()
        {
            int a = 0;

            var lines = readInput().Replace("\r", "").Split('\n');

            var crab = lines[0].Split(',').Select( c=>Convert.ToInt64(c)).ToList();

            crab.Sort();
            
            long dist( long a, long b)
            {
                long d = 0;
                long steps = Math.Abs(a-b);

                d = (steps*(1+steps))/2;

                return d;
            }

            long mind = int.MaxValue;
            for( long x=crab[0];x<=crab[crab.Count-1];x++ )
            {
                long distance = crab.Sum( c => dist(c,x));
                if( distance < mind ) mind = distance;
            }
            

            write(mind);
            writeLine();
        }
    }
}
