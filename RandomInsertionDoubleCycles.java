import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Random;

public class RandomInsertionDoubleCycles implements LLHFunctionInterface{

   private int counter;
   private double score;
   private long timestamp;
   private double f1;
   private Map<LLHFunctionInterface,Double> f2 = new HashMap<>();
   Map<LLHFunctionInterface, Integer> consec_counts = new HashMap<>();
    public RandomInsertionDoubleCycles() {
            counter = 0;
            score = 0;
            timestamp = 0;
            f1 = 0;
     }
   @Override
    public List<Integer> apply(List<Integer> orgTour) {
        return randomInsertionDoubleCycles(orgTour);
    }
    

    public static List<Integer> randomInsertionDoubleCycles(List<Integer> tour) {
        if (tour.size() < 4) {
            return Collections.emptyList();
        }

        int index1 = (int) (Math.random() * tour.size());
        int index2 = (int) (Math.random() * tour.size());

        while (index1 == index2) {
            index2 = (int) (Math.random() * tour.size());
        }
        int insertIndex = (int) (Math.random() * tour.size());
        int temp = tour.get(index1);
        tour.set(index1, tour.get(index2));
        tour.set(index2, temp);

        // Insert the two cities into the tour
        tour.add(insertIndex, tour.get(index1));
        tour.add(insertIndex, tour.get(index2));

        return tour;
    }
    public Integer getConsec_counts(LLHFunctionInterface function) {
        return consec_counts.get(function);
    }
    public void setConsec_counts(LLHFunctionInterface function, Integer count) {
        consec_counts.put(function, count);
    }
    public void addConsec_counts(LLHFunctionInterface function) {
        if (consec_counts.containsKey(function)) {
            consec_counts.put(function, consec_counts.get(function) + 1);
        } else {
            consec_counts.put(function, 1);
        }
    }
    public double getF2(LLHFunctionInterface function) {
        if(f2.containsKey(function)) {
            return f2.get(function);
        } else {
            return 0;
        }
    }
    @Override
    public void addF2(LLHFunctionInterface function, double f2) {
        if(this.f2.containsKey(function)) {
            this.f2.put(function, this.f2.get(function) + f2);
        } else {
            this.f2.put(function, f2);
        }
    }
    @Override
    public int getCounter() {
       return counter;
    }
    @Override
    public void incrementCounter() {
        counter++;
    }
    @Override
    public void setScore(double score) {
        this.score = score;
    }
    @Override
    public double getScore() {
        return score;
    }
    public void setCounter(int counter) {
        this.counter = counter;
    }
    @Override
    public long getTimestamp() {
        return timestamp;
    }
    @Override
    public void setTimestamp(long timestamp) {
        this.timestamp = timestamp;
        
    }
    @Override
    public double getF1() {
        return this.f1; 
    }
    @Override
    public void setF1(double f1) {
        this.f1 = f1;
    }
   
}
