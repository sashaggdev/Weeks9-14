using UnityEngine;

public class Interact : MonoBehaviour
{
    public SpriteRenderer spriteRendererer;
    public Sprite[] sprite;
    int currentIndex = 0;


    void Start()
    {
        sprite = Resources.LoadAll<Sprite>("Food");
    }

    // Update is called once per frame
    void Update()
    {
        spriteRendererer.sprite = sprite[currentIndex];
    }

    public void ChangeSprite()
    {
        currentIndex = (currentIndex + 1);
    }
}
