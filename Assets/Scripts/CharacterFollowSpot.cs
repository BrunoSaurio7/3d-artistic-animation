using UnityEngine;

public class CharacterFollowSpot : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target;        // The character to follow
    public Vector3 offset = new Vector3(0, 5, 0); // Height above character

    void Update()
    {
        if (target != null)
        {
            // Position the light above the target
            transform.position = target.position + offset;
            
            // Point the light at the target
            transform.LookAt(target);
        }
    }
}
