using System;
using System.Collections.Generic;
using System.Text;
using AdventOfCode;
using Genkit;
using System.Linq;

namespace AdventOfCode._2021
{
    public class _2021_1 :AOCDay
    {
        public override void run()
        {
            List<int> i = new List<int>();

            int last = int.MinValue;
            int a =  0;

            List<int> vals = new List<int>();

            foreach( var n in readInput().Replace("\r", "").Split('\n')) 
            {
                int vx = Convert.ToInt32(n);
                vals.Add(vx);

                if( vals.Count == 3 )
                {
                    int v = 0;
                    for( int x=0;x<3;x++ ) v += vals[x];

                    if (last != int.MinValue && v > last)
                    {
                        a++;
                    }
                    last = v;
                    vals.RemoveAt(0);
                }

            }

            write(a);
        }
    }
}
