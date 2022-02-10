using UnityEngine;

namespace Tonhex
{

    [RequireComponent(typeof(SpriteRenderer))]
    public class GhostEyes : MonoBehaviour
    {

        public Sprite up;
        public Sprite down;
        public Sprite left;
        public Sprite right;

        public SpriteRenderer spriteRenderer { get; private set; }
        public Movement movement { get; private set; }

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            movement = GetComponentInParent<Movement>();
        }

        void Update()
        {
            if (spriteRenderer.sprite != up && movement.direction == Vector2.up)
            {
                spriteRenderer.sprite = up;
            }
            else if (spriteRenderer.sprite != down && movement.direction == Vector2.down)
            {
                spriteRenderer.sprite = down;
            }
            else if (spriteRenderer.sprite != left && movement.direction == Vector2.left)
            {
                spriteRenderer.sprite = left;
            }
            else if (spriteRenderer.sprite != right && movement.direction == Vector2.right)
            {
                spriteRenderer.sprite = right;
            }
        }

    }

}
