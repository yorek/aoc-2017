using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Day3
{
    class Point: IEquatable<Point>
    {
        public int X;
        public int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override int GetHashCode()        
        {            
            return X ^ Y;        
        }         
        
        public override bool Equals(object obj)        
        {           
            if (!(obj is Point))                
                return false;             
                
            return Equals((Point)obj);        
        }         
        
        public bool Equals(Point other)        
        {            
            if (X != other.X)                
                return false;             
                
            return Y == other.Y;        
        }         
        
        public static bool operator == (Point point1, Point point2)        
        {            
            return point1.Equals(point2);        
        }         
        
        public static bool operator != (Point point1, Point point2)        
        {            
            return !point1.Equals(point2);        
        }    
    }

    class SpiralValue
    {
        public int Value;
        public int SummedValue;
    
        public Point Coordinates = new Point(0, 0);

        public SpiralValue(int value, int x, int y)
        {
            Value = value;
            Coordinates.X = x;
            Coordinates.Y = y;
        }

        public override string ToString()
        {
            return $"{Value:##########} ({Coordinates.X}, {Coordinates.Y})";
        }
    }
    
    class Spiral: IEnumerable
    {        
        List<(int, int)> spiralPatterns = new List<(int, int)>() {
            (1, 0),
            (0, 1),
            (-1, 0),
            (0, -1)
        };
        private int counter = 0;
        private int spiralCounter;
        private int usedLegs = 0;
        private int legLength = 1;
        private int usedLegLegth = 0;
        private Dictionary<int, SpiralValue> values = new Dictionary<int, SpiralValue>();
        private Dictionary<Point, SpiralValue> valuesByPoint = new Dictionary<Point, SpiralValue>();    
        private SpiralValue lastValue;

        public SpiralValue this[int p]
        {
            get { return values[p]; }
        }

        public SpiralValue this[int x, int y]
        {
            get 
            {
                var p = new Point(x,y);
                if (valuesByPoint.ContainsKey(p)) 
                    return valuesByPoint[p];
                else 
                    return null;
            }
        }        

        public void Add()
        {
            if (counter == 0)
            {
                lastValue = new SpiralValue(1, 0, 0);
                lastValue.SummedValue = 1;
                values.Add(1, lastValue);
                valuesByPoint.Add(lastValue.Coordinates, lastValue);
                counter += 1;
                return;
            }
            
            usedLegLegth += 1;

            // if we just entered the leg, increment the number
            if (usedLegLegth == 1) 
                spiralCounter += 1;

            // if leg is finished, increment number of used legs
            // end reset leg used counter
            if (usedLegLegth == legLength) 
            {
                usedLegs += 1;
                usedLegLegth = 0;
            }

            // if two legs has been used, increment leg length by 1
            // and reset number of used legs
            if (usedLegs == 2)
            {
                legLength += 1;
                usedLegs = 0;
            }

            // get X and Y increments                
            if (spiralCounter > 4) spiralCounter -= 4;
            var s = spiralPatterns[spiralCounter - 1];                

            int prevX = 0;
            int prevY = 0;

            if (lastValue != null) 
            {
                prevX = lastValue.Coordinates.X;
                prevY = lastValue.Coordinates.Y;
            }
            
            counter += 1;
            int x = prevX + s.Item1; 
            int y = prevY + s.Item2;
            
            List<SpiralValue> nearValues = new List<SpiralValue>();
            nearValues.Add(valuesByPoint.GetValueOrDefault(new Point(x+1, y)));
            nearValues.Add(valuesByPoint.GetValueOrDefault(new Point(x+1, y+1)));
            nearValues.Add(valuesByPoint.GetValueOrDefault(new Point(x, y+1)));
            nearValues.Add(valuesByPoint.GetValueOrDefault(new Point(x-1, y+1)));
            nearValues.Add(valuesByPoint.GetValueOrDefault(new Point(x-1, y)));
            nearValues.Add(valuesByPoint.GetValueOrDefault(new Point(x-1, y-1)));
            nearValues.Add(valuesByPoint.GetValueOrDefault(new Point(x, y-1)));
            nearValues.Add(valuesByPoint.GetValueOrDefault(new Point(x+1, y-1)));
            
            lastValue = new SpiralValue(counter, x, y);

            int sum = 0;
            foreach(var nv in nearValues)
            {
                if (nv != null) sum += nv.SummedValue;
            }

            lastValue.SummedValue = sum;
            
            valuesByPoint.Add(lastValue.Coordinates, lastValue);
            values.Add(counter, lastValue);
        }

        public IEnumerator GetEnumerator()
        {
            foreach(var kvp in values)
            {
                yield return kvp.Value;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Spiral s = new Spiral();
            int t = 312051;

            for(int i=0; i<t; i++)
            {
                s.Add();
            }

            /* Part 1 */
            var sp = s[t]; 
            int d = Math.Abs(sp.Coordinates.X) + Math.Abs(sp.Coordinates.Y);
            Console.WriteLine($"{sp} - D: {d}");    
            
            /* Part 2 */
            foreach(SpiralValue sp2 in s)
            {
                Console.WriteLine($"{sp2} -> {sp2.SummedValue}");
                if (sp2.SummedValue > t) break;
            }
        }
    }
}
