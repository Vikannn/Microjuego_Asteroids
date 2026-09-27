using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public GameObject meteorPrefab;
    public float lifeTime = 4f;
    public float spawnRatePerMin = 30;
    public float spawnRatePerIncrement = 1;
    public float minSize;
    public float maxSize;
    public float offsetY = 1f;
    public float offsetX = 0.1f;
    private float spawnNext = 0f;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;   
    }
    // Update is called once per frame
    void Update()
    {
        
        if (Time.time > spawnNext)
        {
            spawnNext = Time.time + (60 / spawnRatePerMin);
            spawnRatePerMin += spawnRatePerIncrement;

            float randX = Random.Range(0 + offsetX, 1 - offsetX);
            Vector3 spawnPos = mainCamera.ViewportToWorldPoint(new Vector3(randX, 1f, 1f));
            spawnPos.y += offsetY;
            spawnPos.z = 0f;

            float size = Random.Range(minSize, maxSize);
            GameObject meteor = Instantiate(meteorPrefab, spawnPos, Quaternion.identity);
            meteor.transform.localScale = new Vector2(size, size);

            Destroy(meteor, lifeTime);
        }

    }
}

