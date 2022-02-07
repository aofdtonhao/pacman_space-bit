using UnityEngine;

namespace Tonhex
{

    [RequireComponent(typeof(Collider2D))]
    public class Item : MonoBehaviour
    {

        void Awake()
        {
            Debug.Log("Item::Awake()");
        }

        void Start()
        {
            Debug.Log("Item::Start()");
        }

    }

}
