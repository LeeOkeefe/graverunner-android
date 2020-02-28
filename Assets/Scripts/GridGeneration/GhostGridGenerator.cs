using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// Generate unique locations within a grid
        /// </summary>
        public List<Vector2> GenerateGhostLocations(int height, int width, int ghostCount, int gemCount)
        {
            var locations = new List<Vector2>();

            for (var i = 0; i < ghostCount + gemCount; i++)
            {
                var location = GetRandomLocation(height, width);

                while (locations.Contains(location) || locations.Count(l => l.y.Equals(location.y)) >= 2)
                {
                    Debug.Log("Tried to duplicate position or spawn more than 2 on the same Y axis");
                    location = GetRandomLocation(height, width);
                }

                locations.Add(location);
            }

            return locations;
        }

        /// <summary>
        /// Returns a random location within a specified grid space
        /// </summary>
        private Vector2 GetRandomLocation(int height, int width)
        {
            var x = m_Random.Next(width);
            var y = m_Random.Next(height);

            return new Vector2(x, y);
        }
    }
}
