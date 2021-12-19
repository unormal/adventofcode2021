using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using AdventOfCode;
using Genkit;
using System.Linq;

namespace AdventOfCode._2021
{
    public class _2021_14 : AOCDay
    {
        public override void run()
        {
            var lines = readInput().Replace("\r", "").Split('\n').ToList();

            var mastertemplate = lines[0];
            lines.RemoveAt(0);
            lines.RemoveAt(0);

            List<(string,string)> steps = new List<(string, string)>();
            Dictionary<string,List<string>> transforms = new Dictionary<string, List<string>>();

            Dictionary<string, ulong> paircounts = new Dictionary<string, ulong>();
            Dictionary<string, ulong> nextcounts = new Dictionary<string, ulong>();
            foreach ( var line in lines )
            {
                var parts = line.Replace(" -> ", ",").Split(',');
                transforms.Add(parts[0], new List<string>());
                transforms[parts[0]].Add( parts[0][0].ToString() + parts[1] );
                transforms[parts[0]].Add( parts[1] + parts[0][1].ToString());
                paircounts.Add(parts[0],0);
                nextcounts.Add(parts[0],0);
            }

            for( int tp = 0;tp<mastertemplate.Length-1;tp++ )
            {
                paircounts[mastertemplate.Substring(tp,2)]++;
                nextcounts[mastertemplate.Substring(tp,2)]++;
            }

            for( int x=0;x<40;x++ )
            {
                foreach( var kv in paircounts )
                {
                    if( kv.Value > 0 )
                    {
                        nextcounts[kv.Key] -= kv.Value;
                        foreach (var expansion in transforms[kv.Key])
                        {
                            nextcounts[expansion] += kv.Value;
                        }
                    }
                }

                paircounts = new Dictionary<string, ulong>( nextcounts );
            }

            Dictionary<char,ulong> counts = new Dictionary<char, ulong>();
            foreach( var pair in paircounts )
            {
                if (!counts.ContainsKey(pair.Key[1])) counts.Add(pair.Key[1], 0);
                counts[pair.Key[1]] += pair.Value;
            }

            counts[mastertemplate[0]]++;

            ulong least = ulong.MaxValue;
            ulong most = ulong.MinValue;
            foreach( var kv in counts )
            { 
                write( kv.Key + " = " + kv.Value );
                writeLine();
                if( kv.Value < least ) least = kv.Value;
                if( kv.Value > most ) most = kv.Value;
            }

            write( most - least );
        }
    }
}
