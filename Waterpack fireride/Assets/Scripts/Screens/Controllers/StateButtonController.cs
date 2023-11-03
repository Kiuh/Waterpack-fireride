using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class StateButtonController : MonoBehaviour
{
    private Image image;

    [SerializeField]
    private Sprite sprite1;

    [SerializeField]
    private Sprite sprite2;

    [SerializeField]
    private bool isSpriteOne;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SwitchSprite()
    {
        isSpriteOne = !isSpriteOne;
        if (isSpriteOne)
        {
            UpdateSprite(sprite1);
        }
        else
        {
            UpdateSprite(sprite2);
        }
    }

    private void UpdateSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }
}
