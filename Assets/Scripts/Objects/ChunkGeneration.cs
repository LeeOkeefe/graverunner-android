using System;
using System.Linq;
using GridGeneration;
using UnityEngine;
using Random = System.Random;

namespace Objects
{
    internal sealed class ChunkGeneration : MonoBehaviour
    {
        private GraveGridGenerator m_GraveGenerator;
        private GhostGridGenerator m_GhostGenerator;

        private int m_GameWidth = 4;

        [SerializeField] private Transform m_Player;
        [SerializeField] private GameObject m_Floor;
        [SerializeField] private GameObject m_Gravestone;
        [SerializeField] private GameObject m_Coin;
        [SerializeField] private GameObject m_Ghost;
        [SerializeField] private GameObject m_Gem;

        private float m_Threshold = 5;
        private float m_NextSpawnY = 30;
        private int m_ChunkSize = 30;
        private int m_GridPadding = 1;
        private int m_InitialChunkOffset = -13;

        private Random m_Random;

        private int m_MaxGhostRows = 8;

        private void Awake()
        {
            m_GraveGenerator = new GraveGridGenerator();
            m_GhostGenerator = new GhostGridGenerator();
        }

        private void Start()
        {
            m_Random = new Random();
        }

        private void Update()
        {
            if (m_Player.transform.position.y >= m_Threshold)
            {
                m_Threshold += m_ChunkSize;
                GenerateChunk();
            }
        }

        /// <summary>
        /// Generates the next chunk containing grids
        /// </summary>
        private void GenerateChunk()
        {
            var usedRows = 2;

            var chunk = Instantiate(m_Floor, new Vector3(1.5f, m_NextSpawnY, 0), Quaternion.identity);

            usedRows += GenerateGraveGrid(chunk.transform, m_InitialChunkOffset + usedRows, 4, 10);
            
            usedRows += m_GridPadding;
            
            usedRows += GenerateGhostGrid(chunk.transform, m_InitialChunkOffset + usedRows);
            
            usedRows += m_GridPadding;
            
            var remainingRows = m_ChunkSize - usedRows - 2;
            
            usedRows += GenerateGraveGrid(chunk.transform, m_InitialChunkOffset + usedRows, remainingRows, remainingRows);
            
            if (usedRows > m_ChunkSize)
            {
                throw new Exception($"Max rows in chunk is {m_ChunkSize} but contained {usedRows} rows");
            }

            m_NextSpawnY += m_ChunkSize;
        }

        /// <summary>
        /// Generate a grid of gravestones with a path of coins
        /// </summary>
        private int GenerateGraveGrid(Transform chunk, int heightOffset, int minRows, int maxRows)
        {
            var numberOfRows = m_Random.Next(minRows, maxRows + 1);

            var openPath = m_GraveGenerator.GeneratePath(numberOfRows, 4);

            for (var x = 0; x < m_GameWidth; x++)
            {
                for (var y = 0; y < numberOfRows; y++)
                {
                    var prefab = openPath.Any(v => v.x == x && v.y == y)
                        ? m_Coin
                        : m_Gravestone;

                    var go = Instantiate(prefab, new Vector2(x, y + heightOffset), Quaternion.identity, chunk);

                    go.transform.localPosition = new Vector2(x - 1.5f, y + heightOffset);
                }
            }

            return numberOfRows;
        }

        /// <summary>
        /// Generate a grid of ghosts, with a chance for gems in the same grid
        /// </summary>
        private int GenerateGhostGrid(Transform chunk, int heightOffset)
        {
            var rows = m_Random.Next(2, m_MaxGhostRows);
            var ghostCount = m_Random.Next(1, rows + 1);
            var gemCount = GenerateChanceOfGems();
            var ghostLocations = m_GhostGenerator.GenerateGhostLocations(rows, m_GameWidth, ghostCount);

            Debug.Log($"Ghost Count: {ghostCount}  Gem Count: {gemCount}");
            
            foreach (var location in ghostLocations)
            {
                var go = Instantiate(m_Ghost, Vector3.zero, Quaternion.identity, chunk);
                go.transform.localPosition = new Vector2(location.x - 1.5f, location.y + heightOffset);
            }

            if (gemCount > 0)
            {
                var gemLocations = m_GhostGenerator.GenerateGemLocations(rows, m_GameWidth, gemCount);
                foreach (var location in gemLocations)
                {
                    var gem = Instantiate(m_Gem, Vector3.zero, Quaternion.identity, chunk);
                    gem.transform.localPosition = new Vector2(location.x - 1.5f, location.y + heightOffset);
                }
            }

            return rows;
        }

        /// <summary>
        /// Generate a number between 0 and 3 based on percentages
        /// </summary>
        private int GenerateChanceOfGems()
        {
            var value = m_Random.Next(0, 100);

            if (value <= 3)
            {
                return 3;
            }

            if (value <= 10)
            {
                return 2;
            }

            if (value <= 30)
            {
                return 1;
            }

            return 0;
        }
    }
}
