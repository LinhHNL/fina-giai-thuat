using System;
using System.Collections.Generic;
using System.Linq;

public class RandomReversingSwapSubsequence : ILLHFunctionInterface
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
    public RandomReversingSwapSubsequence()
    {
               //Khởi tạo các giá trị ban đầu của functions
         counter = 0;
        score = 0;
        timestamp = 0;
        f1 = 0;
    }

    public List<int> Apply(List<int> orgTour)
    {
        return RandomReversingSwapSubsequenceFunction(new List<int>(orgTour));
    }

    public static List<int> RandomReversingSwapSubsequenceFunction(List<int> orgTour)
    {
        // Random Reversing Swap of Sub-Sequences (RRSS): Đảo ngược và hoán đổi ngẫu nhiên hai đoạn tour

        // Chọn ngẫu nhiên hai đoạn tour có cùng số thành phố.
        // Đảo ngược thứ tự các thành phố trong cả hai đoạn.
        // Hoán đổi vị trí của hai đoạn tour đã đảo ngược.

        List<int> tour = new List<int>(orgTour);

        if (tour.Count < 4)
        {
            return orgTour;
        }
        int mid = tour.Count / 2;
        if(mid % 2 != 0)
        {
            mid++;
        }
        int start1 = 0;
        int end1 = start1 +  tour.Count / 2;
        int start2 = mid;
        int end2 = tour.Count - 1;

        List<int> subsequence1 = tour.GetRange(start1, tour.Count / 2);
        List<int> subsequence2 = tour.GetRange(start2, (tour.Count / 2)-1);

        tour[start1] = subsequence2[0];
        tour[start2] = subsequence1[0];

        for (int i = 1; i < (tour.Count / 2) -1; i++)
        {
            tour[start1 + i] = subsequence2[i];
            tour[start2 + i] = subsequence1[i];
        }

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
