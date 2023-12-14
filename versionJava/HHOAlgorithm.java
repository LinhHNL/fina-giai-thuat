import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.List;
import java.util.Random;
import java.util.stream.Collectors;
import java.util.stream.DoubleStream;
import java.util.stream.IntStream;

public class HHOAlgorithm {
    static List<LLHFunctionInterface> neighborhoodfunctions = Arrays.asList(
              new RandomDoubleCycles(),//success
              new RandomInsertion(),//success
              new RandomInsertionDoubleCycles(),
              new RandomInsertionSubsequence(),//success
              new RandomNestedCycles(), //success
              new RandomReversingSubsequence(),//success
            //   new RandomReversingSwapSubsequence(),//success
            //   new RandomSwap(),//success
              new RandomSwapDoubleCycles(),//success
              new RandomSwapSubsequence()
        );
    static List<City> graph = new ArrayList<>();
    private static double fitness(List<City> graph, List<Double> tour,double weight) {
        List<Integer> continuousTour = randomKeyEncode(tour);
        return discreteFitness(graph, continuousTour, weight);
    }
    private static double discreteFitness(List<City> graph, List<Integer> tour,double weight) {
        double fitness = 0;
        for (int i = 0; i < tour.size() - 1; i++) {
            City city1 = graph.get(tour.get(i));
            City city2 = graph.get(tour.get(i + 1));
            if(i != 0){
                fitness += city1.distanceTo(city2)*Math.pow(weight, i);
            }
        }
        fitness  += graph.get(tour.get(tour.size() - 1)).distanceTo(graph.get(tour.get(0)))*Math.pow(weight, tour.size() - 1);
        return fitness;
    }
    public static List<Integer> randomKeyEncode(List<Double> tour) {
        List<Integer> encoded = IntStream.range(0, tour.size())
                .boxed()
                .sorted((i, j) -> Double.compare(tour.get(i), tour.get(j)))
                .collect(Collectors.toList());
        return encoded;
    }
    public static List<Double> randomKeyDecode(List<Integer> tour) {
        List<Double> decoded = new ArrayList<>(Collections.nCopies(tour.size(), 0.0));
        List<Double> city = IntStream.range(0, tour.size())
                .mapToDouble(i -> Math.random())
                .boxed()
                .sorted()
                .collect(Collectors.toList());

        for (int i = 0; i < tour.size(); i++) {
            int idx = tour.get(i);
            decoded.set(idx, city.get(i));
        }
        return decoded;
    }
    public static List<List<Double>> createPopulation(int populationSize) {
        List<List<Double>> population = new ArrayList<>();
        Random random = new Random();

        for (int i = 0; i < populationSize; i++) {
            List<Double> tour = DoubleStream.generate(() -> random.nextDouble())
                    .limit(graph.size())
                    .boxed()
                    .collect(Collectors.toList());

            Collections.shuffle(tour);
            population.add(tour);
        }
        // System.out.println("Population" + population);
        return population;
    }
    public static List<Double> findBestRabbit(List<List<Double>> population,double weight) {
        List<Double> bestRabbit = new ArrayList<>();
        double bestFitness = Double.MAX_VALUE;
        for (List<Double> tour : population) {
            double fitness = fitness(graph, tour,weight);
            // System.out.println("fitness" + fitness);
            if (fitness < bestFitness) {
                bestFitness = fitness;
                bestRabbit = tour;
            }
        }
        return bestRabbit;
    }
    public static double metropolis_acceptance_probability(double current_solution_fitness, double new_solution_fitness, double temperature) {
        if (new_solution_fitness < current_solution_fitness) {
            return 1;
        } else {
            return Math.exp(-Math.abs(new_solution_fitness - current_solution_fitness) / temperature);
        }
    }
    public static LLHFunctionInterface choiceFunction() {
        double totalScore = 0;
        for (LLHFunctionInterface neighborhoodfunction : neighborhoodfunctions) {
            totalScore += neighborhoodfunction.getScore();
        }
        double weight[] = new double[neighborhoodfunctions.size()];;
        if(totalScore!=0){
            for (int i = 0; i < neighborhoodfunctions.size(); i++) {
                weight[i] = neighborhoodfunctions.get(i).getScore()/totalScore;
            }
        }else{
            Arrays.fill(weight, 1.0/neighborhoodfunctions.size());
        }
        double totalWeight = 0;
        for (int i = 0; i < weight.length; i++) {
            totalWeight += weight[i];
        }
        double random = Math.random() * totalWeight;
        double crurent_weight = 0;
        for (int i = 0; i < weight.length; i++) {
            crurent_weight += weight[i];
            if(crurent_weight > random){
                return neighborhoodfunctions.get(i);
            }
        }
        return neighborhoodfunctions.get(0);
    }
    public static double calculateF1(LLHFunctionInterface function,
    double mu, double current_solution_fitness, double new_solution_fitness ,
    long timestamp) {
        
        double f1_hj = 
                function.getF1()+ 
                Math.pow(mu,(function.getCounter()-1))
                *(current_solution_fitness - new_solution_fitness)/timestamp;
        return f1_hj;
        
    }
    public static double calculateF2(
        LLHFunctionInterface function1 , LLHFunctionInterface function2,
        double mu, double current_solution_fitness,double new_solution_fitness
        ) {
     
       double f2_hj_hk = Math.pow(mu,function1.getConsec_counts(function2))
               *(current_solution_fitness - new_solution_fitness)/(function1.getTimestamp() + function2.getTimestamp());
        
        return f2_hj_hk;
    }
    public static List<Double>   HHOAlgorithm(List<City> graph, int maxIteration, int populationSize,double weight) {
        List<List<Double>> population = new ArrayList<>();
        Random random = new Random();
        population = createPopulation(populationSize);
       double mu = 0.99;
       double delta = 1 - mu;
        // for (List<Double> list : population) {
        //     System.out.println(list);
        // }
       double temp = 30;
       double beta = 0.99;
        LLHFunctionInterface pre_selectedFunction = null;
       double F = 0.5;
        List<Double> X_rabbit = new ArrayList<>();
       for (int temperature = 0; temperature < maxIteration; temperature++) {
            X_rabbit = findBestRabbit(population,weight);

            // System.out.println("Best Rabbit" + X_rabbit);
            for (List<Double> tour : population) {
                double E0 = 2 * Math.random() - 1;
                double J = 2 * (1 - Math.random());
                double E = 2 * E0*(1- (temperature / maxIteration));
                double q = Math.random();
                double absE = Math.abs(E);
                if(absE >=1){
                    int r1 = random.nextInt(population.size());
                    int r2 = random.nextInt(population.size());
                    int r3 = random.nextInt(population.size());
                    int r4 = random.nextInt(population.size());
                    List<Double>  tour_rand = population.get(random.nextInt(population.size()));
                    // System.out.println("tour_rand" + tour_rand);
                    List<Double> newTour = new ArrayList<>();

                    if(q >= 0.5){
                        for (int i = 0; i < tour.size(); i++) {
                           double temp_cal = tour_rand.get(i) - r1*Math.abs(tour_rand.get(i) - 2*r2*tour.get(i));
                            newTour.add(temp_cal);
                        }
                    }else{
                        for (int i = 0; i < tour.size(); i++) {
                           double temp_cal = X_rabbit.get(i) + F*(population.get(r1).get(i) - population.get(r2).get(i)) + (population.get(r3).get(i) - population.get(r4).get(i));
                            newTour.add(temp_cal);
                        }
                    }
                double current_solution_fitness = fitness(graph, tour,weight);
                double new_solution_fitness = fitness(graph, newTour,weight);
                double p = metropolis_acceptance_probability(current_solution_fitness, new_solution_fitness, temperature);
                
                if(Math.random() <p){
                    tour = newTour;
                }
                }else{
                    double r = Math.random();
                    if(r >= 0.5){
                        List<Double> newTour = new ArrayList<>();
                        List<Double> Delta_population = new ArrayList<>();
                        for(int i = 0; i < tour.size(); i++){
                            double temp_cal = X_rabbit.get(i) - tour.get(i);
                            Delta_population.add(temp_cal);
                        }
                        if (absE >=0.5){
                            for(int i = 0; i < tour.size(); i++){
                                double temp_cal = Delta_population.get(i) - E*Math.abs(J*X_rabbit.get(i) - tour.get(i));
                                newTour.add(temp_cal);
                            }
                        }else{
                            for(int i = 0; i < tour.size(); i++){
                                double temp_cal = X_rabbit.get(i) + E*Math.abs(Delta_population.get(i));
                                newTour.add(temp_cal);
                            }
                        }
                        double current_solution_fitness = fitness(graph, tour,weight);
                        double new_solution_fitness = fitness(graph, newTour,weight);
                        double p = metropolis_acceptance_probability(current_solution_fitness, new_solution_fitness, temperature);
                        if(Math.random() <p){
                            tour = newTour;
                        }
                    }else{
                        List<Integer> tourendcode = randomKeyEncode(tour);
                        LLHFunctionInterface selectedFunction = choiceFunction();
                        selectedFunction.incrementCounter();

                        long start_time = System.nanoTime();

                        List<Integer> newTour = selectedFunction.apply(tourendcode);
                        long end_time = System.nanoTime();
                        long durationtime = end_time - start_time;
                        selectedFunction.setTimestamp(durationtime);
                        

                        double current_solution_fitness = discreteFitness(graph, tourendcode,weight);
                        double new_solution_fitness = discreteFitness(graph, (newTour),weight);

                        double f1 = calculateF1(selectedFunction, mu, current_solution_fitness, new_solution_fitness, durationtime);
                        selectedFunction.setF1(f1);
                        if(pre_selectedFunction == null){
                            pre_selectedFunction = neighborhoodfunctions.get((neighborhoodfunctions.indexOf(pre_selectedFunction)+1)%(neighborhoodfunctions.size()-1));
                        }
                        pre_selectedFunction.addConsec_counts(selectedFunction);
                        double f2 = calculateF2(pre_selectedFunction, selectedFunction, mu, current_solution_fitness, new_solution_fitness);
                        pre_selectedFunction.addF2(selectedFunction, f2);

                        for (LLHFunctionInterface neighborhoodfunction : neighborhoodfunctions) {
                            
                                 double score = mu*(-neighborhoodfunction.getF1() - neighborhoodfunction.getF2(pre_selectedFunction)) + delta*(end_time - neighborhoodfunction.getTimestamp());

                            double Q = Math.max(0,score+1)/(10*neighborhoodfunctions.size());
                            neighborhoodfunction.setScore(Math.max(score,Q*(Math.pow(1.5,-score))));
                        }
                        double p = metropolis_acceptance_probability(current_solution_fitness, new_solution_fitness, temperature);
                        if(Math.random() <p){
                            tourendcode = newTour;
                        }
                        double d = current_solution_fitness - discreteFitness(graph, tourendcode, weight);
                        if(d >0){
                            mu = 0.99;
                        }else{
                            mu = Math.max(0.01, mu-0.01);
                        }
                        tour = randomKeyDecode(tourendcode);
                        pre_selectedFunction = selectedFunction;
                    }   

                }
            }
            temp= beta*temp;
        }
        return X_rabbit;
    }

    public static void main(String[] args) {
        graph = Arrays.asList(
                new City("HCM",0, 0),
                new City("Ha Noi",1, 1),
                new City("Hue",2, 2),
                new City("Vung Tau",3, 3),
                new City("Da Nang",4, 4),
                new City("Hoi An",5, 5),
                new City("Ha Long",12, 6),
                new City("Van don",7, 7),
                new City("Hai Phong",8, 45),
                new City("Cao Bang",9, 9)
        );
        double weight = 1 + Math.random() * (2 - 1);
        List<Double> bestRabbit = HHOAlgorithm(graph, 50, 100,weight);
        // List<Double> pop = createPopulation(10).get(0);
        // System.out.println("pop" + pop);
        // List<Integer> test = neighborhoodfunctions.get(0).apply(randomKeyEncode(pop));
        // System.out.println("test" + test);
        System.out.println("best finest "+ fitness(graph, (bestRabbit),weight));
        System.out.println("Best Rabbit");
        List<Integer> best =  randomKeyEncode(bestRabbit);
        for (int i = 0; i < best.size(); i++) {
            System.out.print(graph.get(best.get(i)) + " -> ");

        }
    }
}