using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Player_Movement movement;
    public Sprite idle;
    public Sprite jump;
    public Sprite slide;
    public Sprite run;

    private void Awake()
    {
        movement = GetComponentInParent<Player_Movement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        if (movement.jumping)
        {
            spriteRenderer.sprite = jump;
        }
        else if (movement.sliding)
        {
            spriteRenderer.sprite = slide;
        }
        else if (movement.running)
        {
            spriteRenderer.sprite = run;
        }
        else 
        {
            spriteRenderer.sprite = idle;
        }
    }
    
}
