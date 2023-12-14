using System;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Diagnostics;
class HHOAlgorithm<T> {
    
    private    static List<ILLHFunctionInterface> neighborhoodfunctions = new List<ILLHFunctionInterface>()
        {
            new RandomDoubleCycles(),//success
            new RandomInsertion(),// success
            new RandomInsertionDoubleCycles(),//success
            new RandomInsertionSubsequence(),//success
            new RandomNestedCycles(),//success
            new RandomReversingSubsequence(),//success
            new RandomReversingSwapSubsequence(),//success
            new RandomSwap(),//success
            new RandomSwapDoubleCycles(),//success
            new RandomSwapSubsequence(),//success
        };
        //Khởi tạo danh sách các hàm llh 
    private static List<City<T>> graph = new List<City<T>>();
    //Danh sách các thành phố
  public HHOAlgorithm(List<City<T>> graph)
    {
        HHOAlgorithm<T>.graph = graph;
    }

   
    
     static List<int> RandomKeyEncode(List<double> tour)
    {
        //Mô tả: Hàm này dùng để chuyển danh sánh các thành phố trong không gian liên tục chuyển sang không gian rời rạc
        //Input :
        //      tour: danh sách các thành phố trong không gian liên tục
        //Output:
        //      List<int> danh sách các thành phố trong không gian rời rạc
        List<int> encoded = Enumerable.Range(0, tour.Count)
            .OrderBy(i => tour[i])
            .ToList();
        return encoded;
    }

