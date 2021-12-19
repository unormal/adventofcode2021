using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using AdventOfCode;
using Genkit;
using System.Linq;

namespace AdventOfCode._2021
{
    public class _2021_17 : AOCDay
    {

        public override void run()
        {
            var lines = readInput().Replace("\r", "").Split('\n').ToList();


            
            int tx1 = 206;
            int tx2 = 250;
            int ty1 = -105;
            int ty2 = -57; 
            

            /*
            int tx1 = 20;
            int tx2 = 30;
            int ty1 = -10;
            int ty2 = -5;
            */

            int x = 0;
            int y = 0;

            int vx = 6;
            int vy = 9;

            int besty = 0;

            int found = 0;
            for ( int tx=-1000;tx<1000;tx++ )
            {
                for( int ty=-1000;ty<1000;ty++ )
                {
                    vx = tx;
                    vy = ty;
                    x=0;
                    y=0;

                    bool hit = false;
                    int maxy = 0;
                    int steps = 0;

                    while (true)
                    {
                        if( y < ty1 ) break;
                        if( x > tx2 ) break;

                        steps++;
                        if( x >= tx1 && x <= tx2 )
                        {
                            if( y >= ty1 && y <= ty2 )
                            {
                                hit = true;
                                break;
                            }
                        }

                        if( y > maxy ) maxy = y;

                        x += vx;
                        y += vy;

                        if (y > maxy) maxy = y;

                        if (y > maxy) y++;
                        if (vx > 0) vx--;
                        vy--;

                    }

                    if( hit )
                    {
                        if( maxy > besty) besty = maxy;
                        found++;
                    }
                }

            }


            writeLine(besty);
            writeLine(found);
        }
    }
}
