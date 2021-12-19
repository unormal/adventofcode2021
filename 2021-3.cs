using System;
using System.Collections.Generic;
using System.Text;
using AdventOfCode;
using Genkit;
using System.Linq;

namespace AdventOfCode._2021
{
    public class _2021_3 : AOCDay
    {
        public override void run()
        {
            int a = 0;

            var lines = readInput().Replace("\r", "").Split('\n');
            var length = readInput().Replace("\r", "").Split('\n')[0].Length;

            string gamma = "";
            string epsilon = "";

            for( int c=0;c<length;c++ )
            {
                int z = 0;
                int o = 0;

                foreach( var line in lines )
                {
                    if( line[c] == '0') z++;
                    if( line[c] == '1') o++;
                }

                if( z > o )
                {
                    gamma += "0";
                    epsilon += "1";
                }
                else
                if( z < o)
                {
                    gamma += "1";
                    epsilon += "0";
                }
                else
                {
                    ;
                }
            }

            int g = Convert.ToInt32(gamma, 2);
            int e = Convert.ToInt32(epsilon,2);

            write(g*e);


            var o2lines = new List<string>(lines);

            while( o2lines.Count > 1 )
            {
                length = lines[0].Length;

                for (int c = 0; c < length && o2lines.Count > 1 ; c++)
                {
                    int z = 0;
                    int o = 0;

                    string result = "";

                    foreach (var line in o2lines)
                    {
                        if (line[c] == '0') z++;
                        if (line[c] == '1') o++;
                    }

                    if (z > o)
                    {
                        result = "0";
                    }
                    else
                    if (z < o)
                    {
                        result = "1";
                    }
                    else
                    {
                        result = "1";
                    }

                    if( result != "" ) o2lines.RemoveAll( l => l[c] != result[0] );
                }
            }


            var co2lines = new List<string>(lines);

            while (co2lines.Count > 1)
            {
                length = lines[0].Length;

                for (int c = 0; c < length && co2lines.Count > 1; c++)
                {
                    int z = 0;
                    int o = 0;

                    string result = "";

                    foreach (var line in co2lines)
                    {
                        if (line[c] == '0') z++;
                        if (line[c] == '1') o++;
                    }

                    if (z > o)
                    {
                        result = "1";
                    }
                    else
                    if (z < o)
                    {
                        result = "0";
                    }
                    else
                    {
                        result = "0";
                    }

                    if (result != "") co2lines.RemoveAll(l => l[c] != result[0]);
                }
            }

            writeLine();
            write(Convert.ToInt32(o2lines[0], 2) * Convert.ToInt32(co2lines[0], 2));
        }
    }
}
