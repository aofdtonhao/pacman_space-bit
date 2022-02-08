using UnityEngine;

namespace Tonhex
{

    [RequireComponent(typeof(Collider2D))]
    public class Passage : MonoBehaviour
    {

        public Transform connection;

        void OnTriggerEnter2D(Collider2D other)
        {
            Vector3 position = connection.position;
            position.z = other.transform.position.z;
            other.transform.position = position;
        }

    }

}
