using MyMathLibrary;
using UnityEngine;
using TMPro;

public class TargetSpawner : MonoBehaviour
{
    public GameObject[] targetPrefabs;
    public MyGameObject orbitPoint;
    public float spawnInterval = 5f;
    public float minScale = 0.5f;
    public float maxScale = 2f;
    public float minTranslationSpeed = 1f;
    public float maxTranslationSpeed = 5f;
    public float minRotationSpeed = 1f;
    public float maxRotationSpeed = 5f;
    public MyVector3 minPos = new MyVector3(-5, -5, -5);
    public MyVector3 maxPos = new MyVector3(5, 5, 5);

    private float nextSpawnTime = 0f;
    private int targetCount = 0;
    private bool isSpawning = false;
    public GameObject startGamePrefab;
    public TMP_Text timeLeftText;
    public TMP_Text targetsInSceneText;
    public TMP_Text targetsDestroyedText;
    public TMP_Text startGameText;
    private int targetsDestroyed = 0;
    private float gameTime = 30f;


    private void Start()
    {
        Instantiate(startGamePrefab);
        timeLeftText.text = "Time Left: " + gameTime.ToString("F2");
        targetsInSceneText.text = "Targets Alive: " + targetCount.ToString();
        targetsDestroyedText.text = "Targets Destroyed: " + targetsDestroyed.ToString();
        HideGameUI();
    }

    private void Update()
    {
        if (isSpawning)
        {
            if (targetCount < 2)
            {
                SpawnTarget();
            }
            else if (targetCount < 5 && Time.time > nextSpawnTime)
            {
                SpawnTarget();
                nextSpawnTime = Time.time + spawnInterval;
            }
            gameTime -= Time.deltaTime;
            timeLeftText.text = "Time Left: " + gameTime.ToString("F2");
        }
    }

    private void SpawnTarget()
    {
        // Get the current rotation of the orbit point
        MyQuaternion orbitRotation = orbitPoint.GetComponent<MyGameObject>().myTransform.rotation;

        // Generate a random distance from the center of the orbit
        float distance = Random.Range(minPos.x, maxPos.x);

        // Generate a random angle offset
        float angleOffset = Random.Range(-Mathf.PI / 2f, Mathf.PI / 2f);

        // Calculate the position of the target based on the rotation and distance
        MyVector3 spawnPosition = MyVector3.Multiply(orbitRotation * MyVector3.right, distance);

        // Apply the angle offset to the spawn position
        spawnPosition = MyVector3.Multiply(new MyQuaternion(angleOffset, MyVector3.up) * spawnPosition, 1f);

        // Instantiate the target
        GameObject targetPrefab = targetPrefabs[Random.Range(0, targetPrefabs.Length)];
        GameObject targetObject = Instantiate(targetPrefab);
        MyGameObject myGameObject = targetObject.GetComponent<MyGameObject>();
        myGameObject.myTransform.parent = orbitPoint;
        myGameObject.myTransform.position = spawnPosition;
        
        MovingTarget movingTarget = targetObject.GetComponent<MovingTarget>();

        // Randomize the movement constraints
        movingTarget.scalingSpeed = Random.Range(minScale, maxScale);
        movingTarget.translationSpeed = Random.Range(minTranslationSpeed, maxTranslationSpeed);
        movingTarget.rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
        targetCount++;
        targetsInSceneText.text = "Targets Alive: " + targetCount.ToString();
    }

    public void StartGame()
    {
        isSpawning = true;
        gameTime = 30f;
        ShowGameUI();
        Invoke("StopGame", 30f);
        ResetGame();
    }

    private void StopGame()
    {
        HideGameUI();
        isSpawning = false;
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject target in targets)
        {
            Destroy(target);
        }
        Instantiate(startGamePrefab);
        DisplayScore();
    }

    public void OnTargetDestroyed()
    {
        targetCount--;
        targetsDestroyed++;
        targetsInSceneText.text = "Targets Alive: " + targetCount.ToString();
        targetsDestroyedText.text = "Targets Destroyed: " + targetsDestroyed.ToString();
    }

    private void HideGameUI()
    {
        timeLeftText.gameObject.SetActive(false);
        targetsInSceneText.gameObject.SetActive(false);
        targetsDestroyedText.gameObject.SetActive(false);
        startGameText.gameObject.SetActive(true);
    }

    private void ShowGameUI()
    {
        timeLeftText.gameObject.SetActive(true);
        targetsInSceneText.gameObject.SetActive(true);
        targetsDestroyedText.gameObject.SetActive(true);
        startGameText.gameObject.SetActive(false);
    }

    private void ResetGame()
    {
        targetsDestroyed = 0;
        gameTime = 30f;
        targetCount = 0;
        timeLeftText.text = "Time Left: " + gameTime.ToString("F2");
        targetsInSceneText.text = "Targets Alive: " + targetCount.ToString();
        targetsDestroyedText.text = "Targets Destroyed: " + targetsDestroyed.ToString();
    }

    private void DisplayScore()
    {
        startGameText.text = "Shoot the green target to replay\nScore: " + targetsDestroyed.ToString();
    }
}