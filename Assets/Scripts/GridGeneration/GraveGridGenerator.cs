using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.GridGeneration
{
    internal sealed class GraveGridGenerator
    {
        public int Height { get; }

        public int Width { get; }

        public GraveGridGenerator(int height, int width)
        {
            Height = height;
            Width = width;
        }

        /// <summary>
        /// Generate a random path of open points starting from the bottom row
        /// </summary>
        public List<Vector2> GeneratePath()
        {
            var openPoints = new List<Vector2>();

            var random = new Random();
            
            var randomPoint = random.Next(Width);

            var currentX = randomPoint;
            var currentY = 0;
            var lastDirection = GridDirection.Forward;
            openPoints.Add(new Vector2(currentX, currentY));
            
            currentY++;
            openPoints.Add(new Vector2(currentX, currentY));

            while (currentY < Height - 1)
            {
                var directions = GetValidDirections(currentX, lastDirection);

                var direction = directions[random.Next(directions.Count)];

                switch (direction)
                {
                    case GridDirection.Forward:
                        currentY++;
                        break;
                    case GridDirection.Right:
                        currentX++;
                        break;
                    case GridDirection.Left:
                        currentX--;
                        break;
                }

                lastDirection = direction;
                openPoints.Add(new Vector2(currentX, currentY));
            }

            return openPoints;
        }

        /// <summary>
        /// Check the directions that can be used for the next open point
        /// </summary>
        private List<GridDirection> GetValidDirections(int x, GridDirection lastDirection)
        {
            var validDirections = new List<GridDirection>
            {
                GridDirection.Forward
            };

            if (x > 0 && lastDirection != GridDirection.Right)
            {
                validDirections.Add(GridDirection.Left);
            }

            if (x < Width - 1 && lastDirection != GridDirection.Left)
            {
                validDirections.Add(GridDirection.Right);
            }

            return validDirections;
        }
    }
}
