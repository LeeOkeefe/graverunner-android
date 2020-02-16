using System.Collections.Generic;
using System.Drawing;
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

        public List<Vector2> GeneratePath()
        {
            var openPoints = new List<Vector2>();

            var random = new Random();
            
            // Start with the bottom row
            // Picking one at random
            var randomPoint = random.Next(Width);

            var currentX = randomPoint;
            var currentY = 0;
            var lastDirection = GridDirection.Forward;
            openPoints.Add(new Vector2(currentX, currentY));
            
            currentY++;
            openPoints.Add(new Vector2(currentX, currentY));

            while (currentY < Height - 1)
            {
                // Loop through the logic :D
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
