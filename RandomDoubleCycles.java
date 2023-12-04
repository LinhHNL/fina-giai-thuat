import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Random;

public class RandomDoubleCycles implements LLHFunctionInterface{

   private int counter;
   private double score;
   private long timestamp;
   private double f1;
   private double f2;
   
   Map<LLHFunctionInterface, Integer> consec_counts = new HashMap<>();
    public RandomDoubleCycles() {
            counter = 0;
            score = 0;
            timestamp = 0;
            f1 = 0;
            f2 = 0;
     }
   @Override
    public List<Integer> apply(List<Integer> orgTour) {
        return randomDoubleCycles(orgTour);
    }
    
    private  List<Integer> randomDoubleCycles(List<Integer> orgTour) {
        List<Integer> tour = new ArrayList<>(orgTour);
        List<List<Integer>> reversedSublist = new ArrayList<>();
        for (int i = 0; i < tour.size(); i=i+2) {
           reversedSublist.add(tour.subList(i, i+2));

        }
        List<Integer> saveIndexSwap = new ArrayList<>();
        for (int i = 0; i < reversedSublist.size()/2; i++) {
            Random random = new Random();
            int sub1 = random.nextInt(reversedSublist.size()) ;
            int sub2 = random.nextInt(reversedSublist.size());
            while(saveIndexSwap.contains(sub1)) {
                sub1 = random.nextInt(reversedSublist.size());
            }
            while(true) {
                sub2 = random.nextInt(reversedSublist.size());
                if(sub1 != sub2 && !saveIndexSwap.contains(sub2)) {
                    break;
                }
            }
            saveIndexSwap.add(sub1);
            saveIndexSwap.add(sub2);
            Collections.swap(reversedSublist, sub1, sub2);
        }
        
        List<Integer> result = new ArrayList<>();
        for (List<Integer> list : reversedSublist) {
            result.addAll(list);
        }

        return result;
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
    public double getF2() {
        return f2;  
    }
    @Override
    public void setF2(double f2) {
        this.f2 = f2;
    }
}
