using Common.Extensions;
using DailyCode.Base;
using Common.Models;

namespace DailyCode.Year2022.Day08
{
    internal class Day08 : BaseDay
    {
        private Tree[][] _trees = null!;

        public Day08(string sessionId) : base(sessionId)
        {
        }

        protected override void SetupData(FileInputCollection fileInputs)
            => _trees = fileInputs.ConvertAll(x => x.Trim()).Select(line => line.Select(letter => new Tree(int.Parse(letter.ToString()))).ToArray()).ToArray();

        protected override string RunPart1()
        {
            CheckTreesVisibilityFromXZero(_trees);
            CheckTreesVisibilityFromXZero(_trees, reverse: true);
            var pivot = _trees.Pivot();
            CheckTreesVisibilityFromXZero(pivot);
            CheckTreesVisibilityFromXZero(pivot, reverse: true);

            return _trees.Flatten().Count(x => x.IsVisible).ToString();
        }

        protected override string RunPart2()
        {
            CheckTreesScenicViewFromXZero(_trees);
            CheckTreesScenicViewFromXZero(_trees, true);
            var pivot = _trees.Pivot();
            CheckTreesScenicViewFromXZero(pivot);
            CheckTreesScenicViewFromXZero(pivot, true);

            return _trees.Flatten().Max(x => x.ScenicScore).ToString();
        }

        private static Tree[][] CheckTreesVisibilityFromXZero(Tree[][] trees, bool reverse = false)
        {
            for (int y = 0; y < trees.Length; y++)
            {
                if (reverse)
                    trees[y] = trees[y].Reverse().ToArray();

                Tree? previousHighestTree = null;
                for (int x = 0; x < trees[y].Length; x++)
                {
                    var tree = trees[y][x];
                    if (x == 0 || x == trees[y].Length - 1)
                    {
                        tree.IsVisible = true;
                        previousHighestTree = tree;
                    }
                    else if (tree.Height > previousHighestTree!.Height)
                    {
                        tree.IsVisible = true;
                        if (tree.Height < 9)
                            previousHighestTree = tree;
                        else
                            break;
                    }
                }
            }

            return trees;
        }

        private static Tree[][] CheckTreesScenicViewFromXZero(Tree[][] trees, bool reverse = false)
        {
            for (int y = 0; y < trees.Length; y++)
            {
                if (reverse)
                    trees[y] = trees[y].Reverse().ToArray();

                List<Tree> previousTrees = new();
                for (int x = 0; x < trees[y].Length; x++)
                {
                    var tree = trees[y][x];
                    if (y == 0)
                    {
                        tree.ScenicScore *= 0;
                        previousTrees.Add(tree);
                    }
                    else
                    {
                        var blockingTree = previousTrees.LastOrDefault(x => x.Height >= tree.Height);
                        previousTrees.Add(tree);
                        tree.ScenicScore *= blockingTree != null ? x - previousTrees.IndexOf(blockingTree) : x;
                    }
                }
            }

            return trees;
        }
    }
}