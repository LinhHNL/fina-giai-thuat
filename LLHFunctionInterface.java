import java.security.Timestamp;
import java.util.List;

public interface LLHFunctionInterface {
    List<Integer> apply(List<Integer> orgTour);
    int getCounter();
    void incrementCounter();
    long getTimestamp();
    void setTimestamp(long timestamp);
    double getScore();
    void setScore(double score);
    Integer getConsec_counts(LLHFunctionInterface function);
    void setConsec_counts(LLHFunctionInterface function, Integer count);
    void addConsec_counts(LLHFunctionInterface function);
    double getF1();
    void setF1(double f1);
    void addF2(LLHFunctionInterface function,double f2);
    double getF2(LLHFunctionInterface function);
} 