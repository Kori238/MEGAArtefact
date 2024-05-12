using MyMathLibrary;
using UnityEngine;

public class Target : MonoBehaviour
{
    private BoundingObject thisCollider;
    private TargetSpawner spawner;

    private void Start()
    {
        spawner = GameObject.FindObjectOfType<TargetSpawner>();
        thisCollider = GetComponent<BoundingObject>();
        thisCollider.collisionEvent += OnBoundingEnter;
    }

    private void OnBoundingEnter(BoundingObject other, MyVector3 collisionNormal, float penetrationDepth)
    {
        // Check if the colliding object is a bullet
        if (other.gameObject.CompareTag("Bullet"))
        {
            spawner.OnTargetDestroyed();
            Destroy(gameObject); // Destroy the target
        }
    }
}