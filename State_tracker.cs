using System;
using System.Collections.Generic;

public class State_tracker{
    private Board board;
    //record status of each coordinates, key would be serial number of each point, value would be status, 0 for not occupied, or ship_label for the occupying ship
    private Dictionary<int, int> point_status_mapping{get;}
    //list of added ships
    private List<int> ship_list;

    public State_tracker(){
        point_status_mapping = new Dictionary<int, int>(); 
        ship_list = new List<int>();
    }


    public void create_board(int width, int length){
        board = new Board(width, length);
        board.init();
        for(int i = 0; i < board.width; i++){
            for(int j = 0; j < board.length; j++){
                point_status_mapping[new Point(i, j).getSerialNumber()] = 0;
            }
        }
    }

    public Boolean place_ship(Battleship battleship, Point start_point, string direction){

        battleship.label = ship_list.Count + 1;

        int ship_length = battleship.Length;
        int ship_label = battleship.label;

        //check validility of start point
        if(!(start_point.x >= 0 && start_point.x <= (board.width - 1) && 
            start_point.y >= 0 && start_point.y <= (board.length - 1))){
                Console.WriteLine("invalid start point");
                return false;
        }

        //check validility of direction
        if (direction != "N" && direction != "W" && direction != "S" && direction != "E"){
                Console.WriteLine("invalid direction");
                return false;
        }


        switch (direction){
            //north
            case "N":
                int destination_x_n = start_point.x - ship_length + 1;
                if (destination_x_n < 0){
                    Console.WriteLine("ship can't be placed, exceeding the board");
                    return false;
                }

                //check collision with other ships
                for(int i = start_point.x; i >= destination_x_n; i--){
                    if(checkStatusOfPoint(i, start_point.y)){
                        Console.WriteLine("ship can't be placed, colliding with other ships");
                        return false;
                    }
                }
                //place ship
                for(int i = start_point.x; i >= destination_x_n; i--){
                    Point point = new Point(i, start_point.y);
                    board.AddShipToPoint(point);
                    this.point_status_mapping[point.getSerialNumber()] = ship_label;
                }
                break;

            //west
            case "W":
                int destination_y_w = start_point.y - ship_length + 1;
                if (destination_y_w < 0){
                    Console.WriteLine("ship can't be placed, exceeding the board");
                    return false;
                }
                //check collision with other ships
                for(int i = start_point.y; i >= destination_y_w; i--){
                    if(checkStatusOfPoint(start_point.x, i)){
                        Console.WriteLine("ship can't be placed, colliding with other ships");
                        return false;
                    }
                }
                //place ship
                for(int i = start_point.y; i >= destination_y_w; i--){
                    Point point = new Point(start_point.x, i);
                    board.AddShipToPoint(point);
                    this.point_status_mapping[point.getSerialNumber()] = ship_label;
                }
                break;

            //south 
            case "S":
                int destination_x_s = start_point.x + ship_length - 1;
                if (destination_x_s > (board.width - 1)){
                    Console.WriteLine("ship can't be placed, exceeding the board");
                    return false;
                }
                //check collision with other ships
                for(int i = start_point.x; i <= destination_x_s; i++){
                    if(checkStatusOfPoint(i, start_point.y)){
                        Console.WriteLine("ship can't be placed, colliding with other ships");
                        return false;
                    }
                }
                //place ship
                for(int i = start_point.x; i <= destination_x_s; i++){
                    Point point = new Point(i, start_point.y);
                    board.AddShipToPoint(point);
                    this.point_status_mapping[point.getSerialNumber()] = ship_label;
                }
                break;

            //east
            case "E":
                int destination_y_e = start_point.y + ship_length - 1;
                //check whether exceeds the board boundary
                if (destination_y_e > (board.length - 1)){
                    Console.WriteLine("ship can't be placed, exceeding the board");
                    return false;
                }
                //check collision with other ships
                for(int i = start_point.y; i <= destination_y_e; i++){
                    if(checkStatusOfPoint(start_point.x, i)){
                        Console.WriteLine("ship can't be placed, colliding with other ships");
                        return false;
                    }
                }
                //place ship
                for(int i = start_point.y; i <= destination_y_e; i++){
                    Point point = new Point(start_point.x, i);   
                    board.AddShipToPoint(point);
                    this.point_status_mapping[point.getSerialNumber()] = ship_label;
                }
                break;
        }

        ship_list.Add(battleship.Length);
        return true;
 
    }
    //get attack on specific point
    public string under_attack(Point point){
        Boolean hit_or_not = board.underAttack(point);
        if(hit_or_not){
            ship_list[point_status_mapping[point.getSerialNumber()] - 1]--;
            return "hit";
        }else{
            return "miss";
        }
    }

    //determine Defeat or not yet
    public string LoseOrNotYet(){
        foreach (int shiplength in ship_list){
            if(shiplength > 0){
                return "Not Yet";
            }
        }
        return "Lose";
    }

    //print board status
    public void printboard(){
        board.printBoard();
    }

    //true if there is already ship on that point, or return false
    public Boolean checkStatusOfPoint(int x, int y){
        Point point = new Point(x , y);
        if (this.point_status_mapping[point.getSerialNumber()] != 0){
            return true;
        }
        return false;
    }

}
