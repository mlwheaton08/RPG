using RPG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RPG;

internal class Player
{
    public string Name { get; set; }
    public string Title { get; set; }
    public int Defense { get; set; }
    public int Offense { get; set; }
    public int AttackBuff { get; set; }
    public int Health { get; set; }
    public State State { get; set; }
    public List<IItem> Inventory { get; set; } = new List<IItem>();

    public void Configure()
    {
        AttackBuff = 0;
        Game.OutputDialog("What is your name, adventurer? ");
        string playerInputName = Console.ReadLine();
        Name = playerInputName;
        Game.OutputDialog("And what is your title? ");
        string playerInputTitle = Console.ReadLine();
        Title = playerInputTitle;
        Console.Clear();
        Game.OutputDialog($"We gladly welcome {Name}, {Title}!");
        Console.ReadLine();

        Potion potion = new Potion()
        {
            Name = "Potion",
            Quantity = 2,
            SuccessMessage = "You gain 10 points of health!"
        };
        Inventory.Add(potion);

        Net net = new Net()
        {
            Name = "Net",
            Quantity = 1,
            SuccessMessage = "You've successfully trapped the enemy and can take another turn, with an added attack buff!",
            FailMessage = "You fail to trap the enemy."
        };
        Inventory.Add(net);

        Console.Clear();
        Game.OutputDialog($"Here, take these.");
        Console.WriteLine();
        Console.WriteLine("[you're handed 2 glass containers containing red liquid]");
        Console.WriteLine("[...and also what appears to be a fishing net]");
        Console.ReadLine();

        Console.Clear();
        Game.OutputDialog($"That liquid is a healing potion. The net will serve you when the time is right. You have an important battle ahead of you.");
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
                Defense = -3;
                Health = 40;
            }
            if (playerInputStats == "d")
            {
                validInput = true;
                Offense = -3;
                Defense = 3;
                Health = 50;
            }
        }
        while (!validInput);
    }

    public void ClearState()
    {
        State.SkipTurn = false;
        State.Prone = false;
        State.Grappled = false;
        State.Paralyzed = false;
        State.Unconscious = false;
        State.OnFire = false;
        State.Frozen = false;
    }
}