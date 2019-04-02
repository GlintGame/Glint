using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

static class PlayerScore
{
    // max scores are hardcoded, but could be counted if using tags

    public static uint Kills = 0;
    public const uint MaxKills = 2;

    public static uint Luciole = 0;
    public static uint MaxLuciole = 0;

    public static void Reset()
    {
        PlayerScore.Kills = 0;
        PlayerScore.Luciole = 0;
        PlayerScore.MaxLuciole = 0;
    }
}
