using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using AdventOfCode;
using Genkit;
using System.Linq;

namespace AdventOfCode._2021
{
    public class _2021_16 : AOCDay
    {

        public override void run()
        {
            var lines = readInput().Replace("\r", "").Split('\n').ToList();

            Dictionary<string, string> bits = new Dictionary<string, string>()
            {
                { "0", "0000" },
                { "1", "0001" },
                { "2", "0010" },
                { "3", "0011" },
                { "4", "0100" },
                { "5", "0101" },
                { "6", "0110" },
                { "7", "0111" },
                { "8", "1000" },
                { "9", "1001" },
                { "A", "1010" },
                { "B", "1011" },
                { "C", "1100" },
                { "D", "1101" },
                { "E", "1110" },
                { "F", "1111" }
            };

            long stoi(string binary)
            {
                return Convert.ToInt64(binary, 2);
            }

            string msg = "";
            foreach( char c in lines[0])
            {
                msg += bits[c.ToString()];
            }

            string take( int n )
            {
                string result = msg.Substring(0,n);
                msg = msg.Substring(n);
                return result;
            }

            long itake(int n)
            {
                return stoi( take(n));
            }



                int opcount = 0;

                long allver = 0;


            Queue<string> calc = new Queue<string>();

            nextpacket:;


                if (msg.All(c => c == '0')) goto done;

                long version = itake(3);
                long id = itake(3);
                allver += version;
                int bit = 6;
                
                if (id == 4) // literal value
                {
                    string val = "";


                    again:;
                    string start = take(1);
                    string next = take(4);
                    bit += 5;

                    val += next;
                    if (start == "1") goto again;

                    long result = stoi(val);
                    

                    //Console.WriteLine("literal " + result);

                    calc.Enqueue("#:"+result.ToString() + ":-:" + bit);
                    if( msg.Length > 0 ) goto nextpacket;
                }
                else
                {
                
                    string op = "";
                    if( id == 0 ) op = "+";
                    if( id == 1 ) op = "*";
                    if( id == 2 ) op = "m";
                    if( id == 3 ) op = "M";
                    if( id == 5 ) op = ">";
                    if( id == 6 ) op = "<";
                    if( id == 7 ) op = "=";

                    long lengthtype = itake(1);
                    bit += 1;
                    if( lengthtype == 0 ) // 15 bits, total length in bits
                    {
                        long subbits = itake(15);
                        bit += 15;
                        //Console.WriteLine("subpacket len " + subbits);
                        calc.Enqueue(op + ":b:"+subbits+":"+bit);
                        goto nextpacket;
                        ;
                    }
                    else
                    if( lengthtype == 1 ) // 11 bits, number of sub packets
                    {
                        long npackets = itake(11);
                        bit += 11;
                        //Console.WriteLine("subpacket count " + npackets);
                        calc.Enqueue(op + ":p:" + npackets + ":" + bit);
                        goto nextpacket;
                    ;
                }


            }
            done:;


            long doCalc()
            {
                var next = calc.Dequeue();
                //writeLine(next);

                var parts = next.Split(":");
                
                if( parts[0] == "#" )
                {
                    Console.WriteLine("literal " + parts[1]);
                    return Convert.ToInt64(parts[1]);
                }

                long result = 0;

                List<long> nums = new List<long>();
                if ( parts[1] == "p")
                {

                    for( int x=0;x<Convert.ToInt32(parts[2]);x++ )
                    {
                        nums.Add(doCalc());
                    }
                }
                else
                if( parts[1] == "b")
                {
                    int bitcount = Convert.ToInt32(parts[2]);
                    

                    int howmany = 0;
                    var clist = calc.ToList();
                    for( int x=0;x<clist.Count;x++ )
                    {
                        bitcount -= Convert.ToInt32(clist[x].Split(':')[3]);
                        if( bitcount < 0 )
                        {
                            ;
                        }
                        else
                        if( bitcount == 0 )
                        {
                            howmany = x+1;
                        }
                    }

                    howmany = calc.Count-howmany;
                    while( calc.Count > howmany )
                    {
                        nums.Add(doCalc());
                    }
                }

                if( parts[0] == "+")
                {
                    result = 0;
                    nums.ForEach( n => result += n );
                }

                if (parts[0] == "*")
                {
                    result = nums[0];
                    for( int x=1;x<nums.Count;x++ ) result *= nums[x];
                }

                if( parts[0] == "m")
                {
                    nums.Sort();
                    result = nums.First();
                }

                if (parts[0] == "M")
                {
                    nums.Sort();
                    result = nums.Last();
                }

                if( parts[0] == ">")
                {
                    if( nums[0] > nums[1] )
                    {
                        result = 1;
                    }
                    else
                    {
                        result = 0;
                    }
                }

                if (parts[0] == "<")
                {
                    if (nums[0] < nums[1])
                    {
                        result = 1;
                    }
                    else
                    {
                        result = 0;
                    }
                }


                if (parts[0] == "=")
                {
                    if (nums[0] == nums[1])
                    {
                        result = 1;
                    }
                    else
                    {
                        result = 0;
                    }
                }

                Console.WriteLine( parts[0] + " result " + result);

                return result;
            }

            writeLine( "result = " + doCalc());

            writeLine("-done-");
        }
    }
}
