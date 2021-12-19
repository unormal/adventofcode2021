using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using AdventOfCode;
using Genkit;
using System.Linq;

namespace AdventOfCode._2021
{
    public class _2021_19 : AOCDay
    {
        public override void run()
        {
            var lines = readInput().Replace("\r", "").Split('\n').ToList();

            Dictionary<int, List<Point3D>> scanners = new Dictionary<int, List<Point3D>>();

            int min = int.MaxValue;
            int max = int.MinValue;

            int cur = -1;
            foreach( var line in lines )
            {
                if( line.Length == 0 ) continue;

                if( line.Contains("--"))
                {
                    var l = line.Replace("-", "");
                    l = l.Replace(" ", "");
                    l = l.Replace("scanner", "");
                    cur = Convert.ToInt32(l);
                    scanners.Add(cur,new List<Point3D>());
                }
                else
                {
                    var parts = line.Split(',');
                    var point = new Point3D(Convert.ToInt32(parts[0]), Convert.ToInt32(parts[1]), Convert.ToInt32(parts[2]));
                    scanners[cur].Add( point );

                    if( point.x < min ) min = point.x;
                    if (point.y < min) min = point.y;
                    if (point.z < min) min = point.z;

                    if (point.x > max) max = point.x;
                    if (point.y > max) max = point.y;
                    if (point.z > max) max = point.z;
                }
            }

            int nScanners = scanners.Count;
    
            List<List<Point3D>> solved = new List<List<Point3D>>();
            //List<int> remaining = new List<int>();

            List<int> remaining = new List<int> { 17, 10, 23, 11, 7, 6, 14, 8, 1, 3, 5, 2, 13, 15, 18, 19, 21, 22, 9, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 4, 12, 14, 16, 20 };
            for( int x=1;x<nScanners;x++ ) 
            {
                if( !remaining.Contains(x))
                {
                    writeLine(x);
                    remaining.Add(x);
                }
            }

            // shift scanner 0 to 0,0,0
            for ( int x=scanners[0].Count-1;x>=0;x-- )
            {
                //scanners[0][x] = scanners[0][x].subtract( scanners[0][0] );
            }
            solved.Add(scanners[0]);

            List<Point3D> scannerLocations = new List<Point3D>();

            while ( remaining.Count > 0 )
            {
                foreach( var testingScanner in remaining )
                {
                    var s = scanners[testingScanner];

                    List<Point3D> result = new List<Point3D>();
                    bool match = false;
                    writeLine("testing: " + testingScanner);
                    for( int xrot=0;xrot<28;xrot++ )
                    {
                        result.Clear();

                        foreach( var sourcePoint in s )
                        {
                            var transformPoint = new Point3D(sourcePoint);
                            string sequence = "XRTTTRTTTRTTTRTRRTTTRTTTRTTT";

                            for( int x=0;x<sequence.Length && x < xrot;x++ )
                            {
                                if( sequence[x] == 'R')
                                {
                                    int oldx = transformPoint.x;
                                    int oldy = transformPoint.y;
                                    int oldz = transformPoint.z;

                                    transformPoint.x = oldx;
                                    transformPoint.y = oldz;
                                    transformPoint.z = -oldy;
                                }

                                if( sequence[x] == 'T')
                                {
                                    int oldx = transformPoint.x;
                                    int oldy = transformPoint.y;
                                    int oldz = transformPoint.z;

                                    transformPoint.x = -oldy;
                                    transformPoint.y = oldx;
                                    transformPoint.z = oldz;
                                }
                            }

                            result.Add(transformPoint);
                        }

                        if( result[5].z == 8 && result[5].y == 0 && result[5].z == 7 )
                        {
                            ;
                        }
                        foreach (var solve in solved)
                        {
                            foreach( var solvezero in solve )
                            {
                                for (int p = 0; p < result.Count; p++)
                                {
                                    List<Point3D> testPoints = new List<Point3D>();
                                    Point3D zero = new Point3D(result[p]);
                                    for (int p2 = 0; p2 < result.Count; p2++)
                                    {
                                        testPoints.Add( result[p2].subtract(zero).add(solvezero) );
                                    }

                                    var lineup = solve.Where( p => testPoints.Any( p2 => p2.Equals( p ) ) ).ToList().Count;
                                    if(lineup < 1)
                                    {
                                        write( "bad alignment!!!" );
                                        ;
                                    }

                                    if( lineup >= 6 )
                                    {
                                        writeLine("GOOD ALIGNMENT on " + testingScanner);
                                        match = true;

                                        result = testPoints;

                                        scannerLocations.Add(solvezero.subtract(zero));

                                        HashSet<Point3D> used2 = new HashSet<Point3D>();
                                        List<Point3D> unique2 = new List<Point3D>();
                                        foreach (var set in solved)
                                        {
                                            foreach (var e in set)
                                            {
                                                if (!used2.Contains(e))
                                                {
                                                    used2.Add(e);
                                                    unique2.Add(e);
                                                }
                                            }
                                        }

                                        writeLine("total unique: " + unique2.Count);
                                        goto match;
                                    }
                                }
                            }

                        }
                    }

                    match:;
                    if(match)
                    {
                        remaining.Remove(testingScanner);
                        solved.Add(result);

                        goto again;
                    }

                    //foreach orientation
                    //
                }
                again:;
            }

            HashSet<Point3D> used = new HashSet<Point3D>();
            List<Point3D> unique = new List<Point3D>();
            foreach( var set in solved )
            {
                foreach( var e in set )
                {
                    if( !used.Contains(e))
                    {
                        used.Add(e);
                        unique.Add(e);
                    }
                }
            }

            int maxdistance = int.MinValue;
            for( int x=0;x< scannerLocations.Count;x++ )
                for( int y=x+1;y< scannerLocations.Count;y++ )
                {
                    int distance = scannerLocations[x].ManhattanDistance(scannerLocations[y]);
                    if( distance > maxdistance ) maxdistance = distance;
                }
            {

            }

            writeLine("total unique: " + unique.Count );
            writeLine("max distnace: " + maxdistance);
            ;


        }
    }
}
