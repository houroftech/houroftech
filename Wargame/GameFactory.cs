﻿namespace Wargame
{
    class GameFactory
    {
        public GameData CreateNewGame()
        {
            var data = new GameData();

            data.Team1.Add(new Character("Geralt"));
            data.Team1.Add(new Character("Ciri"));
            data.Team1.Add(new Character("Vesemir"));
            data.Team1.Add(new Character("Lambert"));
            data.Team1.Add(new Character("Eskel"));

            data.Team2.Add(new Character("Eredin"));
            data.Team2.Add(new Character("Imlerith"));
            data.Team2.Add(new Character("Caranthir"));
            data.Team2.Add(new Character("Ge'els"));
            data.Team2.Add(new Character("Avallac'h"));

            return data;
        }
    }
}
