using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RPG;

internal class Monster
{
    public string Name { get; set; }
    public int Defense { get; set; }
    public int Offense { get; set; }
    public int Health { get; set; }
    public bool SkipTurn { get; set; }

    public void Configure()
    {
        Random rnd = new Random();

        Defense = rnd.Next(-2, 4);
        Offense = rnd.Next(-2, 3);
        Health = rnd.Next(35, 50);
        SkipTurn = false;

        CreateName();
    }

    public void CreateName()
    {
        MonsterList options = new MonsterList();

        Random rnd = new Random();

        string adjective = options.Adjectives[rnd.Next(options.Adjectives.Count)];
        string firstWord = options.FirstWords[rnd.Next(options.FirstWords.Count)];
        string secondWord = options.SecondWords[rnd.Next(options.SecondWords.Count)];

        if (rnd.Next(0, 2) == 0)
        {
            Name = $"{adjective}{secondWord.ToLower()}";
        }
        else
        {
            Name = $"{adjective} {firstWord}{secondWord.ToLower()}";
        }
    }
}