    static List<double> RandomKeyDecode(List<int> tour)
    {   
        //Mô tả: Hàm này dùng để chuyển danh sách các thành phố đang ở trong không gian rời rạc về không gian liên tục]
         //Input :
        //      tour: chứ danh sách các thành phố trong không gian rời rạc
        //Output:
        //      List<double> danh sách các thành phố trong không gian liên tục
        List<double> decoded = Enumerable.Repeat(0.0, tour.Count).ToList();
        // Tạo mảng chứa đường đi trong không gian liên tục
        List<double> city = Enumerable.Range(0, tour.Count)
            .Select(i => new Random().NextDouble())
            .OrderBy(d => d)
            .ToList();
        // Gán cho các thành phố trọng số tương ứng theo thứ tự tăng dần

        for (int i = 0; i < tour.Count; i++)
        {
            int idx = tour[i];
            decoded[idx] = city[i];
        }
        //Thay thế thành phố thành trong số tương ứng
        return decoded;
    }
     private static double fitness(List<double> tour ,double weight ){
    //Mô tả:
    //Hàm này dùng để tính fitness của một cá thể haws trong bầy trong không gian liên tục
    //Input:
    //      tour : danh sách của các thành phố trong không gian liên tục
    //      weight : trọng số weight của cá thể
    //Output:
    //      double :giá trị double đại diện cho kết quả fitness của cá thể đầu vào
        List<int> tourInt = RandomKeyEncode(tour); 
    //Chuyển hawk từ không gian liên tục sang không gian rời rạc
        return discreteFitness(tourInt,weight);
    //  Tính khoảng cách của đường đi đầu vào bằng hàm discreteFitness()


    }
    private static double discreteFitness(List<int> tour,double weight){
        //Mô tả:
        //Hàm này dùng để tính fitness của một cá thể haws trong không gian rời rạc
        //Input:
        //      tour : danh sách của các thành phố trong không gian rời rạc
        //      weight : trọng số weight của cá thể
        //Output:
        //     double: giá trị double đại diện cho kết quả fitness của cá thể đầu vào    
        double fitness = 0.0;
        // Khởi tạo fitness ban đầu
        for (int i = 0; i < tour.Count -1; i++)
        {
            //  Lấy thành phố hiện tại và thành phố tiếp theo
            City<T> city = graph[i];
            City<T> city1 = graph[(i + 1) % tour.Count];

            // Ở lần đi thứ nhất thì bỏ qua trọng số weight và cộng giá trị khoảng cách giữa 2 thành phố vào fitness
            // Ở các lần đi tiếp theo trọng số weigh sẽ được nhân vào giá trị khoảng cách giữa 2 thành phố theo hệ số weight tăng dần với weight**i
            if(i!=0){
                fitness += city.DistanceTo(city1)*Math.Pow(weight, i);
            }else{
                fitness += city.DistanceTo(city1);

            }
            
        }
        //Kết quả của fitness trả về tìm được
        return fitness;
    }
 public static List<(List<double>, double)> CreatePopulation(int populationSize)
    {
        //Mô tả:
        //Hàm này dùng để tạo ra N quần thể haws dựa vạo populationSize đầu vào
        //Input:
        //      populationSize: size của quần thể đầu vào
        //Output:
        //    List<(List<double>, double)>:  danh sách các cá thể haws trong quần thể với weight tương ứng của mỗi cá thể  
        
        List<(List<double>, double)> population = new List<(List<double>, double)>();
        //Tạo danh sách quần thể ban đầu
        Random random = new Random();

        //Chạy vòng for từ 0 đến populationSize để tạo từng quần thể
        for (int i = 0; i < populationSize; i++)
        {
            List<double> tour = Enumerable.Range(0, graph.Count)
            //tạo một danh sách các số nguyên từ 0 đến kích thước của đồ thị (đại diện cho các thành phố).
                .Select(_ => random.NextDouble())
            //tạo một danh sách các số thực ngẫu nhiên cho mỗi thành phố.
                .ToList();
            //chuyển đổi kết quả thành một danh sách các số thực.
            tour.Sort((a, b) => random.Next(-1, 2));

            double weight = 1 + random.NextDouble(); 
            //tạo ra một số thực ngẫu nhiên từ 1 đến 2 
            population.Add((tour, weight));
            //Thêm một tuple chứa tour (danh sách các số thực) và trọng lượng vào quần thể.
        }
        //Trả về danh sách các quần thể đã được tạo thành
        return population;
    }
public static (List<double>, double) FindBestRabbit(List<(List<double> tour, double weight)> population)
{
    //Mô tả: Hàm này dùng để tìm rabbit tốt nhất của quần thể 
    //Input: 
    //      population : danh sách các cá thể trong quần thể
    //Output:
    //     (List<double>, double):  cả thể tốt nhất trong quần thể được tìm thấy
    List<double> bestRabbit = new List<double>();
    //Khai báo một danh sách rỗng bestRabbit để lưu trữ tour tốt nhất
    double bestFitness = double.MaxValue;
    //Khởi tạo một biến bestFitness với giá trị lớn nhất có thể (double.MaxValue) để đại diện cho độ phù hợp tệ nhất ban đầu.
    double weightBestRabbit = double.MaxValue;
    //Khởi tạo một biến weightBestRabbit với giá trị lớn nhất có thể để lưu trữ trọng lượng của tour tốt nhất.

    foreach ((List<double> tour, double weight) in population)
    {
      //Duyệt qua từng cá thể (tour và trọng lượng) trong quần thể population.

        double tempFitness = fitness(tour, weight);
        //Tính toán độ phù hợp tạm thời tempFitness của tour hiện tại bằng hàm fitness.

        if (tempFitness < bestFitness)
            //Kiểm tra xem tempFitness có nhỏ hơn bestFitness (tốt hơn) không.
        {
            bestFitness = tempFitness;
            bestRabbit = new List<double>(tour);
            weightBestRabbit = weight;

            //Nếu tốt hơn thì lưu giá trị mới vào các biến
        }
    }
    //trả về rabbit tốt nhất trong quần thể với trọng số tương ứng
    return (bestRabbit, weightBestRabbit);
}
       public static double MetropolisAcceptanceProbability(double currentSolutionFitness, double newSolutionFitness, double temperature)
    {
          
    // Input:
    //     current_solution_fitness: float
    //         Khoảng cách đường đi của kết quả hiện tại
    //     new_solution_fitness: float
    //         Khoảng cách đường đi của kết quả mới
    //     temperature: float
    //         Biến temperature để điều chỉnh xác suất chấp nhận kết quả mới
    // Chức năng:
    //     Tính toán xác suất dựa trên khoảng cách đường đi hiện tại và khoảng cách đường đi mới để quyết định có cập nhật kết quả hiện tại bằng kết quả mới hay không
    // Output:
    //     double: Xác suất chấp nhận kết quả mới của metropolis acceptance

        if (newSolutionFitness < currentSolutionFitness)
    // Chắc chắn cập nhật kết quả nếu như khoảng cách mới nhỏ hơn khoảng cách hiện tại
        {
            return 1;
        }
        else
    //   Trả về một tỉ lệ để cập nhật kết quả nếu như khoảng mới lớn hơn khoảng cách hiện tại
    // Chấp nhận kết quả tệ hơn để có khả năng thoát khỏi local optimal
    
        {
            return Math.Exp(-Math.Abs(newSolutionFitness - currentSolutionFitness) / temperature);
        }
    }

