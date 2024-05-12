using MyMathLibrary;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    private TargetSpawner spawner;

    private void Start()
    {
        spawner = GameObject.FindObjectOfType<TargetSpawner>();
        GetComponent<BoundingObject>().collisionEvent += OnBoundingEnter;
    }

    private void OnBoundingEnter(BoundingObject other, MyVector3 _, float __)
    {
        if (other.gameObject.CompareTag("Bullet")) {
            Destroy(gameObject);
            spawner.StartGame();
        }
    }
}