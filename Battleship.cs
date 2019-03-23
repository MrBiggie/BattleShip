public class Battleship{

    private static int Width = 1;
    public int Length{get;}
    //label: record the No. of the ship, for example, the first ship with label 1, the second added ship with label 2
    public int label{get;set;}

    public Battleship(int length){
        this.Length = length;
    }
}