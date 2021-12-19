using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using AdventOfCode;
using Genkit;
using System.Linq;

namespace AdventOfCode._2021
{
    public class _2021_6 : AOCDay
    {
        public override void run()
        {
            int a = 0;

            var lines = readInput().Replace("\r", "").Split('\n');

            List<int> fish = lines[0].Split(',').Select( x => Convert.ToInt32(x)).ToList();

            Dictionary<int,long> fishtable = new Dictionary<int, long>();
            for( int p = 0; p<= 9;p++)
            {

                Dictionary<int, long> f = new Dictionary<int, long>();

                for (int x = 0; x <= 8; x++)
                {
                    f.Add(x, 0);
                }

                f[p] = 1;
                /*
                foreach (var fi in fish)
                {
                    f[fi]++;
                } */

                Dictionary<int, long> f2 = new Dictionary<int, long>();
                for (int x = 0; x < 80; x++)
                {
                    f2.Clear();

                    f2.Add(8, f[0]);
                    f2.Add(7, f[8]);
                    f2.Add(6, f[7] + f[0]);
                    f2.Add(5, f[6]);
                    f2.Add(4, f[5]);
                    f2.Add(3, f[4]);
                    f2.Add(2, f[3]);
                    f2.Add(1, f[2]);
                    f2.Add(0, f[1]);

                    var swap = f2;
                    f2 = f;
                    f = swap;
                }

                long s = 0;
                foreach (var kv in f) s += kv.Value;
                write( p + "=" + s );
                writeLine();
                fishtable.Add(p,s);
            }

            long sum = 0;
            foreach( var f in fish )
            {
                sum += fishtable[f];
            }

            write(sum);
            writeLine();
        }
    }
}
