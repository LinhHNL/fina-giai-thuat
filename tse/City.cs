using System;

class City<T>
{
    private string name;
    private T x;
    private T y;

    public City(string name, T x, T y)
    {
        this.name = name;
        this.x = x;
        this.y = y;
    }

    public string GetName()
    {
        return this.name;
    }

    public T GetX()
    {
        return this.x;
    }

    public T GetY()
    {
        return this.y;
    }

    public double DistanceTo(City<T> city) 
    {   
        
        double xDistance = Math.Abs(Convert.ToDouble(GetX()) - Convert.ToDouble(city.GetX()));
        double yDistance = Math.Abs(Convert.ToDouble(GetY()) - Convert.ToDouble(city.GetY()));
        double distance = Math.Sqrt((xDistance * xDistance) + (yDistance * yDistance));
        return distance;
    }

    public override string ToString()
    {
        return GetName();
    }
}