using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveVelocity {

    public void SetSpeed(float speed);
    void SetVelocity(Vector3 velocityVector);
    void SetUseAcceleration(bool useAcceleration);
    Vector3 GetVelocity();
    void Disable();
    void Enable();

}
