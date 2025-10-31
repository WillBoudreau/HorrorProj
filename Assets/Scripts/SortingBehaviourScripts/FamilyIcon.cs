using UnityEngine;

public class FamilyIcon : MonoBehaviour
{
    [Header("Family Icon Settings")]
    [SerializeField] private Color iconColor = Color.white;
    [SerializeField] private Vector2 iconSize = new Vector2(100, 100);
    [SerializeField] private Sprite iconSprite;

    void Start()
    {
        // Set initial icon properties
        SetupIcon(iconSprite, iconColor, iconSize);
    }
    ///<summary>
    /// Set the icon up
    ///</summary>
    public void SetupIcon(Sprite sprite, Color color, Vector2 size)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }
        spriteRenderer.sprite = sprite;
        spriteRenderer.color = color;
        transform.localScale = new Vector3(size.x / 100, size.y / 100, 1);
    }
}
