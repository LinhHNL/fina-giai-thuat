using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tse
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<City<Double>> graph = new List<City<Double>>();
            graph.Add(new City<Double>("Ha Noi", 1, 1));
            graph.Add(new City<Double>("Hai Phong", 2, 2));
            graph.Add(new City<Double>("Da Nang", 3, 3));
            graph.Add(new City<Double>("Ho Chi Minh", 4, 4));
            graph.Add(new City<Double>("Can Tho", 5, 5));
            graph.Add(new City<Double>("Hue", 6, 6));
            graph.Add(new City<Double>("Nha Trang", 7, 7));
            graph.Add(new City<Double>("Vung Tau", 8, 8));
            graph.Add(new City<Double>("Da Lat", 9, 9));
            graph.Add(new City<Double>("Quang Ninh", 10, 10));
            graph.Add(new City<Double>("Quang Binh", 11, 11));
            graph.Add(new City<Double>("Quang Tri", 12, 12));
            graph.Add(new City<Double>("Quang Nam", 13, 13));
            graph.Add(new City<Double>("Quang Ngai", 14, 14));
            graph.Add(new City<Double>("Binh Dinh", 15, 15));
            HHO_5LLH<Double> hho = new HHO_5LLH<Double>(graph);
           ( List<int> bestSolution , double fitness) = HHO_5LLH<Double>.HHO_MCF(30, 10);
            foreach (var value in bestSolution)
        {
            Console.Write ($" {value} ");
        }





                                            
        }
    }
}