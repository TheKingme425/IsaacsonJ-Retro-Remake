using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Player_Movement movement;
    public Sprite idle;
    public Sprite jump;
    public Sprite slide;
    public AnimationSprite run;

    private void Awake()
    {
        movement = GetComponentInParent<Player_Movement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false;
        run.enabled = false;
    }

    private void LateUpdate()
    {
        run.enabled = movement.running;

        if (movement.jumping)
        {
            spriteRenderer.sprite = jump;
        }
        else if (movement.sliding)
        {
            spriteRenderer.sprite = slide;
        }
        else if (!movement.running)
        {
            spriteRenderer.sprite = idle;
        }
    }
    
}
