using System.Linq;
using Assets.Scripts.GridGeneration;
using UnityEngine;

namespace Assets.Scripts.Objects
{
    internal sealed class Spawn : MonoBehaviour
    {
        [SerializeField] private GameObject[] m_SpawnObjects;

        private int m_Rows = 8;
        private int m_Columns = 4;
        private GraveGridGenerator m_GraveGenerator;

        private float timeSinceLastGridGeneration = 0f;

        private void Awake()
        {
            m_GraveGenerator = new GraveGridGenerator(m_Rows, m_Columns);
        }

        private void Start()
        {
            GenerateGraveGrid(5);
        }

        private void Update()
        {
            timeSinceLastGridGeneration += Time.deltaTime;
            if (timeSinceLastGridGeneration >= 25f)
            {
                timeSinceLastGridGeneration -= 25f;
                GenerateGraveGrid(5);
            }
        }

        private void GenerateGraveGrid(int heightOffset)
        {
            var openPath = m_GraveGenerator.GeneratePath();

            for (var x = 0; x < m_Columns; x++)
            {
                for (var y = 0; y < m_Rows; y++)
                {
                    var prefab = openPath.Any(v => v.x == x && v.y == y)
                        ? m_SpawnObjects[1]
                        : m_SpawnObjects[0];

                    Instantiate(prefab, new Vector2(x, y + heightOffset), Quaternion.identity);
                }
            }
        }
    }
}
