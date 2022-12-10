﻿namespace DailyCode.Year2022.Models
{
    public sealed class Tree
    {
        public Tree(int height)
        {
            Height = height;
        }

        public int Height { get; set; }

        public int ScenicScore { get; set; } = 1;

        public bool IsVisible { get; set; }
    }
}