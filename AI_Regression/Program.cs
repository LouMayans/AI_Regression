using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI_K_Means;
using System.Numerics;
namespace AI_Regression
{
    class Program
    {
        static Point[] points;

        static int numberOfPoints = 7;
        static void Main(string[] args)
        {
            //Generates points;
            points = PointsGenerator.GeneratePoints(numberOfPoints, 10, 20);

            for (int i = 0; i < points.GetLength(0); i++)
            {
                Console.WriteLine("Point {0} = ({1},{2})", i, points[i].position.X, points[i].position.Y);
            }

            //mean of X and Y
            Vector2 meanOfPoints = Vector2.Zero;
            meanOfPoints.X = points.Sum(point => point.position.X) / numberOfPoints;
            meanOfPoints.Y = points.Sum(point => point.position.Y) / numberOfPoints;

            // (Xi - MeanX) || (Yi - MeanY)
            Vector2[] ab = new Vector2[numberOfPoints];
            for (int i = 0; i < numberOfPoints; i++)
            {
                ab[i].X = points[i].position.X - meanOfPoints.X;
                ab[i].Y = points[i].position.Y - meanOfPoints.Y;
            }

            //(Xi - MeanX)^2 || (Yi - MeanY)^2
            Vector2[] a2b2 = new Vector2[numberOfPoints];
            for (int i = 0; i < numberOfPoints; i++)
            {
                a2b2[i].X = ab[i].X * ab[i].X;
                a2b2[i].Y = ab[i].Y * ab[i].Y;
            }

            // c = ab.X * ab.Y
            float[] c = new float[numberOfPoints];
            for (int i = 0; i < numberOfPoints; i++)
                c[i] = ab[i].X * ab[i].Y;

            //Sum c
            float sumC = c.Sum(x => x);

            // Sum(abi.x^2) Sum(abi.y^2)
            Vector2 sumOfa2b2 = Vector2.Zero;
            sumOfa2b2.X = a2b2.Sum(x => x.X);
            sumOfa2b2.Y = a2b2.Sum(x => x.Y);

            //Sum A and B multiplied
            float mult = (float)Math.Sqrt(sumOfa2b2.X) * (float)Math.Sqrt(sumOfa2b2.Y);

            //Correlation R
            float r = sumC / mult;

            //Sum product
            float sumProductOfMeans = 0;
            sumProductOfMeans = points.Sum(point => point.position.X * point.position.Y) / numberOfPoints;

            float m = (meanOfPoints.X * meanOfPoints.Y - sumProductOfMeans) / (meanOfPoints.X * meanOfPoints.X - (points.Sum(point => point.position.X * point.position.X))/numberOfPoints);

            float yInt = meanOfPoints.Y - m * meanOfPoints.X;
            Console.WriteLine("Correlation r = {0}", r);
            Console.WriteLine("y = {0}x + {1}", m, yInt);
            Console.Read();
        }
    }
}
