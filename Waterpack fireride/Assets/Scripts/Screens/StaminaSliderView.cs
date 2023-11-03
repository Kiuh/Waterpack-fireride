using Player;
using UnityEngine;
using UnityEngine.UI;

public class StaminaSliderView : MonoBehaviour
{
    [SerializeField]
    private Stamina stamina;

    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    private void Update()
    {
        slider.value = stamina.StaminaValue.Value / stamina.StaminaValue.MaxValue;
    }
}
