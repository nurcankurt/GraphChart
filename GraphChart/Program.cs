using System;
using System.Collections.Generic;


namespace Program
{
    class Program
    {
        public struct Point
        {
            public Point(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public int X { get; }
            public int Y { get; }
        }
        private static List<Point> ConvertArrayToPoints(int[] array)
        {
            List<Point> coordinates = new List<Point>();
            for (int i = 0; i < array.Length; i++)
            {
                coordinates.Add(new Point(i, array[i]));
            }
            return coordinates;
        }

        
        private static List<Point> Normalize(List<Point> coordinates, int rangeXMin, int rangeXMax, int rangeYMin, int rangeYMax)
        {
            List<Point> normalizedPoints = new List<Point>();
            int minX = coordinates.Min(c => c.X);
            int minY = coordinates.Min(c => c.Y);
            int maxX = coordinates.Max(c => c.X);
            int maxY = coordinates.Max(c => c.Y);


            foreach (var point in coordinates)
            {
                int normalizedX = rangeXMin + (point.X - minX) * (rangeXMax - rangeXMin) / (maxX - minX);
                int normalizedY = rangeYMin + (point.Y - minY) * (rangeYMax - rangeYMin) / (maxY - minY);

                normalizedPoints.Add(new Point(normalizedX, normalizedY));
            }
            return normalizedPoints;

        }
        static void DrawChartLines(int height, int weight)
        {
            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("|");
            }
            for (int i = 0; i < weight; i++)
            {
                Console.SetCursorPosition(i,height);
                Console.Write("-");
            }
        }
        public static void Main()
        {
            int[] originalArray = { 0, 3, 6, 9, 12, 15, 15,12,9,6,3,0,-3,-6,-9,-12,-15,-15,-12,-9 };
            List<Point> coordinates =  ConvertArrayToPoints(originalArray);


            Console.Clear();

            int rangeXMin = Console.CursorLeft + 1;
            int rangeXMax = Console.WindowWidth - 1;
            int rangeYMin = Console.CursorTop + 1;
            int rangeYMax = Console.WindowHeight - 1;


            List<Point> normalizedPoints = Normalize(coordinates, rangeXMin , rangeXMax, rangeYMin , rangeYMax);

            //DrawChartLines(rangeYMax, rangeXMax);



            for (int segmentIndex = 0; segmentIndex < normalizedPoints.Count - 1; segmentIndex++)
            {
                int currentX;

                for (currentX = normalizedPoints[segmentIndex].X; currentX < normalizedPoints[segmentIndex + 1].X; currentX++)
                {
                    Console.SetCursorPosition(currentX, rangeYMax - normalizedPoints[segmentIndex].Y);
                    Console.Write("-");
                }

                int minY, maxY;
                if (normalizedPoints[segmentIndex].Y < normalizedPoints[segmentIndex + 1].Y)
                {
                    minY = normalizedPoints[segmentIndex].Y;
                    maxY = normalizedPoints[segmentIndex + 1].Y;
                }
                else
                {
                    minY = normalizedPoints[segmentIndex + 1].Y;
                    maxY = normalizedPoints[segmentIndex].Y;
                }

                for (int currentY = minY; currentY < maxY; currentY++)
                {
                    Console.SetCursorPosition(currentX, rangeYMax - currentY);
                    Console.Write("|");
                }
            }


            //Console.ForegroundColor = ConsoleColor.Black;
            //Console.BackgroundColor = ConsoleColor.Red;
            // Console.SetCursorPosition( point.X,rangeYMax - point.Y);
            //Console.Write("\u2580");

            Console.ReadKey();

        }
    }
}

