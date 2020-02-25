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

        private int m_Rows = 6;
        private int m_Columns = 4;

        [SerializeField] private Transform m_Player;
        [SerializeField] private GameObject m_Floor;
        [SerializeField] private GameObject m_Gravestone;
        [SerializeField] private GameObject m_Coin;
        [SerializeField] private GameObject m_Ghost;

        private float m_Threshold = -5;
        private float m_NextSpawnY = 10;
        private int m_ChunkSize = 10;

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
            var chunk = Instantiate(m_Floor, new Vector3(1.5f, m_NextSpawnY, 0), Quaternion.identity);

            var numberOfRows = GenerateGraveGrid(chunk.transform, -2);

            GenerateGhostGrid(chunk.transform, -2 + m_Rows);

            m_NextSpawnY += m_ChunkSize;
        }

        private int GenerateGraveGrid(Transform chunk, int heightOffset)
        {
            // var random = new Random(4, 11);
            var random = new Random();
            var numberOfRows = random.Next(4, 10);

            var openPath = m_GraveGenerator.GeneratePath(m_Rows, m_Columns);
            //var openPath = m_GraveGenerator.GeneratePath(numberOfRows ,m_Columns);

            for (var x = 0; x < m_Columns; x++)
            {
                for (var y = 0; y < m_Rows; y++)
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

        private void GenerateGhostGrid(Transform chunk, int heightOffset)
        {
            //var availableRows = m_ChunkSize - rows;

            // call ghost generator, get coordinates for ghosts
            var rows = m_ChunkSize - m_Rows;
            var locations = m_GhostGenerator.GenerateGhostLocations(rows, m_Columns, 3);
            Debug.Log(locations.Count);
            // instantiate ghosts at locations + offsets specified by ghost generator

            for (var i = 0; i < locations.Count; i++)
            {
                var go = Instantiate(m_Ghost, new Vector2(locations[i].x, locations[i].y + heightOffset), Quaternion.identity, chunk);
                go.transform.localPosition = new Vector2(locations[i].x - 1.5f, locations[i].y + heightOffset);
            }
        }
    }
}
