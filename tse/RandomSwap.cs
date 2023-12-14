using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
public static class ListExtensions
{
    public static void Swap<T>(this List<T> list, int indexA, int indexB)
    {
        T tmp = list[indexA];
        list[indexA] = list[indexB];
        list[indexB] = tmp;
    }
}
public class RandomSwap : ILLHFunctionInterface
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

    public RandomSwap()
    {        //Khởi tạo các giá trị ban đầu của functions

        counter = 0;
        score = 0;
        timestamp = 0;
        f1 = 0;
    }

    public List<int> Apply(List<int> orgTour)
    {
        return RandomSwapFunction(new List<int>(orgTour));
    }

    private List<int> RandomSwapFunction(List<int> orgTour)
    {
        // Random Swap (RS): Hoán vị ngẫu nhiên
        // Chọn ngẫu nhiên hai thành phố trên tour và hoán đổi vị trí của chúng.
        List<int> tour = new List<int>(orgTour);
        Random random = new Random();
        int i = random.Next(tour.Count);
        int j = random.Next(tour.Count);
        ListExtensions.Swap(tour, i, j);
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
