﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Wargame
{
    class Character
    {
        internal static int[] baseStats = new[] { 15, 14, 13, 12, 10, 8 };
        internal string Name { get; set; }
        internal int MaxHP { get; set; }
        internal int CurrentHP { get; set; }
        internal int Initiative { get; set; }
        internal int InitiativeModifier => DEX.Modifier;
        internal int BaseStrength { get; set; }
        internal int NumDmgDice { get; set; }
        internal CharacterInventory Inventory { get; set; }
        internal DiceRoll Roll => StatRoll(STR);
        internal bool Alive => CurrentHP > 0;
        internal string Class => GetType().Name;
        internal Stat STR { get; set; }
        internal Stat CON { get; set; }
        internal Stat DEX { get; set; }
        internal Stat INT { get; set; }
        internal Stat WIS { get; set; }
        internal Stat CHR { get; set; }

        public Character(string name, int maxHP = 20, int baseStrength = 4)
        {
            Name         = name;
            MaxHP        = maxHP;
            CurrentHP    = maxHP;
            NumDmgDice   = 1;
            BaseStrength = baseStrength;

            var rolls = new Stack<int>(baseStats.OrderBy(x => Guid.NewGuid()));
            STR = new Stat(rolls.Pop());
            CON = new Stat(rolls.Pop());
            DEX = new Stat(rolls.Pop());
            INT = new Stat(rolls.Pop());
            WIS = new Stat(rolls.Pop());
            CHR = new Stat(rolls.Pop());

            Inventory = new CharacterInventory();
        }


        internal DiceRoll StatRoll(Stat stat) => new DiceRoll(NumDmgDice, BaseStrength, stat.Modifier);

        public override string ToString() => $"{(Alive ? " " : "X")} {Name}: \t{CurrentHP}/{MaxHP} HP ({Class})";

        internal void RollInitiative() => Initiative = new DiceRoll(sides: 20, modifier: DEX.Modifier).DoRoll().Total;

        internal string PrintStats() => $"{Name} ({Class}) [STR: {STR}({STR.Modifier}) CON: {CON}({CON.Modifier}) DEX: {DEX}({DEX.Modifier}) INT: {INT}({INT.Modifier}) WIS: {WIS}({WIS.Modifier}) CHR: {CHR}({CHR.Modifier})]";
    }
}