    public static List<double> explorationPhase(List<double> randomTour,List<double> bestRabbit, List<double> tour,List<(List<double>, double)> population , double F,double q){
        //Mô tả: Đây là hàm dùng để thực hiện giai đoạn exploration 
        //Input : 
        //     randomTour: Chứa cá thể random của quần thể 
        //     besRabbit: Chứa cá thể tốt nhất của quần thể 
        //      tour    : chứa cá thể hiện tại của quần thể 
        //      population : Chứa danh sách các cá thể trong quần thể 
        //      q: giá trị ngẫu nhiên để thực hiện hành động
        //      F : tham số  Scaling Factor
        //Output:
        //      List<double> : Chứa cá thể mới được tạo thành
        Random random = new Random();
            
        int r1 = random.Next(population.Count);
        int r2 = random.Next(population.Count);
        int r3 = random.Next(population.Count);
        int r4 = random.Next(population.Count);
        //  Khởi tạo các tham số của DE/best/2 mutation
        
        List<double> newTour = new List<double>();
        //Tạo biến chứa quần thể mới
        if (q >= 0.5)
        //  Cập nhật lại vị trí dựa vào vị trí của một đường đi ngẫu nhiên khác nếu q >= 0.5

        {
         
            for (int j = 0; j < tour.Count; j++)
            {
                newTour.Add(randomTour[j] - r1 * Math.Abs(randomTour[j] - 2 * r2 * bestRabbit[j]));
            }
        }
        else
        //  Cập nhật lại vị trí theo DE/best/2 mutation nếu q < 0.5

        {
  
            (List<double> tourR3, double weight1) = population[r3];
            (List<double> tourR4, double weight2) = population[r4];
            //Lấy giá trị của các tourR3 và tourR4 với r3,r4 tương ứng
            for (int j = 0; j < tour.Count; j++)
            {
                newTour.Add(bestRabbit[j] + F * (tourR3[j] - tourR4[j]));
            }
        }      
        //Trả về kết quả là tour mới vừa được cập nhật
        return newTour;
    }
    public static (List<int>,double) HHO_MCF(int T , int N){
            
        // Input:
        //     N: integer
        //         Số lượng đường đi sẽ được cập nhật dần dần để tìm đường đi tối ưu
        //     T: integer
        //         Số vòng lặp để thực hiện thuật toán
        // Chức năng:
        //     Thực hiện T vòng lặp
        //     Trong mỗi vòng lặp, lần lượt cập nhật từng đường đi trong N đường đi theo thuật toán HHO_MCF, kết hợp với các cơ chế Random Key Encoding, Differential Evolution, Neighborhood Search Operator, Metropolist Acceptance
        // Output:
        //     X_rabbit: list[int]
        //         Một mảng một chiều biểu diễn đường đi tối ưu nhất mà thuật toán tìm được theo dạng [c0, c1, c2, ..., cn], với ci là thành phố thứ i
        //     fitness: double
        //         Khoảng cách đường đi của kết quả đã tìm được

        List<(List<double>, double)> population = new  List<(List<double>, double)>();
        //Tạo biến chứa danh sách các cá thể trong quần thể
        Random random = new Random();
        population = CreatePopulation(N);
        //Gọi hàm để tạo các cá thể trong quần thể

        // # Các tham số để tính điểm của các neighborhood search operators
        double mu = 0.99;
        //Hệ số của intensification, tăng khả năng sử dụng lại operator

        double delta = 1 - mu;
        //Hệ số của diversification, tăng khả năng sử dụng các operator khác
        ILLHFunctionInterface previousFunction = null;
        //Khởi tạo giá trị None để thay thế cho llh trước của vòng lặp đầu tiên
        //  Các tham số để tính metropolis acceptance
        double temp = 30;
        // beta dùng để điều chỉnh temp, qua mỗi vòng lặp, temp sẽ giảm dần đi để giảm tỉ lệ chấp nhận kết quả tệ hơn
        double beta = 0.99;
        // temp để chiều chỉnh tỉ lệ chấp nhận kết quả mới của metropolis accpetance

         //Tham số của thuật toán HHO
         //  Scaling Factor
        double F = 0.5;
        
        (List<double> bestRabbit, double bestRabbitWeight)  = (new List<double>(), double.MinValue);
        //Khởi tạo giá trị ban đầu cho bestRabbit và bestRabbitWeight
        for(int t = 0 ; t < T ;t++){
            // Thực hiện T vòng lặp  của thuật toán HHO

            (bestRabbit ,bestRabbitWeight) = FindBestRabbit(population);
            // Gọi hàm để tìm giá trị tốt nhất trong quần thể

           for (int i = 0; i < population.Count; i++)
                //Thực hiện lặp qua từng cá thể trong quần thẻ
            {

                (List<double> tour, double weight) = population[i];
                //Lấy giá trị của cá thể và trọng số tương ứng của cá thể hiện tại

                double E0 = 2 * random.NextDouble() - 1;
                //Khởi tạo năng lượng ban đầu E)


       
                double E = 2 * E0 * (1 - t / T);
                //Cập nhật năng lượng E của rabbit
                double absE = Math.Abs(E);
                
                double J = 2 * (1 - random.NextDouble());
                //Khởi tạo sức nhảy J

                //Tạo q ngầy nhiên để xác định hành động trong các gia đoạn sau
                double q = random.NextDouble();
                if (absE >= 1)
                //Tiến tới gian đoạn khám phá nếu absE >=1
                {
                    
                    (List<double> randomTour, double randomWeight) = population[random.Next(population.Count)];
                    //Lấy một cá thể ngẫu nhiên trong quần thể 

                    List<double> newTour = new List<double>();
                    //Khởi tạo giá trị newTour ban đầu

                    newTour = explorationPhase(randomTour,bestRabbit,tour,population,F,q);
                    //Gọi hàm để thực hiện giai đoạn khai thác và gán nó cho newTour vừa được khởi tạo
                    
                    double currentSolutionFitness = fitness(tour, weight);
                    //tính toán fitness hiện tại của quần thể
                    double newSolutionFitness = fitness(newTour, weight);
                    //tính toán fitness của newTour mới vừa được tạo thành
                    double acceptanceProbability = MetropolisAcceptanceProbability(currentSolutionFitness, newSolutionFitness, t);
                    //Tính toán xác suất chấp nhận kết quả mới của metropolis acceptance

                    if (random.NextDouble() < acceptanceProbability)
                    //Dùng tỉ lệ để xem có chấp nhận được đi mới hay không
                    {
                        population[i] = (newTour, weight);
                    }
                }else{

                    //Tiến tới giai đoạn khai thác nếu |E| < 1
                   double rand = random.NextDouble();
                    //    Tạo biến ngẫu nhiên rand để xác định cập nhật vị trí của đường đi đang xét theo thuật toán HHO gốc hay sử dụng các neighborhood search operators
                    // Cập nhật lại vị trí theo HHO nếu rand >= q

                    if(rand>=0.5){

                            double r = random.NextDouble();
                            //  Tạo ngẫu nhiên r để xác định có thực hiện HHO hay không
                 
                            if(r>=0.5){

                                
                                List<double> deltaPopulation = new List<double>();
                              
                                for (int j = 0; j < tour.Count; j++)
                                {
                                    deltaPopulation.Add(bestRabbit[j] - tour[j]);
                                    //Tính khoảng cách từ X_rabbit đến đường đi đang xét
                                }
                                
                                List<double> newTour = new List<double>();
                                //Khởi tạo giá trị newTour ban đầu

                                if(absE>=0.5){
                                    //  Soft besiege
                                    for (int j = 0; j < tour.Count; j++)
                                    {
                                        newTour.Add(deltaPopulation[j] - E * Math.Abs(J * bestRabbit[j] - tour[j]));
                                    }
                                }else{
                                    // Hard besiege

                                    for (int j = 0; j < tour.Count; j++)
                                    {
                                        newTour.Add(bestRabbit[j] + E*Math.Abs(deltaPopulation[j]));
                                    }
                                }
                                double currentSolutionFitness = fitness(tour, weight);
                                //tính toán fitness hiện tại của quần thể
                                double newSolutionFitness = fitness(newTour, weight);
                                //tính toán fitness của newTour mới vừa được tạo thành
                                double acceptanceProbability = MetropolisAcceptanceProbability(currentSolutionFitness, newSolutionFitness, temp);
                                //Tính toán xác suất chấp nhận kết quả mới của metropolis acceptance

                                if (random.NextDouble() < acceptanceProbability)
                                //Dùng tỉ lệ để xem có chấp nhận được đi mới hay không
                                {
                                    population[i] = (newTour, weight);
                                }
                            }
                        }
                    else
                    // Sử dụng các neighborhood search operators nếu rand < q
                    {

                        List<int> tourInt = RandomKeyEncode(tour);
                        //Chuyển hawks sang không gian rởi rạc
                        
                        ILLHFunctionInterface function = ChoiceFunction();
                        //Chọn một llh
                        function.IncrementCounter();
                        //Cập số lần được gọi đến của llh

                        Stopwatch stopwatch = Stopwatch.StartNew();
                        List<int> newTourInt = function.Apply(tourInt);
                        stopwatch.Stop();
                        long elapsedTime = stopwatch.ElapsedMilliseconds;
                        function.SetTimestamp( elapsedTime);
                        //Tính toán và thực hiện lưu lại thời gian thwucj hiện


                        double currentSolutionFitness = fitness(tour, weight);
                        //Tính toán đường đi hiện tại 
                        double newSolutionFitness = fitness(RandomKeyDecode(newTourInt), weight);
                        //Tính toán đường đi mới vừa tính được

                        double f1_hj = CalculateF1(function, mu, currentSolutionFitness, newSolutionFitness, elapsedTime);
                        function.SetF1(f1_hj);
                        //Thực hiện tính toán f1 và cập nhật f1 của llh được chọn

                        if (previousFunction == null)
                        {
                            //Thực hiện lấy 1 llh nếu đây là lần lặp đầu tiên 
                            int index = neighborhoodfunctions.IndexOf(previousFunction);
                            previousFunction = neighborhoodfunctions[(index + 1) % (neighborhoodfunctions.Count)];
                        }

                        previousFunction.AddConsecCounts(function);
                        double f2_hj_hk = CalculateF2(function, previousFunction, mu, currentSolutionFitness, newSolutionFitness);
                        previousFunction.AddF2(previousFunction, f2_hj_hk);
                        //Thực hiện tính toán và cập nhật lại f2 của llh được chọn

                        foreach (ILLHFunctionInterface neighborhoodFunction in neighborhoodfunctions)
                        //Cập nhật điểm của các llh
                        {
                            // Điều chỉnh để số điểm của các llh không âm
                        
                            double score = mu*(-neighborhoodFunction.GetF1() -  neighborhoodFunction.GetF2(function) )+ delta*(elapsedTime);
                            double Q = Math.Max(0, score + 1) / (10 * (neighborhoodfunctions.Count));
                            neighborhoodFunction.SetScore(Math.Max(score,Q*(Math.Pow(1.5,-score))));
                         }


                        //Tính toán tỉ lệ p của metropolist acceptance
                        double p = MetropolisAcceptanceProbability(currentSolutionFitness, newSolutionFitness, temp);
                        // Dùng tỉ lệ p xác định có cập nhật đường đi hay không
                        if (random.NextDouble() < p)
                        {
                            population[i] = (RandomKeyDecode(newTourInt), weight);
                        }


                        double d = currentSolutionFitness - discreteFitness(newTourInt, weight);
                        // Dựa vào mức độ cải thiện của kết quả để cập nhật mu và delta
                        if (d > 0)
                        {
                            mu = 0.99;
                        }else{
                            mu = Math.Max(0.01, mu - 0.01);
                        }
                        delta = 1 - mu;
                        // Lưu llh trước đó thành llh vừa được chọn 
                        previousFunction = function;
                        // Chuyển đường về lại không gian liên tục
                        tour = RandomKeyDecode(newTourInt);

                    }
                }

            }
            // Cập nhập tham số temp của metropolist acceptance
            temp = beta*temp;
        }
        (List<double> best ,double weightBestRabbit) = FindBestRabbit(population);
        //Trả về kết quả đường đi tốt nhất
        return (RandomKeyEncode(best) , fitness(best,weightBestRabbit));
    }

