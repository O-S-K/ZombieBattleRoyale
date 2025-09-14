using Sirenix.OdinInspector;
using UnityEngine;

public class MoveVelocity : MonoBehaviour, IMoveVelocity
{
    [ReadOnly, SerializeField] private float moveSpeed;
    [SerializeField] private float acceleration = 10f;

    private Vector3 currentVelocity;
    private Vector3 velocityVector;
    private Rigidbody rigidbody;
    private bool useAcceleration = true;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void SetSpeed(float speed)
    {
        this.moveSpeed = speed;
    }

    public void SetVelocity(Vector3 velocityVector)
    {
        this.velocityVector = velocityVector;
    }

    public void SetUseAcceleration(bool useAcceleration)
    {
        this.useAcceleration = useAcceleration;
    }

    public Vector3 GetVelocity()
    {
        return currentVelocity;
    }


    public void Disable()
    {
        this.enabled = false;
        rigidbody.velocity = Vector3.zero;
    }

    public void Enable()
    {
        this.enabled = true;
    }

    private void FixedUpdate()
    {
        Vector3 targetVelocity = velocityVector.normalized * moveSpeed;
        if (!useAcceleration)
        {
            rigidbody.velocity = targetVelocity;
        }
        else
        {
            currentVelocity = Vector3.MoveTowards(
                currentVelocity,
                targetVelocity,
                acceleration * Time.fixedDeltaTime
            );
            rigidbody.velocity = currentVelocity;
        }
    }
}