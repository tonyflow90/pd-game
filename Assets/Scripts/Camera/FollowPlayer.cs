using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform target;

    public float speed = .125f;

    void LateUpdate()
    {
        Debug.Log(target.position);
        transform.position = target.position;
    }
}
