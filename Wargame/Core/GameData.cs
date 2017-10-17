﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Wargame
{
    class GameData
    {
        internal int RoundNumber { get; set; }
        internal Stack<Character> RoundOrder;
        public BindingList<Character> Team1 = new BindingList<Character>();
        public BindingList<Character> Team2 = new BindingList<Character>();
        public BindingList<Character> AvailableCharacters = new BindingList<Character>();
        internal List<Item> Vendor = new List<Item>();
        internal List<Character> LivingCharacters => Team1.Concat(Team2).Where(c => c.Alive).OrderBy(c => c.Initiative).ToList();
        internal bool GameOver => !LivingCharacters.Any();
        internal int TeamSize = 5;
        public bool TeamsFull => Team1.Count == TeamSize && Team2.Count == TeamSize;

        public GameData() { }

        internal string InitiativeList()
        {
            StringBuilder initiativeList = new StringBuilder();
            foreach (var c in RoundOrder)
            {
                initiativeList.AppendLine($"{c.Initiative}: {c.Name}");
            }
            return initiativeList.ToString();
        }

        internal string TeamRoster(int team = 1)
        {
            StringBuilder roster = new StringBuilder();
            foreach (var c in team == 1 ? Team1 : Team2)
            {
                roster.AppendLine($"{c}");
            }
            return roster.ToString();
        }
    }
}
