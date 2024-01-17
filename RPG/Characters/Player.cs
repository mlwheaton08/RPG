using RPG.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Characters;

internal class Player
{
    public string Name { get; set; }
    public string Title { get; set; }
    public int Defense { get; set; }
    public int Offense { get; set; }
    public int Health { get; set; }

    public void CreatePlayer()
    {
        Game.OutputDialog("What is your name, adventurer? ");
        string playerInputName = Console.ReadLine();
        Name = playerInputName;
        Game.OutputDialog("And what is your title? ");
        string playerInputTitle = Console.ReadLine();
        Title = playerInputTitle;
        Console.Clear();
        Game.OutputDialog($"We gladly welcome {Name}, {Title}!");
        Console.ReadLine();

        bool validInput = false;
        do
        {
            Console.Clear();
            Game.OutputDialog("Now what are your skills?");
            Console.WriteLine();
            Console.WriteLine("[Type 'o' for offense, or type 'd' for defense.]");
            string playerInputStats = Console.ReadLine();

            if (playerInputStats == "o")
            {
                validInput = true;
                Offense = 3;
                Defense = -1;
                Health = 40;
            }
            if (playerInputStats == "d")
            {
                validInput = true;
                Offense = -1;
                Defense = 3;
                Health = 50;

            }
        }
        while (!validInput);
    }
}