using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG;

internal class Game
{
    public void Run()
    {
        var monster = new Monster();
        monster.Configure();

        var player = new Player();
        player.Configure();

        Console.Clear();
        OutputDialog($"Very well. Certainly someone with these skills can handle a {monster.Name} with ease, no?");
        Console.ReadLine();
        Console.Clear();
        OutputDialog($"Get ready, {player.Name}.");
        Console.ReadLine();
        OutputDialog("On guard!");
        Console.ReadLine();

        bool playerWin = Battle(monster, player);
        BattleOver(monster, player, playerWin);
    }

    public bool Battle(Monster monster, Player player)
    {
        // lil note: small chance to get knockdown (higher if offensive player), in which case can roll again to attack

        bool playerWin = false;
        bool endBattle = false;
        do
        {
            bool monsterDead = PlayerTurn(monster, player);
            bool playerDead = false;
            if (!monsterDead && !monster.SkipTurn)
            {
                playerDead = MonsterTurn(monster, player);
            }
            monster.SkipTurn = false;

            if (monsterDead)
            {
                playerWin = true;
                endBattle = true;
            }
            else if (playerDead)
            {
                playerWin = false;
                endBattle = true;
            }
        }
        while (!endBattle);

        return playerWin;
    }

    public bool PlayerTurn(Monster monster, Player player)
    {
        Random rnd = new Random();
        int damage = 0;

        bool validInput = false;
        do
        {
            Console.Clear();
            BattleHeader(monster, player);
            Console.WriteLine("'a' to attack");
            Console.WriteLine("'p' to use potion");
            Console.WriteLine("'n' to cast net");
            string playerInputAttack = Console.ReadLine();

            switch (playerInputAttack)
            {
                case "a":
                    {
                        validInput = true;
                        damage += rnd.Next(1, 11) + player.Offense + player.AttackBuff - monster.Defense;

                        if (damage > 0)
                        {
                            Console.Clear();
                            OutputDialog($"The {monster.Name} takes {damage - player.AttackBuff} damage!", 20);
                            if (player.AttackBuff > 0) OutputDialog($" Plus an additional {player.AttackBuff} points!", 20);
                            Console.ReadLine();

                            monster.Health -= damage;
                        }
                        else
                        {
                            Console.Clear();
                            OutputDialog($"You miss!", 20);
                            Console.ReadLine();
                        }

                        player.AttackBuff = 0;
                    }
                    break;
                case "p":
                    {
                        IItem potion = player.Inventory.Find(item => item.Name == "Potion");

                        if (potion.Quantity > 0)
                        {
                            validInput = true;
                            Console.Clear();
                            OutputDialog(potion.SuccessMessage, 20);
                            Console.ReadLine();
                            potion.Use(monster, player);
                        }
                        else
                        {
                            Console.Clear();
                            OutputDialog($"You're all out of this item!", 20);
                            Console.ReadLine();
                        }
                    }
                    break;
                case "n":
                    {
                        IItem net = player.Inventory.Find(item => item.Name == "Net");

                        if (net.Quantity > 0)
                        {
                            validInput = true;

                            if (rnd.Next(1, 3) == 2)
                            {
                                Console.Clear();
                                OutputDialog(net.SuccessMessage, 20);
                                Console.ReadLine();
                                net.Use(monster, player);
                            }
                            else
                            {
                                Console.Clear();
                                OutputDialog(net.FailMessage, 20);
                                Console.ReadLine();
                            }
                        }
                        else
                        {
                            Console.Clear();
                            OutputDialog($"You're all out of this item!", 20);
                            Console.ReadLine();
                        }
                    }
                    break;
            }
        }
        while (!validInput);

        bool monsterDead = false;
        if (monster.Health <= 0) monsterDead = true;
        return monsterDead;
    }

    public bool MonsterTurn(Monster monster, Player player)
    {
        Random rnd = new Random();
        int damage = rnd.Next(1, 11) + monster.Offense - player.Defense;

        Console.Clear();
        BattleHeader(monster, player);
        OutputDialog($"The {monster.Name} attacks!", 20);
        Console.WriteLine();
        OutputDialog("..........................", 30);

        if (damage > 0)
        {
            Console.Clear();
            OutputDialog($"You take {damage} damage!", 20);
            Console.ReadLine();

            player.Health -= damage;
        }
        else
        {
            Console.Clear();
            OutputDialog($"It misses!", 20);
            Console.ReadLine();
        }

        bool playerDead = false;
        if (player.Health <= 0) playerDead = true;
        return playerDead;
    }

    public void BattleOver(Monster monster, Player player, bool playerWin)
    {
        Console.Clear();
        Console.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
        BattleHeader(monster, player);
        Console.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
        Console.ReadLine();

        if (playerWin)
        {
            Console.Clear();
            OutputDialog($"Rejoice! The {monster.Name} has been slain!");
            Console.ReadLine();

            Console.Clear();
            OutputDialog($"All hail {player.Name}, {player.Title}!");
            Console.ReadLine();

            Console.Clear();
            OutputDialog("THE END");
            Console.ReadLine();
        }
        else
        {
            Console.Clear();
            OutputDialog($"Oh heavens, I can't watch!");
            Console.ReadLine();

            Console.Clear();
            OutputDialog($"The {monster.Name} has denied our only hope!");
            Console.ReadLine();

            Console.Clear();
            OutputDialog($"Rest easy, {player.Name}, {player.Title}...");
            Console.ReadLine();

            Console.Clear();
            OutputDialog("THE END");
            Console.ReadLine();
        }
    }

    public static void OutputDialog(string sentence, int timeOut = 40)
    {
        foreach (char letter in sentence)
        {
            Console.Write(letter);
            Thread.Sleep(timeOut);
        }
    }

    public void BattleHeader(Monster monster, Player player)
    {
        Console.WriteLine($"{monster.Name}: {monster.Health}         {player.Name}: {player.Health}   Attack buff: {player.AttackBuff}");
    }
}