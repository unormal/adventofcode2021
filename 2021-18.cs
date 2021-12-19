using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using AdventOfCode;
using Genkit;
using System.Linq;

namespace AdventOfCode._2021
{
    public class _2021_18 : AOCDay
    {
        public class num
        {
            public long nleft;
            num left = null;

            public long nright;
            num right = null;

            num parent = null;

            int parentCount()
            {
                if( parent == null ) return 0;
                return 1 + parent.parentCount();
            }

            public static num parse( ref string input )
            {
                if( input[0] != '[')
                {
                    throw new Exception();
                }

                num result = new num();

                input = input.Substring(1);
                if( input[0] == '[')
                {
                    result.left = parse(ref input);
                    result.left.parent = result;
                }
                else
                {
                    int next = input.IndexOf(',');
                    result.nleft = Convert.ToInt64(input.Substring(0, next));
                    input = input.Substring(next);
                }

                if( input[0] != ',') throw new Exception();
                input = input.Substring(1);

                if (input[0] == '[')
                {
                    result.right = parse(ref input);
                    result.right.parent = result;
                }
                else
                {
                    int next = input.IndexOf(']');
                    result.nright = Convert.ToInt64(input.Substring(0, next));
                    input = input.Substring(next);
                }

                if (input[0] != ']') throw new Exception();
                input = input.Substring(1);

                return result;
            }

            public num copy( num parent = null )
            {
                num res = new num();
                
                res.parent = parent;
                res.nleft = nleft;
                res.nright = nright;

                res.left = left?.copy(res);
                res.right = right?.copy(res);
               
                return res;
            }

            public num add( num b )
            {
                num newnum = new num();
                this.parent = newnum;
                b.parent = newnum;

                newnum.left = this;
                newnum.right = b;
                
                //Console.Write("before " ); newnum.print();
                newnum.reduce();
                //Console.Write("after "); newnum.print();

                return newnum;
            }


            public bool reduceExplode(Dictionary<int, (num, long, string)> orders)
            {
                if( parentCount() >= 4 )
                {
                    if (left != null) throw new Exception();
                    if (right != null) throw new Exception();

                    if(orders.ContainsKey(leftorder - 1))
                    {
                        if( orders[leftorder - 1].Item3 == "left" )
                        {
                            orders[leftorder - 1].Item1.nleft += nleft;
                        }
                        else
                        {
                            orders[leftorder-1].Item1.nright += nleft;
                        }
                    }


                    if (orders.ContainsKey(rightorder+1) )
                    {
                        if (orders[rightorder + 1].Item3 == "left")
                        {
                            orders[rightorder + 1].Item1.nleft += nright;
                        }
                        else
                        {
                            orders[rightorder + 1].Item1.nright += nright;
                        }
                    }

                    if ( parent.left == this )
                    {
                        parent.left = null;
                        parent.nleft = 0;
                    }

                    if( parent.right == this )
                    {
                        parent.right = null;
                        parent.nright = 0;
                    }

                    return true;
                }

                if (this.left != null && this.left.reduceExplode(orders)) return true;
                if (this.right != null && this.right.reduceExplode(orders)) return true;

                return false;
            }

            public bool reduceSplit(Dictionary<int, (num, long, string)> orders )
            {
                if( left == null && nleft >= 10 )
                {
                    left = new num();
                    left.parent = this;
                    left.nleft = (long)Math.Floor(((double)nleft/(double)2f));
                    left.nright = (long)Math.Ceiling(((double)nleft / (double)2f));

                    nleft = 0;

                    return true;
                }

                if( right == null && nright >= 10 )
                {
                    right = new num();
                    right.parent = this;
                    right.nleft = (long)Math.Floor(((double)nright / (double)2f));
                    right.nright = (long)Math.Ceiling(((double)nright / (double)2f));

                    nright = 0;

                    return true;
                }


                if (this.left != null && this.left.reduceSplit(orders)) return true;
                if (this.right != null && this.right.reduceSplit(orders)) return true;

                return false;
            }

            public void print()
            {
                if (left == null)
                {
                    Console.Write( "[" );
                    Console.Write( nleft );
                    Console.Write( ",");
                }
                else
                {
                    Console.Write("[");
                    left.print();
                    Console.Write(",");
                }

                if (right == null)
                {
                    Console.Write(nright);
                }
                else
                {
                    right.print();
                }

                Console.Write("]");
                if( parent == null ) Console.WriteLine();
            }

            public void order( Dictionary<int,(num, long, string)> orders )
            {
                if( left != null )
                {
                    left.order( orders );
                }

                if( left == null )
                {
                    int n = orders.Count;
                    leftorder = n;
                    orders.Add( n, (this, nleft, "left") );
                }

                if (right != null)
                {
                    right.order(orders);
                }

                if (right == null)
                {
                    int n = orders.Count;
                    rightorder = n;
                    orders.Add(n, (this, nright, "right"));
                }
            }

            int leftorder;
            int rightorder;

            public num reduce()
            {
                again:;
                print();

                var orders = new Dictionary<int, (num, long, string)>();
                order(orders);


                foreach( var num in orders )
                {
                    if( num.Value.Item1 != null && num.Value.Item1.reduceExplode(orders) ) goto again;
                }

                foreach (var num in orders)
                {
                    if (num.Value.Item1 != null && num.Value.Item1.reduceSplit(orders)) goto again;
                }

                return this;
            }

            public long magnitude()
            {
                long leftmag = 0;
                if( left == null )
                {
                    leftmag = nleft;
                }
                else
                {
                    leftmag = left.magnitude();
                }

                long rightmag = 0;
                if( right == null )
                {
                    rightmag = nright;
                }
                else
                {
                    rightmag = right.magnitude();
                }

                return (3*leftmag)+(2*rightmag);
            }
        }

        public override void run()
        {
            var lines = readInput().Replace("\r", "").Split('\n').ToList();

            List<num> nums = new List<num>();

            num answer = null;
            foreach( var line in lines )
            {
                string l = line;
                var next = num.parse(ref l);
                next.reduce();
                
                nums.Add( next.copy() );
                if( answer == null )
                {
                    answer = next.copy();
                }
                else
                {
                    write("  ");
                    answer.print();
                    write("+ ");
                    next.print();
                    write("= ");

                    answer = answer.add(next.copy());
                    answer.print();
                    writeLine();
                }
            }


            answer.print();
            writeLine( answer.magnitude() );
            writeLine();
            writeLine();
            writeLine("--part 2--");

            long max = long.MinValue;

            for( int a=0;a<nums.Count;a++ )
            {
                for( int b=0;b<nums.Count;b++ )
                {
                    if( a != b )
                    {
                        long val = nums[a].copy().add(nums[b].copy()).reduce().magnitude();
                        if (val > max)
                        {
                            max = val;
                        }
                    }
                }
            }

            writeLine(max);
            ;
        }
    }
}
