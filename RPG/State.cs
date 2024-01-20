using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG;

internal class State
{
    public bool SkipTurn { get; set; } = false;
    public bool Prone { get; set; } = false;
    public bool Grappled { get; set; } = false;
    public bool Paralyzed { get; set; } = false;
    public bool Unconscious { get; set; } = false;
    public bool OnFire { get; set; } = false;
    public bool Frozen { get; set; } = false;
}