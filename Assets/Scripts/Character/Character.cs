using UnityEngine;

namespace Tonhex
{

    [RequireComponent(typeof(Movement))]
    public class Character : MonoBehaviour
    {

        public Movement movement { get; private set; }

        void Awake()
        {
            movement = GetComponent<Movement>();
        }

    }

}
