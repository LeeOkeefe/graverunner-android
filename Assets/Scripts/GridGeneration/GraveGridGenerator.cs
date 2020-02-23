using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace GridGeneration
{
    internal sealed class GraveGridGenerator
    {
        public List<Vector2> GeneratePath(int height, int width)
        {
            var openPoints = new List<Vector2>();

            var random = new Random();
            
            var randomPoint = random.Next(width);

            var currentX = randomPoint;
            var currentY = 0;
            var lastDirection = GridDirection.Forward;
            openPoints.Add(new Vector2(currentX, currentY));
            
            currentY++;
            openPoints.Add(new Vector2(currentX, currentY));

            while (currentY < height - 1)
            {
                var directions = GetValidDirections(currentX, width, lastDirection);

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

        private List<GridDirection> GetValidDirections(int x, int width, GridDirection lastDirection)
        {
            var validDirections = new List<GridDirection>
            {
                GridDirection.Forward
            };

            if (x > 0 && lastDirection != GridDirection.Right)
            {
                validDirections.Add(GridDirection.Left);
            }

            if (x < width - 1 && lastDirection != GridDirection.Left)
            {
                validDirections.Add(GridDirection.Right);
            }

            return validDirections;
        }
    }
}
