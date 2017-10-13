﻿using System;
using System.Linq;

namespace Wargame
{
    class GameEngine
    {
        private Random rng = new Random();
        private GameData gd;

        public GameEngine(GameData gameData)
        {
            gd = gameData;
        }


        internal int RollDice(int num = 1, int sides = 6)
        {
            return Enumerable.Range(1, num).Aggregate(0, (acc, _) => acc += rng.Next(1, sides + 1));
        }

        internal string DoAttack(Character attacker, Character defender)
        {
            var dmg = RollDice(1, 6);
            defender.CurrentHP -= dmg;
            if (!defender.Alive)
                return $"{attacker.Name} killed {defender.Name} (by dealing {dmg} damage)";
            return $"{attacker.Name} dealt {dmg} damage to {defender.Name}";
        }

        internal void StartNextRound()
        {
            gd.RoundNumber++;
            gd.RoundOrder.Clear();
            var initlist = (from c in gd.Team1.Concat(gd.Team2)
                            where c.Alive
                            select new
                            {
                                aCharacter = c,
                                InitiativeRoll = RollDice(1, 20)
                            });

            foreach (var i in initlist.OrderBy(l => l.InitiativeRoll))
            {
                gd.RoundOrder.Push(i.aCharacter);
                i.aCharacter.Initiative = i.InitiativeRoll;
            }
            // todo: add stat modifiers
        }

        internal Tuple<bool, string> ProcessAttack()
        {
            if (!gd.Team1.Any(t1 => t1.Alive)) // no one alive on team1
                return Tuple.Create(true, "Team 2 won!\r\nGame Over");
            else if (!gd.Team2.Any(t2 => t2.Alive)) // no one alive on team2
                return Tuple.Create(true, "Team 1 won!\r\nGame Over");

            var attacker = gd.RoundOrder.Pop();
            if (!attacker.Alive) return Tuple.Create(false, $"{attacker.Name} can't attack because they're dead. skipping to next player.");

            return Tuple.Create(false, $"{DoAttack(attacker, (gd.Team1.Contains(attacker) ? gd.Team2 : gd.Team1).Where(x => x.Alive).ToArray().OrderBy(q => Guid.NewGuid()).First())}");
        }
    }
}
