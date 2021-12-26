using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using AdventOfCode;
using Genkit;
using System.Linq;

namespace AdventOfCode._2021
{
    public class _2021_24 : AOCDay
    {
        public override void run()
        {
            var lines = readInput().Replace("\r", "").Split('\n').ToList();

            long[] vals = new long[4];
            Dictionary<string, long> offset = new Dictionary<string, long>()
            {
                {"w",0 } ,
                {"x",1 } ,
                {"y",2 } ,
                {"z",3 } 
            };

            long getval( string value )
            {
                if( offset.ContainsKey(value))
                {
                    return vals[offset[value]];
                }
                return Convert.ToInt64(value);
            }

            bool debugging = false;
            Random rnd = new Random();
            //for( int p=0;p<14;p++ )
            //{
            // 99
            //string mn = "";

            //a 1
            //b 2
            //c 3
            //d 4
            //e 5
            //f 6
            //g 7
            //h 8
            //i 9
            //j 1
            //k 2
            //l 3
            //m 4
            //n 5
            //o 6 
            //p 7
            //q 8
            //r 9
            //s 1
            //t 2
            //u 3
            //v 4 
            //w 5
            //x 6
            //y 7 
            //z 8

            // add z y 
            // z = (((d0+2)*26)+(d1+13))*26)+(d3+13)

            //string mn = "923357800000000";
                       //123456789ABCDE
            string mn = "81111379141811";


            // d3 = d4
            // d5+2 = d6
            // d2+6 = d7
            // d10-3 = d11
            // d9+7 = d12
            // d1-7 = d14
            // d8-8 = d13

            //for( int test=999999;test>=111111;test--)
            //{


            //mn = test.ToString();
            //Console.WriteLine(mn);
            //var model = Convert.ToInt64(test.ToString()+mn);

            //if (model % 100000 == 0) Console.WriteLine(model);

            for ( int x=0;x<vals.Length;x++ ) vals[x] = 0;

                //if( mn.Contains("0")) continue;

                int ip = 0;
                foreach (var line in lines)
                {
                    var parts = line.Split(" ");

                if( parts[0] == "+") debugging = true;
                bool reversed = true;
                // add z y // z = (((d0+2)*26)+(d1+13))*26)+(d3+13)
                if ( debugging )
                {
                    Console.Clear();
                    Console.SetCursorPosition(0,0);
                    foreach( var pair in offset )
                    Console.WriteLine(pair.Key + "   " + vals[pair.Value] + "            ");
                    Console.WriteLine(         "ip   " + ip);
                    Console.WriteLine();
                    Console.WriteLine("next > " + line + "        ");
                    if( Console.ReadLine() != "" ) debugging = false;
                }

                /*
                if( parts[0] == "?")
                    {
                        if( getval("z") == 1 )
                        {
                            Console.WriteLine("test = " + test);
                            break;
                            ;
                        }
                        else
                        {
                            Console.WriteLine("test = " + test + " NO " + getval("z"));
                            goto again;
                        }
                    }*/

                    if (parts[0] == "inp")
                    {
                        //if( ip > 0 ) Console.WriteLine(vals[offset["z"]]);
                        var input = Convert.ToInt64(mn[ip].ToString());  //Console.ReadLine();
                        ip++;
                        vals[offset[parts[1]]] = Convert.ToInt64(input);
                    }
                    else
                    if (parts[0] == "add")
                    {
                        vals[offset[parts[1]]] = vals[offset[parts[1]]] + getval(parts[2]);
                    }
                    else
                    if( parts[0] == "mov" || parts[0] == "set")
                    {
                        vals[offset[parts[1]]] = getval(parts[2]);
                    }
                    if (parts[0] == "mul")
                    {
                        vals[offset[parts[1]]] = vals[offset[parts[1]]] * getval(parts[2]);
                    }
                    else
                    if (parts[0] == "div")
                    {
                        vals[offset[parts[1]]] = vals[offset[parts[1]]] / getval(parts[2]);
                    }
                    else
                    if (parts[0] == "mod")
                    {
                        vals[offset[parts[1]]] = vals[offset[parts[1]]] % getval(parts[2]);
                    }
                    else
                    if (parts[0] == "eql")
                    {
                        vals[offset[parts[1]]] = (vals[offset[parts[1]]] == getval(parts[2])) ? 1 : 0;
                    }
                    else
                    if( parts[0] == "ret")
                    {
                        break;
                    }
                }
                again:;
            //}
            
            //write( model + " = " + vals[offset["z"]] );
            //Console.WriteLine();

            if ( vals[offset["z"]] == 0)
            {
                write("model:"+mn);
                Console.ReadLine();
            }

            //}
            //939979992969124
            Console.WriteLine(vals[offset["z"]]);

            Console.WriteLine("---");
            foreach (var o in offset)
            {
                Console.WriteLine(o.Key + "   " + vals[o.Value]);
            }
        }

    }
}
