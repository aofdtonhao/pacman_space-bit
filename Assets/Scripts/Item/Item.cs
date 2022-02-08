using UnityEngine;

namespace Tonhex
{

    [RequireComponent(typeof(Collider2D))]
    public class Item : MonoBehaviour, IScorable
    {

        public int scorePoints { get; set; }

        void Awake()
        {
            Debug.Log("Item::Awake() - TODO");
        }

        void Start()
        {
            Debug.Log("Item::Start() - TODO");
        }

        public void Scored()
        {
            Debug.Log("Item::Scored() - TODO");
        }

    }

}
