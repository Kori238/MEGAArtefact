using MyMathLibrary;
using UnityEngine;

public class Target : MonoBehaviour
{
    private BoundingObject thisCollider;

    private void Start()
    {
        thisCollider = GetComponent<BoundingObject>();
        thisCollider.collisionEvent += OnBoundingEnter;
    }

    private void OnBoundingEnter(BoundingObject other, MyVector3 collisionNormal, float penetrationDepth)
    {
        // Check if the colliding object is a bullet
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject); // Destroy the target
        }
    }
}