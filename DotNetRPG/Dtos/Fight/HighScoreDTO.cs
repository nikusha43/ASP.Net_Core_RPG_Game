﻿namespace DotNetRPG.Dtos.Fight
{
    public class HighScoreDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }=string.Empty;
        public int Fights { get; set; }
        public int Vicoties { get; set; }
        public int Defeats { get; set; }

    }
}
