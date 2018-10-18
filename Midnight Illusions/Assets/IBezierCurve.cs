using UnityEngine;

public interface IBezierCurve
{
    Vector3 GetPoint(float t);

    Vector3 GetVelocity(float t);

    Vector3 GetDirection(float t);
}