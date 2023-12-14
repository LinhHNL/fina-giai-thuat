using System;
using System.Collections.Generic;

public interface  ILLHFunctionInterface
{
    List<int> Apply(List<int> orgTour);
    int GetCounter();
    void IncrementCounter();
    long GetTimestamp();
    void SetTimestamp(long timestamp);
    double GetScore();
    void SetScore(double score);
    int GetConsecCounts(ILLHFunctionInterface function);
    void SetConsecCounts(ILLHFunctionInterface function, int count);
    void AddConsecCounts(ILLHFunctionInterface function);
    double GetF1();
    void SetF1(double f1);
    void AddF2(ILLHFunctionInterface function, double f2);
    double GetF2(ILLHFunctionInterface function);
}
