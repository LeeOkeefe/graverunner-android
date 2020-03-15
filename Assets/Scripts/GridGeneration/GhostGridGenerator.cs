using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Random = System.Random;

namespace GridGeneration
{
    internal sealed class GhostGridGenerator
    {
        private readonly Random m_Random;
        private const int MaxGhostsPerRow = 2;

        public GhostGridGenerator()
        {
            m_Random = new Random();
        }

        /// <summary>
        /// Generate unique locations within a grid
        /// </summary>
        public List<Vector2> GenerateGhostLocations(int height, int width, int ghostCount)
        {
            var possibleLocations = GeneratePossibleLocations(height, width);

            var locations = new List<Vector2>();

            for (var i = 0; i < ghostCount; i++)
            {
                var location = GetAndRemoveRandomLocation(possibleLocations);
                locations.Add(location);

                RemoveFullRows(possibleLocations);
            }

            Debug.Log(locations.Count);
            return locations;
        }

        public List<Vector2> GenerateGemLocations(int rows, int gameWidth, int gemCount)
        {
            var possibleLocations = GeneratePossibleLocations(rows, gameWidth);

            var randomLocations = new List<Vector2>();

            for (var i = 0; i < gemCount; i++)
            {
                randomLocations.Add(GetAndRemoveRandomLocation(possibleLocations));
            }
            return randomLocations;
        }

        private static void RemoveFullRows(ICollection<Vector2> possibleLocations)
        {
            var groups = possibleLocations.GroupBy(g => g.y);
            foreach (var group in groups)
            {
                if (group.Count() != MaxGhostsPerRow) 
                    continue;

                Debug.Log("We have 2 ghosts with y value " + group.Key);
                foreach (var value in group)
                {
                    possibleLocations.Remove(value);
                }
            }
        }

        private static List<Vector2> GeneratePossibleLocations(int height, int width)
        {
            var locations = new List<Vector2>();

            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    locations.Add(new Vector2(i, j));
                }
            }

            return locations;
        }

        /// <summary>
        /// Returns a random location within a specified grid space
        /// </summary>
        private Vector2 GetAndRemoveRandomLocation(IList<Vector2> locations)
        {
            if (!locations.Any())
                throw new Exception("Did not have location for object");

            var randomIndex = m_Random.Next(locations.Count);
            var randomLocation = locations[randomIndex];
            locations.RemoveAt(randomIndex);
            return randomLocation;
        }
    }
}
