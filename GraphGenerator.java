import java.io.*;
import java.util.*;

class GraphGenerator {
    public GraphGenerator() {

    }

    public void generate_file(String filename, int graph_size) {
        if (filename == null) {
            filename = "TSP" + Integer.toString(graph_size) + ".txt";
        }
        int max = 500;
        int min = 0;
        int name = 0;
        try (FileWriter fileWriter = new FileWriter(filename)) {
            for (int i = 1; i <= graph_size; i++) {
                if (i > 1) {
                    fileWriter.write("\n");
                }
                fileWriter.write(name + " ");
                fileWriter.write(Double.toString(Math.random()*(max - min + 1) + min) + " ");
                fileWriter.write(Double.toString(Math.random()*(max - min + 1) + min)) ;
                name += 1;
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public List<City> generate_graph(String filename) {
        String filePath = "" + filename;
        List<City> graph = new ArrayList<>();
        try (BufferedReader br = new BufferedReader(new FileReader(filePath))) {
            String line;
            while ((line = br.readLine()) != null) {
                String[] coordinates = line.split(" "); // assuming coordinates are comma-separated
                String name = coordinates[0].trim();
                double x = Double.parseDouble(coordinates[1].trim());
                double y = Double.parseDouble(coordinates[2].trim());
                graph.add(new City(name, x, y));
            }
            
        } catch (IOException e) {
            e.printStackTrace();
        }
        return graph;
    }
}