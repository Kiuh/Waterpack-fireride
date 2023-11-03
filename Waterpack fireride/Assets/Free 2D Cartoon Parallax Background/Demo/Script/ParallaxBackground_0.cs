using UnityEngine;

public class ParallaxBackground_0 : MonoBehaviour
{
    [Header("Layer Settings")]
    public float[] Layer_Speed = new float[7];
    public GameObject[] Layer_Objects = new GameObject[7];

    [SerializeField]
    private Transform targetCamera;
    private float[] startPos = new float[7];
    private float boundSizeX;
    private float sizeX;

    private void Start()
    {
        sizeX = Layer_Objects[0].transform.localScale.x;
        boundSizeX = Layer_Objects[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        for (int i = 0; i < 5; i++)
        {
            startPos[i] = targetCamera.position.x;
        }
    }

    private void Update()
    {
        for (int i = 0; i < 5; i++)
        {
            float temp = targetCamera.position.x * (1 - Layer_Speed[i]);
            float distance = targetCamera.position.x * Layer_Speed[i];
            Layer_Objects[i].transform.position = new Vector3(
                startPos[i] + distance,
                Layer_Objects[i].transform.position.y,
                Layer_Objects[i].transform.position.z
            );
            if (temp > startPos[i] + (boundSizeX * sizeX))
            {
                startPos[i] += boundSizeX * sizeX;
            }
            else if (temp < startPos[i] - (boundSizeX * sizeX))
            {
                startPos[i] -= boundSizeX * sizeX;
            }
        }
    }
}
