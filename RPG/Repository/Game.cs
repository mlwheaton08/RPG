using RPG.Characters;
using RPG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Repository;

internal class Game
{
    public void Run()
    {
        Random rnd = new Random();
        var monster = new Monster()
        {
            Defense = rnd.Next(-3, 4),
            Offense = rnd.Next(-3, 4),
            Health = rnd.Next(35, 50)
        };
        monster.CreateName();

        //Console.WriteLine(monster.Name);
        //Console.ReadLine();

        // configure player character
        var player = new Player();
        player.CreatePlayer();

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
            if (!monsterDead)
            {
                playerDead = MonsterTurn(monster, player);
            }

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
            Console.WriteLine("Press 'a' to attack!");
            string playerInputAttack = Console.ReadLine();

            if (playerInputAttack == "a")
            {
                validInput = true;
                damage += rnd.Next(1, 11) + player.Offense - monster.Defense;
            }
        }
        while (!validInput);

        if (damage > 0)
        {
            Console.Clear();
            OutputDialog($"The {monster.Name} takes {damage} damage!", 20);
            Console.ReadLine();

            monster.Health -= damage;
        }
        else
        {
            Console.Clear();
            OutputDialog($"You miss!", 20);
            Console.ReadLine();
        }

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
        Console.ReadLine();

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
        Console.WriteLine($"{monster.Name}: {monster.Health}         {player.Name}: {player.Health}");
    }
}