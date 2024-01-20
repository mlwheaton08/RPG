using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG;

internal class Net : IItem
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public string SuccessMessage { get; set; }
    public string FailMessage { get; set; }

    public void Use(Monster monster, Player player)
    {
        monster.State.SkipTurn = true;
        player.AttackBuff = 5;

        if (Quantity > 0)
        {
            Quantity--;
        }
    }
}