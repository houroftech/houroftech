﻿using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Wargame
{
    public partial class BattleForm : Form
    {
        private GameData Game { get; set; }
        private StringBuilder Messages { get; set; }
        private GameEngine Engine { get; set; }


        public BattleForm()
        {
            InitializeComponent();
            Messages = new StringBuilder();
            btnAttack.Enabled = false;
        }

        private void BtnCreateGame_Click(object sender, EventArgs e)
        {
            Game = (new GameFactory()).CreateNewGame();
            Engine = new GameEngine(Game);
            Engine.StartNextRound();
            btnAttack.Enabled = true;
            RefreshLog();
        }

        private void RefreshLog()
        {
            txtLog.Text = Messages.Append(Game).ToString();
            txtTeam1.Text = Game.TeamRoster(1);
            txtTeam2.Text = Game.TeamRoster(2);
            Messages.Clear();
        }

        private void BtnAttack_Click(object sender, EventArgs e)
        {
            btnAttack.Enabled = false;

            var status = Engine.ProcessAttack();
            Messages.AppendLine($"{status.Item2}\r\n");

            if (!Game.RoundOrder.Any()) Engine.StartNextRound();
            RefreshLog();
            btnAttack.Enabled = !status.Item1;
        }

    }
}
