import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Random;

public class RandomSwapDoubleCycles implements LLHFunctionInterface{

   private int counter;
   private double score;
   private long timestamp;
   private double f1;
   private Map<LLHFunctionInterface,Double> f2 = new HashMap<>();
   Map<LLHFunctionInterface, Integer> consec_counts = new HashMap<>();
    public RandomSwapDoubleCycles() {
            counter = 0;
            score = 0;
            timestamp = 0;
            f1 = 0;
            
     }
   @Override
    public List<Integer> apply(List<Integer> orgTour) {
        return randomSwapDoubleCycles(orgTour);
    }
    

    private  List<Integer> randomSwapDoubleCycles(List<Integer> orgTour) {
        List<Integer> tour = new ArrayList<>(orgTour);
        List<List<Integer>> reversedSublist = new ArrayList<>();
        for (int i = 0; i < tour.size(); i=i+2) {
           reversedSublist.add(tour.subList(i, i+2));

        }
        List<List<List<Integer>>> squenceList = new ArrayList<>();
        squenceList.add(reversedSublist.subList(0, reversedSublist.size()/2));        
        squenceList.add(reversedSublist.subList(reversedSublist.size()/2, reversedSublist.size()));
        for (List<List<Integer>> list : squenceList) {
            List<Integer> saveIndexSwap = new ArrayList<>();
            for (int i = 0; i < list.size()/2; i++) {
                Random random = new Random();
                int sub1 = random.nextInt(list.size());
                int sub2 = random.nextInt(list.size());
                while(saveIndexSwap.contains(sub1)) {
                    sub1 = random.nextInt(list.size());
                }
                while(true) {
                    sub2 = random.nextInt(list.size());
                    if(sub1 != sub2 && !saveIndexSwap.contains(sub2)) {
                        break;
                    }
                }
                saveIndexSwap.add(sub1);
                saveIndexSwap.add(sub2);
                Collections.swap(list, sub1, sub2);
            }
        }
        List<Integer> result = new ArrayList<>();
        for (List<List<Integer>> list : squenceList) {
            for (List<Integer> list2 : list) {
                result.addAll(list2);
            }
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
