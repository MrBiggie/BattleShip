public class Point{
    public int x;
    public int y;

    public Point(int x, int y){
        this.x = x;
        this.y = y;
    }

    //calculate a unique serialnumber for each point 
    public int getSerialNumber(){
        return 10 * x + y -1;
    }

    //rewrite Equals method
    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        if ((obj.GetType().Equals(this.GetType())) == false)
        {
            return false;
        }
            
        Point temp = null;
        
        temp = (Point)obj;

        return this.x.Equals(temp.x) && this.y.Equals(temp.y);

    }

    //rewrite GetHashCode method
    public override int GetHashCode()
    {
        return this.x.GetHashCode() + this.y.GetHashCode();
    }
}