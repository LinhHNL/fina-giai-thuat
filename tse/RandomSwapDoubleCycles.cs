using System;
using System.Collections.Generic;
using System.Linq;

public class RandomSwapDoubleCycles : ILLHFunctionInterface
{
    private int counter;
    //Số lần được gọi đển của llh này
    private double score;
    //Điểm được gọi đển của llh này
    private long timestamp;
    //Thời gian chạy gần nhất của llh này
    private double f1;
    //Giá trị f1 gần nhất
    private Dictionary<ILLHFunctionInterface, double> f2 = new Dictionary<ILLHFunctionInterface, double>();
    //Danh sách giá trị f2 tương ứng với từng cặp llhs
    private Dictionary<ILLHFunctionInterface, int> consecCounts = new Dictionary<ILLHFunctionInterface, int>();
    //Danh sách số lần được gọi cùng nhau của từng cặp llhs

    public RandomSwapDoubleCycles()
    {        //Khởi tạo các giá trị ban đầu của functions

        counter = 0;
        score = 0;
        timestamp = 0;
        f1 = 0;
    }

    public List<int> Apply(List<int> orgTour)
    {
        return RandomSwapDoubleCyclesFunction(orgTour);
    }

    private List<int> RandomSwapDoubleCyclesFunction(List<int> tour)
    {
        // Random Swap Double Cycle (RSDC): Hoán đổi ngẫu nhiên theo cặp và hoán đổi đoạn tour

        // Chọn ngẫu nhiên các cặp đoạn tour có cùng số thành phố (ít nhất 4).
        // Hoán đổi vị trí của các thành phố trong từng cặp đoạn.
        // Hoán đổi vị trí của hai đoạn tour đã hoán đổi.
            if (tour.Count < 8)
            {
            return tour;
            }

            var random = new Random();

            // Select two pairs of indices (i1, i2) and (j1, j2) randomly within the tour bounds.
            int i1 = random.Next(tour.Count / 2);
            int i2 = i1 + random.Next(1, tour.Count / 2);
            int j1 = random.Next(tour.Count / 2);
            while (j1 == i1 || j1 == i2)
            {
                j1 = random.Next(tour.Count / 2);
            }
            int j2 = j1 + random.Next(1, tour.Count / 2);
            while (j2 == i1 || j2 == i2 || j2 == j1)
            {
                j2 = random.Next(tour.Count / 2);
            }

            // Ensure the selected pairs have the same length.
            if (i2 - i1 != j2 - j1)
            {
            return tour;
            }

            // Swap city positions within each pair.
            var temp1 = tour[i1];
            tour[i1] = tour[i2];
            tour[i2] = temp1;
            temp1 = tour[j1];
            tour[j1] = tour[j2];
            tour[j2] = temp1;

            var subSequence1 = tour.GetRange(i1, i2 - i1 + 1);
            tour.RemoveRange(i1, subSequence1.Count);
            tour.InsertRange(j1, subSequence1);

            return tour;
    }

     public double GetF2(ILLHFunctionInterface function)
    //Input :
    //      function : function cần lấy giá trị F2
    //Output : 
    //      Giá trị f2 tương ứng với function được truyền vào
    {
        if (f2.ContainsKey(function))
        //Nếu function đã tồn tại thì lấy ra giá trị f2 tương ứng nếu không thì trả về 0
        {
            return f2[function];
        }
        else
        {
            return 0;
        }
    }

    public void AddF2(ILLHFunctionInterface function, double f2Value)
    //Set giá trị f2 của function tương ứng 
    //Input :
    //      function : function cần set giá trị 
    //      f2value  : Giá trị f2 tương ứng 
    {
        if (f2.ContainsKey(function))
        {
            f2[function] = f2Value;
        }
        else
        {
            f2[function] = f2Value;
        }
    }

    public int GetConsecCounts(ILLHFunctionInterface function)
    //Lấy số lần được gọi cùng nhau của 2 llhs 
    //Nếu đã tồn tại thì trả về số lần được gọi cùng nhau của 2 này và llh được truyền vào
    //Nếu không thì trả về 0
    {
        if (consecCounts.ContainsKey(function))
        {
            return consecCounts[function];
        }
        else
        {
            return 0;
        }
    }

    public void SetConsecCounts(ILLHFunctionInterface function, int count)
    {
        consecCounts[function] = count;
    }
    
    public void AddConsecCounts(ILLHFunctionInterface function)
    //Tăng số lần được gọi cùng nhau của 2 llh lên 1
    {

        if (consecCounts.ContainsKey(function))
        {
            consecCounts[function]++;
        }
        else
        {
            consecCounts[function] = 1;
        }
    }

    public int GetCounter()
    //Lấy số lần được gọi đến của llh này
    
    {
        return counter;
    }

    public void IncrementCounter()
    //Tăng số lần được gọi của lln này
    {
        counter++;
    }

    public void SetScore(double scoreValue)
    //Set điểm của llh này
    {
        score = scoreValue;
    }

    public double GetScore()
    //Lấy điểm của llh này
    {
        return score;
    }

    public void SetCounter(int counterValue)
    //Set điểm của llh này
    {
        counter = counterValue;
    }

    public long GetTimestamp()
    //Lấy lần thời gian chạy của lần được gọi gần nhất của llhs này
    {
        return timestamp;
    }

    public void SetTimestamp(long timestampValue)
    //Set thời gian chạy của lần được gọi gần nhất của llhs này
    {
        timestamp = timestampValue;
    }

    public double GetF1()
    //Lấy giá trị f1 của llh này
    {
        return f1;
    }

    public void SetF1(double f1Value)
    // Set giá trị f1 của llh này
    {
        f1 = f1Value;
    }

}
