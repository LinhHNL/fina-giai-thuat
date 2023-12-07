import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Random;

public class RandomReversingSwapSubsequence implements LLHFunctionInterface{

   private int counter;
   private double score;
   private long timestamp;
   private double f1;
   private Map<LLHFunctionInterface,Double> f2 = new HashMap<>();
   Map<LLHFunctionInterface, Integer> consec_counts = new HashMap<>();
    public RandomReversingSwapSubsequence() {
            counter = 0;
            score = 0;
            timestamp = 0;
            f1 = 0;
            
     }
   @Override
    public List<Integer> apply(List<Integer> orgTour) {
        return randomReversingSwapSubsequence(orgTour);
    }
    public static List<Integer> randomReversingSwapSubsequence(List<Integer> orgTour) {
        // List<Integer> tour = new ArrayList<>(orgTour);
        // // Check that the tour has at least 4 cities
        // if (tour.size() < 4) {
        //     return Collections.emptyList();
        // }
        // // Randomly select two indices from the first half of the tour
        // int start1 = (int) (Math.random() * (tour.size() / 2));
        // int end1 = start1 + (int) (Math.random() * (tour.size() / 2));
        // // Calculate the number of elements to be swapped
        // int noc = end1 - start1;
        // // Randomly select an index from the second half of the tour
        // int start2 = (int) (Math.random() * (tour.size() / 2) + noc);

        // // Calculate the end index for the second subsequence
        // int end2 = start2 + noc;
        // while (end2 >= tour.size()) {
        //    start2 = (int) (Math.random() * (tour.size() / 2) + noc);

        // // Calculate the end index for the second subsequence
        //          end2 = start2 + noc;
        // }
        // // Swap the two subsequences
        // List<Integer> subsequence1 = tour.subList(start1, end1 + 1);
        // List<Integer> subsequence2 = tour.subList(start2, end2 + 1);
        // tour.set(start1, subsequence2.get(0));
        // tour.set(start2, subsequence1.get(0));
        // for (int i = 1; i < noc; i++) {
        //     tour.set(start1 + i, subsequence2.get(i));
        //     tour.set(start2 + i, subsequence1.get(i));
        // }

        // return tour;
        List<Integer> tour = new ArrayList<>(orgTour);
        // Check that the tour has at least 4 cities
        if (tour.size() < 4) {
            return Collections.emptyList();
        }
        Random rand = new Random();
        // Randomly select two indices from the first half of the tour
        int start1 = rand.nextInt(tour.size() / 2);
        int end1 = start1 + rand.nextInt(tour.size() / 2 - start1);
        // Calculate the number of elements to be swapped
        int noc = end1 - start1;
        // Randomly select an index from the second half of the tour
        int start2 = rand.nextInt(tour.size() - noc - start1) + start1;
        // Calculate the end index for the second subsequence
        int end2 = start2 + noc;
        // Swap the two subsequences
        List<Integer> subsequence1 = new ArrayList<>(tour.subList(start1, end1 + 1));
        List<Integer> subsequence2 = new ArrayList<>(tour.subList(start2, end2 + 1));
        Collections.reverse(subsequence1);
        Collections.reverse(subsequence2);
        tour.subList(start1, end1 + 1).clear();
        tour.addAll(start1, subsequence2);
        tour.subList(start2, end2 + 1).clear();
        tour.addAll(start2, subsequence1);
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
    @Override
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
}
