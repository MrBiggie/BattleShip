using System;
using System.Collections.Generic;

public class Board{

    // three status on each point on Board, 1 for occupied, -1 for attacked or 0 for empty
    private string [,] coordinates{get;}

    public int width{get;}
    public int length{get;}

    public Board (int width, int length){
        this.width = width;
        this.length = length;

        //here, for 2 dimentional array, x for width and y for length
        this.coordinates = new string[width, length];
    }

    //initialization of coordinate map
    public void init(){
        for(int i = 0; i < this.width; i++){
            for(int j = 0; j < this.length; j++){
                coordinates[i,j] = "0";
            }
        }
    }

    //record the point that is attacked, change coordinate label to "X"
    public Boolean underAttack(Point point){
        string status = coordinates[point.x, point.y];
        if(status == "0" || status == "X"){
            coordinates[point.x, point.y] = "X";
            return false;
        }else{
            coordinates[point.x, point.y] = "X";
            return true;
        }
    }

    //add ships to point, if empty, added successfully, if occupied, fail
    public Boolean AddShipToPoint(Point point){
        if(this.coordinates[point.x, point.y] == "0"){
            this.coordinates[point.x, point.y] = "1";
            return true;
        }
        return false;
    }

    //print board status
    public void printBoard(){
        for(int i = 0; i < this.width; i++){
            for(int j = 0; j < this.length; j++){
                Console.Write(coordinates[i,j] + " ");
            }
            Console.WriteLine();
        }
    }
}