import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.List;
import java.util.stream.Collectors;
import java.util.stream.IntStream;

public class RandomKeyEncoding {

    public static void main(String[] args) {
        // Example usage:
        List<Double> continuousTour = new ArrayList<>(List.of(3.2, 1.8, 4.5, 2.1));
        List<Integer> discreteTour = randomKeyEncode(new ArrayList<>(continuousTour));

        System.out.println("Continuous Tour: " + continuousTour);
        System.out.println("Discrete Tour: " + discreteTour);

        List<Double> decodedTour = randomKeyDecode(new ArrayList<>(discreteTour));
        System.out.println("Decoded Continuous Tour: " + decodedTour);

                List<Integer> discreteTou2r = randomKeyEncode(new ArrayList<>(continuousTour));
        System.out.println("Discrete Tour: " + discreteTou2r);

    }

    public static List<Integer> randomKeyEncode(List<Double> tour) {
        // Create a list of indices sorted by corresponding weights
        List<Integer> sortedIndices = IntStream.range(0, tour.size())
                .boxed()
                .sorted(Comparator.comparingDouble(tour::get))
                .collect(Collectors.toList());

        return sortedIndices;
    }

    public static List<Double> randomKeyDecode(List<Integer> tour) {
        // Create an array to store the continuous tour
        List<Double> decoded = new ArrayList<>(Collections.nCopies(tour.size(), 0.0));

        // Assign weights to cities in ascending order
        List<Double> city = IntStream.range(0, tour.size())
                .mapToDouble(i -> Math.random())
                .boxed()
                .sorted()
                .collect(Collectors.toList());

        // Replace cities with corresponding weights
        for (int i = 0; i < tour.size(); i++) {
            int idx = tour.get(i);
            decoded.set(idx, city.get(i));
        }

        return decoded;
    }
}
