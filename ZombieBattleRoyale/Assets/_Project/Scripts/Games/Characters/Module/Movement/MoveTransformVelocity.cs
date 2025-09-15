/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTransformVelocity : MonoBehaviour, IMoveVelocity {

    [SerializeField] private float moveSpeed;
    private Vector3 velocityVector;
 

    public void SetSpeed(float speed)
    {
        this.moveSpeed = speed;
    }

    public void SetVelocity(Vector3 velocityVector) {
        this.velocityVector = velocityVector;
    }

    public void SetUseAcceleration(bool useAcceleration)
    {
        // Do nothing
    }

    public Vector3 GetVelocity()
    {
        return velocityVector * moveSpeed;
    }

    private void Update() {
        transform.position += velocityVector * moveSpeed * Time.deltaTime;
    }

    public void Disable() {
        this.enabled = false;
    }

    public void Enable() {
        this.enabled = true;
    }

}
