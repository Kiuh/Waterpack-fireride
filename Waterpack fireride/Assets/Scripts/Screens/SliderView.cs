using Player;
using UnityEngine;

public class SliderView : MonoBehaviour
{
    [SerializeField]
    private Fuel fuel;

    private Material material;

    private void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    private void Update()
    {
        material.SetFloat("_Slider", fuel.FuelValue.Value / fuel.FuelValue.MaxValue);
    }
}
