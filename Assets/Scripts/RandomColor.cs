using UnityEngine;

public class RandomColor : MonoBehaviour
{
    [SerializeField] BlockScriptableObject blockScriptableObject;
    SpriteRenderer[] myChildren;
    Sprite[] colors;
    private void Awake()
    {
        colors = blockScriptableObject.allColors;
        myChildren = gameObject.GetComponentsInChildren<SpriteRenderer>();
        int random = Random.Range(0, colors.Length);
        foreach (SpriteRenderer spriteRenderer in myChildren)
        {
            spriteRenderer.sprite = colors[random];
        }
    }
}
