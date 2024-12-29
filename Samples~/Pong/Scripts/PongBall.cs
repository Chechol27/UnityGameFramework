using UnityEngine;
using Random = UnityEngine.Random;

namespace UnityGameFramework.Samples.Pong
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(TrailRenderer))]
    public class PongBall : MonoBehaviour
    {
        [SerializeField] private float maxSpeed;
        public void StartPush(Vector3 direction)
        {
            GetComponent<Rigidbody>().AddForce(direction, ForceMode.Impulse);
        }

        public void Restart()
        {
            transform.position = Vector3.zero;
            GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            GetComponent<TrailRenderer>().Clear();
        }

        private void OnCollisionEnter(Collision other)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.maxLinearVelocity = maxSpeed;
            Quaternion velocityRot = Quaternion.AngleAxis(Random.Range(-10, 10), Vector3.up);
            rb.AddForce(velocityRot * rb.linearVelocity, ForceMode.Impulse); //Rotate trayectory a bit to avoid deadlocks
        }
    }
}
