using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace tse
{
    public class Program
    {
        public static void Main(string[] args)
        {
                      List<string> files = new List<string>();
// Khởi tạo file csv
            using (var csvWriter = new StreamWriter("datarllh.csv"))
            {
                // Viết header
                csvWriter.WriteLine("Name, Time (nanos)");
            // Duyệt qua tất cả các file trong folder dataset
            foreach (var file in Directory.GetFiles("datasets", "*.txt"))
                {
                    // Tên file
                    string fileName = Path.GetFileName(file);

                    // Đọc file và tạo graph
                    List<City<double>> graph = new List<City<double>>();
                    using (var reader = new StreamReader(file))
                    {
                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            var data = line.Split(" ");
                            Double X = Double.Parse(data[1]);
                            Double Y = Double.Parse(data[2]);
                            // Console.WriteLine(data[0] + " " +data[1] + " " +data[2]);
                            // Console.WriteLine(data[0] + " " +X + " " +Y);
                            graph.Add(new City<Double>(data[0], X, Y));
                        }
                    }
                    // Khởi tạo stopwatch
                    Stopwatch stopwatch = new Stopwatch();

                    // Bắt đầu tính thời gian
                    stopwatch.Start();
                    HHO_5LLH<double> hho = new HHO_5LLH<double>(graph);
                // Chạy thuật toán
                    (List<int> bestSolution, double fitness) = HHO_5LLH<double>.HHO5LLH(30, 10);
                        stopwatch.Stop();
                    csvWriter.WriteLine($"{fileName}, {stopwatch.ElapsedTicks}");

                }
            }

            }


                                            
        }
    
}