     static ILLHFunctionInterface ChoiceFunction()
{   
    Random random = new Random();
    double totalScore = 0;
    // Khởi tạo giá trị total Score ban đầu
    foreach (ILLHFunctionInterface neighborhoodFunction in neighborhoodfunctions)
    {
        totalScore += neighborhoodFunction.GetScore();
    }
    //Tính toán giá trị total Score của các llh

    double[] weight = new double[neighborhoodfunctions.Count];


    if (totalScore != 0)
    {
        for (int i = 0; i < neighborhoodfunctions.Count; i++)
        {
            weight[i] = neighborhoodfunctions[i].GetScore() / totalScore;
        }
    }
    else
    {
        for (int i = 0; i < neighborhoodfunctions.Count; i++)
        {
            weight[i] = 1.0 / neighborhoodfunctions.Count;
        }
    }
    //Chuyển đổi từ điểm sang trọng số

    double totalWeight = weight.Sum();
    //Tính tổng trọng số

    double ran = random.NextDouble() * totalWeight;
    //  Chọn 1 giá trị ngẫu nhiên trong tổng trọng số

    double currentWeight = 0;

    for (int i = 0; i < weight.Length; i++)
    //  Duyệt qua từng llh và kiểm trá trị ngẫu nhiên ở đâu và trả về, llh ở đó

    {
        currentWeight += weight[i];

        if (currentWeight > ran)
        {
            return neighborhoodfunctions[i];
        }
    }

    return neighborhoodfunctions[0];
}
     static double CalculateF1(ILLHFunctionInterface function, double mu, double currentSolutionFitness, double newSolutionFitness, long timestamp)
    {
    // Input:
    //     function: ILLHFunctionInterface
    //         llh cần thực hiện tính toán f1
    //     mu: float
    //         Hệ số mu đóng vai trò như trọng số của f1
    //     currentSolutionFitness: float
    //         Khoảng cách đường đi của kết quả hiện tại
    //     newSolutionFitness: float
    //         Khoảng cách đường đi của kết quả mới
    //     timestamp: float
    //         Thời gian để hj tính toán ra kết quả mới
    // Output:
    //     float: Giá trị f1 của hj đã được cập nhật sau lần gọi thứ n
        double f1_hj = function.GetF1() +
                    Math.Pow(mu, (function.GetCounter() - 1)) *
                    (currentSolutionFitness - newSolutionFitness) / timestamp;
        return f1_hj;
    }

     static double CalculateF2(ILLHFunctionInterface function1, ILLHFunctionInterface function2, double mu, double currentSolutionFitness, double newSolutionFitness)
    {

        // Input:
        //     function1 : ILLHFunctionInterface
        //     function2 : ILLHFunctionInterface
        //     mu: float
        //         Hệ số mu đóng vai trò như trọng số của f2
        //     currentSolutionFitness: float
        //         Khoảng cách đường đi của kết quả hiện tại
        //     newSolutionFitness: float
        //         Khoảng cách đường đi của kết quả mới
        // Output:
        //     float: Giá trị f2 của hj theo sau bởi hk đã được cập nhật sau lần gọi thứ n
        double f2_hj_hk = Math.Pow(mu, function1.GetConsecCounts(function2)) *
                        (currentSolutionFitness - newSolutionFitness) / (function1.GetTimestamp() + function2.GetTimestamp());

        return f2_hj_hk;
    }

}

