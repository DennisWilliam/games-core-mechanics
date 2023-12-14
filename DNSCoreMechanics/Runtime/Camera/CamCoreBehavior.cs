using UnityEngine;

namespace DNSCoreMechanics.CameraCore
{
    public class CamCoreBehavior : MonoBehaviour
    {
        [SerializeField] Transform player;
        [SerializeField] Vector3 offset;
        [SerializeField]  float speed;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 desiredPos = player.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPos, speed * Time.deltaTime);
        }
    }
}

