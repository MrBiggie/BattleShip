using System;

namespace battle_ship
{
    class Program
    {
        public static int DEFAULT_WIDTH = 10;
        public static int DEFAULT_LENGTH = 10;
        static void Main(string[] args)
        {
            Console.WriteLine("How many ships you want to add?");
            int num_of_ships = int.Parse(Console.ReadLine());
            

            State_tracker state_Tracker = new State_tracker();

            state_Tracker.create_board(DEFAULT_WIDTH, DEFAULT_LENGTH);
            state_Tracker.printboard();

            int i = 0;

            while(i < num_of_ships){
                String direction;
                Point start_point;
                int length_of_ship;
                int start_x;
                int start_y;

                Console.WriteLine("what is the length of your ship?");
                length_of_ship = int.Parse(Console.ReadLine());


                do{
                    Console.WriteLine("at which start point you want to start to put your ship? for example: < 2,3 > ");
                    string[] input = Console.ReadLine().Split(',');
                    start_x = int.Parse(input[0]);
                    start_y = int.Parse(input[1]);
                    start_point = new Point(start_x, start_y);
                }while(! (start_x >= 0 && start_y >= 0 && start_x < DEFAULT_WIDTH && start_y < DEFAULT_LENGTH));

                do{
                    Console.WriteLine("at which direction you want to put your ship? for example: N, W, S, E");
                    direction = Console.ReadLine();
                }while(direction != "N" && direction != "W" && direction != "S" && direction != "E");

                Battleship battleship = new Battleship(length_of_ship);
                if(state_Tracker.place_ship(battleship, start_point, direction)){
                    Console.WriteLine($"Ship with length {battleship.Length} has been added to board successfully");
                    i++;
                    state_Tracker.printboard();
                }else{
                    continue;
                }
            }

            while(state_Tracker.LoseOrNotYet() == "Not Yet"){
                Console.WriteLine("which point do you want to attack? for example: <3,4>");
                string[] input = Console.ReadLine().Split(',');
                Point point = new Point(int.Parse(input[0]), int.Parse(input[1]));
                Console.WriteLine(state_Tracker.under_attack(point));
                state_Tracker.printboard();
            }
            Console.WriteLine("DEFEAT !!!!!!!!");
        }
    }
}
