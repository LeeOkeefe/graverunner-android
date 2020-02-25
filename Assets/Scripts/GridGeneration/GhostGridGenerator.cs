using System.Collections.Generic;
using System.Data;
using UnityEngine;
using Random = System.Random;

namespace GridGeneration
{
    internal sealed class GhostGridGenerator
    {
        private Random m_Random;

        public GhostGridGenerator()
        {
            m_Random = new Random();
        }

        public List<Vector2> GenerateGhostLocations(int height, int width, int ghostCount)
        {
            var locations = new List<Vector2>();

            // new random

            // pick a couple of random spots between 0 and height, and 0 and width
            for (var i = 0; i < ghostCount; i++)
            {
                var location = GetRandomLocation(height, width);

                while (locations.Contains(location))
                {
                    location = GetRandomLocation(height, width);
                    break;
                }

                locations.Add(location);
            }

            return locations;
        }


        private Vector2 GetRandomLocation(int height, int width)
        {
            var x = m_Random.Next(width);
            var y = m_Random.Next(height);

            return new Vector2(x, y);
        }
    }
}
