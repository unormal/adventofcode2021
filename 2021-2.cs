using System;
using System.Collections.Generic;
using System.Text;
using AdventOfCode;
using Genkit;
using System.Linq;

namespace AdventOfCode._2021
{
    public class _2021_2 :AOCDay
    {
        public override void run()
        {
            int a = 0;

            int x = 0;
            int depth = 0;
            int aim = 0;

            foreach( var line in readInput().Replace("\r", "").Split('\n') )
            {
                var parts = line.Split(" ");

                string s = parts[0];
                int n = Convert.ToInt32(parts[1]);

                if( s == "forward") x += n;
                if( s == "back") x -= n;
                //if( s == "down") depth += n;
                //if( s == "up") depth -= n;

                if( s == "down") aim += n;
                if( s == "up") aim -= n;
                if( s == "forward")
                {
                    depth += aim*n;
                }
            }

            write(Math.Abs(x)*Math.Abs(depth));
        }
    }
}
