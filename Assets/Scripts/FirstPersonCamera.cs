
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public Transform Target;
    public Vector3 offset;

    void LateUpdate()
    {
        if (Target == null)
        {
            return;
        }

        offset = new Vector3(0, 1.4f, -4.5f);

        transform.position = Target.position + offset;


    }
}
