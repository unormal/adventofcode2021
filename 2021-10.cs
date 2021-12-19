using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using AdventOfCode;
using Genkit;
using System.Linq;

namespace AdventOfCode._2021
{
    public class _2021_10 : AOCDay
    {
        public override void run()
        {
            var lines = readInput().Replace("\r", "").Split('\n');

            List<long> scores = new List<long>();
            //int score = 0;
            foreach ( var line in lines )
            {
                int brack = 0; //[
                int paren = 0; // (
                int brace = 0; // {
                int angle = 0; // <

                Stack<char> expect = new Stack<char>();
                bool good = true;

                for ( int cp = 0;cp<line.Length;cp++ )
                {
                    var c = line[cp];
                    
                    if( c == '[' ) expect.Push(']');
                    if( c == '(' ) expect.Push(')');
                    if( c == '{') expect.Push('}');
                    if( c == '<') expect.Push('>');

                    if (c == ']') if( expect.Pop() != ']') good = false;
                    if (c == ')') if (expect.Pop() != ')') good = false;
                    if (c == '}') if (expect.Pop() != '}') good = false;
                    if (c == '>') if (expect.Pop() != '>') good = false;

                    if ( !good ) 
                    {
                        /*char bad = line[cp];
                        if( bad == ')') score += 3;
                        if( bad == ']') score += 57;
                        if(  bad == '}') score += 1197;
                        if( bad == '>') score += 25137; */
                        break;
                    }
                }

                if( good )
                {
                    long score = 0;
                    while( expect.Count > 0 )
                    {
                        char next = expect.Pop();
                        score *= 5;
                        if( next == ')') score += 1;
                        if (next == ']') score += 2;
                        if (next == '}') score += 3;
                        if (next == '>') score += 4;
                    }
                    scores.Add(score);
                }
            }

            scores.Sort();
             writeLine(scores[(scores.Count/2)]);
        }
    }
}
