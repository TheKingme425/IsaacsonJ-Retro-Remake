using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerSpriteRenderer smallRenderer;
    public PlayerSpriteRenderer bigRenderer;

    public DeathAnimation deathAnimation { get; private set; }

    public bool big => bigRenderer.enabled;
    public bool dead => deathAnimation.enabled;
    public bool starpower { get; private set; }

     private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        movement = GetComponent<PlayerMovement>();
        deathAnimation = GetComponent<DeathAnimation>();
        activeRenderer = smallRenderer;
    }

    public void Hit()
    {
            if (!dead) //&& !starpower)
           {
               if (big) 
               {
                    Shrink();
               }
                else 
                {
                    Death();
                }  
            }
    }

    public void Shrink()
    {
       // smallRenderer.enabled = true;
        //bigRenderer.enabled = false;
        //activeRenderer = smallRenderer;

        //capsuleCollider.size = new Vector2(1f, 1f);
        //capsuleCollider.offset = new Vector2(0f, 0f);

        //StartCoroutine(ScaleAnimation());
    }

    
    public void Death()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        deathAnimation.enabled = true;

        GameManager.Instance.ResetLevel(3f);
    }
}