using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

static class PlayerScore
{
    // max scores are hardcoded, but could be counted if using tags

    public static uint Kills { get; set; }
    public const uint MaxKills = 2;

    public static uint Luciole { get; set; }
    public const uint MaxLuciole = 3;
}
