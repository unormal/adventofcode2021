using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using AdventOfCode;
using Genkit;
using System.Linq;

namespace AdventOfCode._2021
{
    public class _2021_23 : AOCDay
    {
        public class map
        {

        }

        public override void run()
        {
            var lines = readInput().Replace(" ", "#").Replace("\r", "").Split('\n').ToList();

            string map = readInput().Replace("\r\n","");
            
            int w = lines[0].Length;
            int h = lines.Count;

            StringBuilder sb = new StringBuilder();
            Dictionary<string,int> visited = new Dictionary<string, int>();
            List<(string,int)> stack = new List<(string, int)>();

            int pinoffset = w*h;
            map += map;

            print(map);
            stack.Add((map, 0));

            string target = "#############" +
                            "#...........#" +
                            "###A#B#C#D###" +
                            "###A#B#C#D###" +
                            "###A#B#C#D###" +
                            "###A#B#C#D###" +
                            "#############";

            string rules = "#############"+
                           "#.._._._._..#"+
                           "###A#B#C#D###"+
                           "###A#B#C#D###"+
                           "###A#B#C#D###" +
                           "###A#B#C#D###" +
                           "#############";

            
            int lowest = int.MaxValue;

            int score( (string,int) a, (string,int) b  )
            {
                return b.Item2 - a.Item2;
            }

            List<(int,int)> EnumerateValidMoves( string map, int pos )
            {
                var adj = new List<(int,int)>();
                                
                bool valid( string map, int pos1, int pos2 )
                {
                    if( map[pos1+ pinoffset] == 'X') return false; // don't move if you're pinned
                                        
                    if( rules[pos1] == map[pos1] && ((map[pos1+w] == '.' && map[pos+w+w] == '#') || (map[pos1 + w] == '.' && (map[pos1 + w + w] == '.' || map[pos1 + w + w] == map[pos1]) && map[pos + w + w + w] == '#') || (map[pos1 + w] == '.' && (map[pos1 + w + w] == '.' || map[pos1 + w + w] == map[pos1]) && (map[pos1 + w + w + w] == '.' || map[pos1 + w + w + w] == map[pos1]) && map[pos + w + w + w + w] == '#')) )
                    {
                        return pos2 == pos1+w;
                    }

                    if( rules[pos1] == map[pos1] && map[pos1 + w] == '#' ) return false; // don't ever move if you match the end
                    if ( rules[pos1] == map[pos1] && map[pos1+w] == map[pos1]) return false; // don't ever move if you're locked in

                    if( rules[pos2] == map[pos1] && map[pos2] == '.' ) // your hallway, check if it's occupied
                    {
                        if( map[pos2+w] == '.' || map[pos2+w] == map[pos1] ) return true;
                    }

                    if ( (map[pos2] == '.' && (rules[pos2] == '_' || rules[pos2] == '.' || (rules[pos1] != '.' && rules[pos1] != '_' && rules[pos2] != map[pos1]))) ) return true; // can move into hall

                    return false;
                }

                if( pos < map.Length-1 && map[pos+1] == '.' && valid(map,pos,pos+1) ) adj.Add((pos,pos+1));
                if( pos > 0 && map[pos - 1] == '.' && valid(map, pos, pos - 1)) adj.Add((pos,pos-1));
                if( pos < map.Length-w && map[pos + w] == '.' && valid(map, pos, pos + w)) adj.Add((pos,pos+w));
                if( pos > w && map[pos - w] == '.' && valid(map, pos, pos - w)) adj.Add((pos,pos-w));

                return adj;
            }
            
            void print( string map )
            {
                for( int y=0;y<h;y++ )
                {
                    writeLine(map.Substring(w*y, w));
                }

                for( int x=0;x<w*h;x++ )
                {
                    if( map[x+pinoffset] == 'X')
                    {
                        //Console.SetCursorPosition(x%w,x/w);
                        //Console.Write("X");
                    }
                }
            }

            string Swap(string map, int p1, int p2)
            {
                sb.Clear();
                sb.Append(map);
                var s1 = sb[p1];
                sb[p1] = sb[p2];
                sb[p2] = s1;
                return sb.ToString();
            }

            int n = 0;
            List<(int,int)> pos = new List<(int,int)>();
            while ( stack.Count > 0 )
            {
                //stack.Sort( score );
                var next = stack.Last();
                stack.RemoveAt(stack.Count-1);

                //Console.SetCursorPosition(0,0);
                //print( next.Item1 );

                // check for direct solution, take it!

                if( next.Item2 >= 46310) continue;
                for (int hall = 1 + w; hall < 11 + w; hall++)
                {
                    if (next.Item1[hall] != '.')
                    {
                        int holcol = 3;
                        if( next.Item1[hall] == 'A') holcol = 3;
                        if (next.Item1[hall] == 'B') holcol = 5;
                        if (next.Item1[hall] == 'C') holcol = 7;
                        if (next.Item1[hall] == 'D') holcol = 9;

                        if (next.Item1[holcol + w +w] == '.')
                        {
                            if ( next.Item1[holcol+w+w+w] == '.' || next.Item1[holcol + w + w+w] == next.Item1[hall] )
                            {
                                bool clear = true;
                                if( hall == holcol+w )
                                {
                                    clear = true;
                                    // just move in
                                }
                                else
                                if( hall > holcol+w )
                                {
                                    for( int check=hall-1; check <= holcol + w; check--)
                                    {
                                        if( next.Item1[check] != '.')
                                        {
                                            clear = false;
                                            break;
                                        }
                                    }
                                    //
                                }
                                else
                                {
                                    for (int check = hall + 1; check <= holcol + w; check++)
                                    {
                                        if (next.Item1[check] != '.')
                                        {
                                            clear = false;
                                            break;
                                        }
                                    }
                                }

                                if( clear )
                                {
                                    //Console.SetCursorPosition(0, 0);
                                    //print(next.Item1);
                                    //Console.ReadLine();

                                    // unpin
                                    sb.Clear();
                                    sb.Append(next.Item1);
                                    sb[hall + pinoffset] = '.';
                                    next = (sb.ToString(),next.Item2);
                                }
                            }
                        }
                    }
                }

                if ( n++ % 1000 == 0 )
                {
                    Console.SetCursorPosition(20,20);
                    Console.WriteLine(next.Item2);
                }

                if( next.Item1.Substring(0,target.Length) == target && next.Item2 < lowest )
                {
                    Console.SetCursorPosition(41,41);
                    Console.WriteLine("Found:"+next.Item2+ "     ");
                    lowest = next.Item2;
                    ;
                }

                if( visited.ContainsKey(next.Item1) && visited[next.Item1] <= next.Item2 ) continue;
                
                if( visited.ContainsKey( next.Item1 ))
                {
                    visited[next.Item1] = next.Item2;
                }
                else
                {
                    visited.Add(next.Item1, next.Item2);
                }
                tryagain:;
                pos.Clear();

                bool haveToMove = false;
                for (int x = 0; x < next.Item1.Length/2; x++)
                {
                    if (next.Item1[x] == 'A' && rules[x] == '_' ) { haveToMove = true; pos.AddRange(EnumerateValidMoves(next.Item1, x)); break; }
                    if (next.Item1[x] == 'B' && rules[x] == '_' ) { haveToMove = true; pos.AddRange(EnumerateValidMoves(next.Item1, x)); break; };
                    if (next.Item1[x] == 'C' && rules[x] == '_' ) { haveToMove = true; pos.AddRange(EnumerateValidMoves(next.Item1, x)); break; };
                    if (next.Item1[x] == 'D' && rules[x] == '_' ) { haveToMove = true; pos.AddRange(EnumerateValidMoves(next.Item1, x)); break; };
                }

                if( !haveToMove)
                {
                    for (int x = 0; x < next.Item1.Length/2; x++)
                    {
                        if (next.Item1[x] == 'A') pos.AddRange(EnumerateValidMoves(next.Item1, x));
                        if (next.Item1[x] == 'B') pos.AddRange(EnumerateValidMoves(next.Item1, x));
                        if (next.Item1[x] == 'C') pos.AddRange(EnumerateValidMoves(next.Item1, x));
                        if (next.Item1[x] == 'D') pos.AddRange(EnumerateValidMoves(next.Item1, x));
                    } 
                }

                //Console.SetCursorPosition(0,0);
                //print( next.Item1 );

                foreach ( var p in pos )
                {
                    //Console.SetCursorPosition(p.Item2%w, p.Item2/w);
                    /*
                    if( p.Item1+1 == p.Item2) Console.Write(">");
                    if (p.Item1 - 1 == p.Item2) Console.Write("<");
                    if (p.Item1 + w == p.Item2) Console.Write("V");
                    if (p.Item1 - w == p.Item2) Console.Write("^");
                    */
                    int cost = 0;

                    if( next.Item1[p.Item1] == 'A' ) cost = 1;
                    if( next.Item1[p.Item1] == 'B' ) cost = 10;
                    if( next.Item1[p.Item1] == 'C' ) cost = 100;
                    if( next.Item1[p.Item1] == 'D' ) cost = 1000;

                    var child = Swap(next.Item1, p.Item1, p.Item2 );

                    for( int hall=1+w;hall<11+w;hall++ )
                    {
                        if(child[hall] != '.' && hall != p.Item2 )
                        {
                            sb.Clear();
                            sb.Append(child);
                            sb[hall+pinoffset] = 'X';
                            child = sb.ToString();
                        }
                    }

                    if (!visited.ContainsKey(child) || visited[child] > next.Item2 + cost)
                    {
                        stack.Add((child, next.Item2 + cost));
                    }
                }
                
                /*
                string key = Console.ReadLine();
                if( key != "" ) 
                {
                    goto tryagain;
                 }*/
            }

            Console.WriteLine("lowest: " + lowest);
            ;

        }
    }
}
