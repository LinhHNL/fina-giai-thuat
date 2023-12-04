class City{
    private String name;
    private double x;
    private double y;

    public City(String name, double x, double y){
        this.name = name;
        this.x = x;
        this.y = y;
    }

    public String getName(){
        return this.name;
    }

    public double getX(){
        return this.x;
    }

    public double getY(){
        return this.y;
    }
    public double distanceTo(City city){
        double xDistance = Math.abs(getX() - city.getX());
        double yDistance = Math.abs(getY() - city.getY());
        double distance = Math.sqrt( (xDistance*xDistance) + (yDistance*yDistance) );
        return distance;
    }
    @Override
    public String toString() {
        return getName();
    }
}
