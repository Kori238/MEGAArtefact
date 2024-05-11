using MyMathLibrary;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public MyRigidBody myRigidbody;
    public float speed = 10f;
    public float lifetime = 5f; // Lifetime in seconds

    private void Start()
    {
        myRigidbody = GetComponent<MyRigidBody>();
        myRigidbody.thisCollider.collisionEvent += OnBoundingEnter;
        StartCoroutine(DestroyAfterLifetime());
    }

    public void Shoot(MyVector3 direction)
    {
        myRigidbody = GetComponent<MyRigidBody>();
        myRigidbody.AddForce(MyVector3.Multiply(direction, speed));
    }

    private void OnBoundingEnter(BoundingObject other, MyVector3 collisionNormal, float penetrationDepth)
    {
        // Ignore collision with player
        if (other.gameObject != GameObject.FindWithTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyAfterLifetime()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}