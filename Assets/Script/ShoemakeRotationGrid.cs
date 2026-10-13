using UnityEngine;

public class ShoemakeRotationGrid : MonoBehaviour
{
    [System.Serializable]
    public struct BoxInstance
    {
        public Transform transform;
        public Vector3 tumbleAxis;
        public float tumbleSpeed;
    }

    [Header("Grid Dimensions")]
    [SerializeField] private Vector3Int gridDimensions = new Vector3Int(3, 3, 3);
    [SerializeField] private float spacing = 1.8f;
    [SerializeField] private Vector3 boxScale = Vector3.one * 0.9f;

    [Header("Tumble Dynamics")]
    [SerializeField] private bool enableDynamicTumble = true;
    [SerializeField] private float minTumbleSpeed = 15f;
    [SerializeField] private float maxTumbleSpeed = 65f;

    [Header("Visuals")]
    [SerializeField] private Material boxMaterial;

    private BoxInstance[] _boxes;

    private void Awake()
    {
        GenerateGrid();
    }

    private void Update()
    {
        if (!enableDynamicTumble || _boxes == null) return;

        float dt = Time.deltaTime;
        for (int i = 0; i < _boxes.Length; i++)
        {
            // Continuous frame-rate independent delta rotation around intrinsic axis
            _boxes[i].transform.Rotate(_boxes[i].tumbleAxis, _boxes[i].tumbleSpeed * dt, Space.World);
        }
    }

    [ContextMenu("Regenerate Grid")]
    public void GenerateGrid()
    {
        // Clean existing children
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            if (Application.isPlaying)
                Destroy(transform.GetChild(i).gameObject);
            else
                DestroyImmediate(transform.GetChild(i).gameObject);
        }

        int totalBoxes = gridDimensions.x * gridDimensions.y * gridDimensions.z;
        _boxes = new BoxInstance[totalBoxes];

        Vector3 offset = new Vector3(
            (gridDimensions.x - 1) * spacing * 0.5f,
            (gridDimensions.y - 1) * spacing * 0.5f,
            (gridDimensions.z - 1) * spacing * 0.5f
        );

        int index = 0;
        for (int x = 0; x < gridDimensions.x; x++)
        {
            for (int y = 0; y < gridDimensions.y; y++)
            {
                for (int z = 0; z < gridDimensions.z; z++)
                {
                    Vector3 localPos = new Vector3(x * spacing, y * spacing, z * spacing) - offset;
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.name = $"Box_{x}_{y}_{z}";
                    cube.transform.SetParent(transform, false);
                    cube.transform.localPosition = localPos;
                    cube.transform.localScale = boxScale;

                    if (boxMaterial != null)
                    {
                        cube.GetComponent<MeshRenderer>().sharedMaterial = boxMaterial;
                    }

                    // 1. Uniform Shoemake sampling for initial un-biased orientation
                    cube.transform.localRotation = GetShoemakeUniformRotation();

                    // 2. Uniform random tumble axis on unit sphere
                    _boxes[index] = new BoxInstance
                    {
                        transform = cube.transform,
                        tumbleAxis = Random.onUnitSphere,
                        tumbleSpeed = Random.Range(minTumbleSpeed, maxTumbleSpeed)
                    };

                    index++;
                }
            }
        }
    }

    /// <summary>
    /// Generates a perfectly uniform random unit quaternion using Ken Shoemake's subgroup algorithm.
    /// Eliminates pole clustering present in naive Euler randomizations.
    /// </summary>
    public static Quaternion GetShoemakeUniformRotation()
    {
        float u1 = Random.value; // [0, 1)
        float u2 = Random.value;
        float u3 = Random.value;

        float sqrtOneMinusU1 = Mathf.Sqrt(1f - u1);
        float sqrtU1 = Mathf.Sqrt(u1);

        float twoPiU2 = 2f * Mathf.PI * u2;
        float twoPiU3 = 2f * Mathf.PI * u3;

        // Shoemake mapping: (x, y, z, w)
        float x = sqrtOneMinusU1 * Mathf.Sin(twoPiU2);
        float y = sqrtOneMinusU1 * Mathf.Cos(twoPiU2);
        float z = sqrtU1 * Mathf.Sin(twoPiU3);
        float w = sqrtU1 * Mathf.Cos(twoPiU3);

        return new Quaternion(x, y, z, w);
    }

    [ContextMenu("Re-roll All Orientations")]
    public void ReseedRotations()
    {
        if (_boxes == null) return;

        for (int i = 0; i < _boxes.Length; i++)
        {
            if (_boxes[i].transform != null)
            {
                _boxes[i].transform.localRotation = GetShoemakeUniformRotation();
                _boxes[i].tumbleAxis = Random.onUnitSphere;
            }
        }
    }
}