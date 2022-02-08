using System.Collections.Generic;
using UnityEngine;

namespace Tonhex
{

    [RequireComponent(typeof(Collider2D))]
    public class Node : MonoBehaviour
    {

        public static readonly IReadOnlyList<Vector2> DIRECTIONS_TO_CHECK = new[] {
            Vector2.up,
            Vector2.down,
            Vector2.left,
            Vector2.right
        };

        public List<Vector2> availableDirections { get; private set; }

        public new Collider2D collider { get; private set; }

        void Awake()
        {
            collider = GetComponent<Collider2D>();
        }

        void Start()
        {
            availableDirections = new List<Vector2>();

            foreach (Vector2 directionToCheck in DIRECTIONS_TO_CHECK) {
                CheckAvailableDirection(directionToCheck);
            }
        }

        private void CheckAvailableDirection(Vector2 direction)
        {
            RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * collider.bounds.extents, 0f, direction, 1f, GameManager.Instance.mazeLayer);

            if (hit.collider == null) {
                availableDirections.Add(direction);
            }
        }

    }

}
