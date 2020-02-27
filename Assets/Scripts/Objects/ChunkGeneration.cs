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

        private float m_Threshold = 5;
        private float m_NextSpawnY = 30;
        private int m_ChunkSize = 30;

        private void Awake()
        {
            m_GraveGenerator = new GraveGridGenerator();
            m_GhostGenerator = new GhostGridGenerator();
        }

        private void Update()
        {
            if (m_Player.transform.position.y >= m_Threshold)
            {
                m_Threshold += m_ChunkSize;
                GenerateChunk();
            }
        }

        private void GenerateChunk()
        {
            var currentOffset = -13;

            var chunk = Instantiate(m_Floor, new Vector3(1.5f, m_NextSpawnY, 0), Quaternion.identity);

            currentOffset += GenerateGraveGrid(chunk.transform, currentOffset);

            GenerateGhostGrid(chunk.transform, currentOffset);

            m_NextSpawnY += m_ChunkSize;
        }

        private int GenerateGraveGrid(Transform chunk, int heightOffset)
        {
            var random = new Random();
            var numberOfRows = random.Next(4, 10);

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

        private int GenerateGhostGrid(Transform chunk, int heightOffset)
        {
            var rows = 4;
            var locations = m_GhostGenerator.GenerateGhostLocations(rows, m_GameWidth, 3);

            for (var i = 0; i < locations.Count; i++)
            {
                var go = Instantiate(m_Ghost, Vector3.zero, Quaternion.identity, chunk);
                go.transform.localPosition = new Vector2(locations[i].x - 1.5f, locations[i].y + heightOffset);
            }

            return rows;
        }
    }
}
