using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UIElements;
using Random = System.Random;

namespace Assets.Scripts.Objects
{
    internal sealed class Spawn : MonoBehaviour
    {
        [SerializeField] private Transform[] m_SpawnLocations;
        [SerializeField] private GameObject[] m_SpawnObjects;

        [SerializeField] [Range(0, 100)] private float[] m_SpawnProbabilities;
        [SerializeField] private float m_SecondsToSpawn = 1f;

        private static Random m_Random;
        private Slot[] m_Grid;
        private int m_Rows = 4;
        private int m_Columns = 4;

        private void Start()
        {
            m_Random = new Random();
            m_Grid = new Slot[m_Rows * m_Columns];
            StartCoroutine(GenerateRandomSpawns());
        }

        /// <summary>
        /// Spawns game objects randomly between the spawn locations
        /// </summary>
        private IEnumerator GenerateRandomSpawns()
        {
            while (true)
            {
                GenerateGrid();
                //RemoveGridBlocks();

                for (var i = 0; i < m_Grid.Length; i++)
                {
                    if (m_Grid[i] == null)
                        continue;

                    Instantiate(m_Grid[i].GameObject, m_Grid[i].SpawnPosition, Quaternion.identity);

                    if (i != 0 && i % 4 == 0)
                    {
                        yield return new WaitForSeconds(m_SecondsToSpawn);
                    }
                }
            }
        }

        private void GenerateGrid()
        {
            var rowIndex = 0;
            var columnIndex = 0;

            for (var i = 0; i < m_Grid.Length; i++)
            {
                var randomSlot = CreateRandomSlot(rowIndex, columnIndex);

                if (m_Grid.Contains(randomSlot))
                {
                    randomSlot = null;
                }

                m_Grid[i] = randomSlot;

                columnIndex++;
                if (i != 0 && i % m_Columns == 0)
                {
                    rowIndex++;
                    columnIndex = 0;
                }
            }
        }

        private void RemoveGridBlocks()
        {
            if (m_Grid.Take(4).All(x => x.IsGraveStone))
            {
                Debug.Log("DESTROYED 4 IN A ROW");
                Destroy(m_Grid[0].GameObject);
            }

            var duplicates = m_Grid.GroupBy(x => x.X).Where(g => g.Skip(1).Any()).SelectMany(g => g);
            foreach (var dupe in duplicates)
            {
                Debug.Log("Destroyed duplicate X");
                //m_Grid.Remove(dupe);
                //TODO: causing unity crash. Find any slots with the same X axis then remove them from the list before instantiation
            }
        }

        private Slot CreateRandomSlot(int x, int y)
        {
            var randomNumber = m_Random.Next(0, 4);
            var randomObject = GetRandomObjects(4);
            var randomLocation = GetRandomElements(m_SpawnLocations, 4);
            var slot = new Slot(randomObject[randomNumber], randomLocation[randomNumber].position, x, y);
            return slot;
        }

        /// <summary>
        /// Returns the specified number of random elements from a collection 
        /// </summary>
        private static List<T> GetRandomElements<T>(IReadOnlyList<T> collection, int amount)
        {
            var randomElements = new List<T>();

            for (var i = 0; i < amount; i++)
            {
                var randomNumber = m_Random.Next(collection.Count);

                while (randomElements.Contains(collection[randomNumber]))
                {
                    randomNumber = m_Random.Next(collection.Count);
                }

                randomElements.Add(collection[randomNumber]);
            }

            return randomElements;
        }

        /// <summary>
        /// Get random game objects based on their probabilities
        /// </summary>
        private List<GameObject> GetRandomObjects(int amountOfObjects)
        {
            var randomObjects = new List<GameObject>();

            for (var j = 0; j < amountOfObjects; j++)
            {
                for (var i = 0; i < m_SpawnObjects.Length; i++)
                {
                    var random = UnityEngine.Random.value;

                    if (random <= m_SpawnProbabilities[i])
                    {
                        randomObjects.Add(m_SpawnObjects[i]);
                    }
                }
            }

            return randomObjects;
        }
    }
}
