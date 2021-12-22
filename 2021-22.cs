using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using AdventOfCode;
using Genkit;
using System.Linq;

namespace AdventOfCode._2021
{
    public class _2021_22 : AOCDay
    {
        class cube
        {
            public string step;
            public cube()
            {

            }

            public cube( Point3DL min, Point3DL max )
            {
                this.min = min;
                this.max = max;
            }

            public Point3DL min;
            public Point3DL max;

            public override string ToString()
            {
                return min.ToString() + "->" + max.ToString();

            }

            public long size()
            {
                return (max.x-min.x+1)*(max.y-min.y+1)*(max.z-min.z+1);
            }

            bool overlaps(cube other)
            {
                return (max.x >= other.min.x && min.x <= other.max.x && max.y >= other.min.y && min.y <= other.max.y && max.z >= other.min.z && min.z <= other.max.z );
            }

            public List<cube> subtract(cube other)
            {
                List<cube> result = new List<cube>();

                if( !overlaps(other))
                {
                    result.Add(this);
                }
                else
                {
                    // x and y partition

                    if( other.min.y > min.y )
                    {
                        // north slice
                        var next = new cube( new Point3DL(min.x, min.y, min.z ), new Point3DL(max.x, Math.Min(max.y,other.min.y-1), max.z) );
                        if( next.size() > 0 ) result.Add(next);
                    }

                    if (other.max.y < max.y)
                    {
                        // south slice
                        var next = new cube(new Point3DL(min.x, other.max.y+1, min.z), new Point3DL(max.x, max.y, max.z));
                        if (next.size() > 0) result.Add(next);
                    }

                    if (other.max.x < max.x )
                    {
                        // east slice
                        var next = new cube(new Point3DL( Math.Max(min.x, other.max.x+1), Math.Max(min.y,other.min.y), min.z), new Point3DL(max.x, Math.Min(max.y,other.max.y), max.z));
                        if (next.size() > 0) result.Add(next);
                    }

                    if (other.min.x > min.x)
                    {
                        // west slice
                        var next = new cube(new Point3DL(min.x, Math.Max(min.y, other.min.y), min.z), new Point3DL( Math.Min(max.x, other.min.x-1), Math.Min(max.y, other.max.y), max.z));
                        if (next.size() > 0) result.Add(next);
                    }

                    if ( other.min.z > min.z )
                    {
                        // top chunk
                        var next = new cube(new Point3DL(Math.Max(min.x,other.min.x), Math.Max(min.y,other.min.y), min.z), new Point3DL( Math.Min(max.x, other.max.x), Math.Min(max.y, other.max.y), Math.Min(max.z,other.min.z-1) ) );
                        if (next.size() > 0) result.Add(next);
                    }

                    if( other.max.z < max.z )
                    {
                        // bottom chunk
                        var next = new cube(new Point3DL(Math.Max(min.x, other.min.x), Math.Max(min.y, other.min.y), Math.Min(other.max.z+1, max.z)), new Point3DL(Math.Min(max.x, other.max.x), Math.Min(max.y, other.max.y), max.z));
                        if (next.size() > 0) result.Add(next);
                    }
                }

                return result;
            }
        }


        public override void run()
        {
            var lines = readInput().Replace("\r", "").Split('\n').ToList();

            List<(string, int,int, int,int, int,int)> steps = new List<(string, int, int, int, int, int, int)>();

            Dictionary<(int,int,int), bool> cube = new Dictionary<(int, int, int), bool>();

            int minx = int.MaxValue;
            int maxx = int.MinValue;

            int miny = int.MaxValue;
            int maxy = int.MinValue;

            int minz = int.MaxValue;
            int maxz = int.MinValue;


            List<cube> oncubes = new List<cube>();
            List<cube> offcubes = new List<cube>();

            foreach ( var line in lines )
            {
                var l = line.Replace("..", ".").Replace("x=","").Replace("y=","").Replace("z=","");
                var p1 = l.Split(' ');
                string on = p1[0];

                var p2 = p1[1].Split(',');

                var newcube = new cube();

                newcube.min = new Point3DL( 
                    Convert.ToInt64(p2[0].Split('.')[0]),
                    Convert.ToInt64(p2[1].Split('.')[0]),
                    Convert.ToInt64(p2[2].Split('.')[0]) );

                newcube.max = new Point3DL(
                    Convert.ToInt64(p2[0].Split('.')[1]),
                    Convert.ToInt64(p2[1].Split('.')[1]),
                    Convert.ToInt64(p2[2].Split('.')[1]));


                newcube.step = on;

                oncubes.Add(newcube);
            }

            long totalcubes = 0;

            List<cube> used = new List<cube>();

            long total = 0;

            foreach (var next in oncubes)
            {
                List<cube> cubes = new List<cube>();
                foreach (var u in used)
                {
                    cubes.AddRange(u.subtract(next));
                }
                if( next.step == "on" ) cubes.Add(next);
                used = cubes;

                total = 0;
                foreach (var c in used) total += c.size();
                writeLine("step total: " + total);
            }

            total = 0;
            foreach( var c in used ) total += c.size();
            
            writeLine("total: " + total);
        }
    }
}
