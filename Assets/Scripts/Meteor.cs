using UnityEngine;

public class Meteor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 10f;
    public float lifeTime = 4f;
    public GameObject meteorPrefab;
    public Vector3 targetVector;
    public float offset = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * targetVector * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Meteor")
        {
            Split(targetVector);
        }
    }
    public void Split(Vector3 objectVector)
    {   
        if (transform.localScale.x < 0.10f)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 leftDirection = new Vector3(-objectVector.y, objectVector.x, 0).normalized;
        Vector3 rightDirection = new Vector3(objectVector.y, -objectVector.x, 0).normalized;

        float currentOffset = offset * transform.localScale.x;
        Vector3 leftSpawnPos = transform.position + (leftDirection * offset);
        Vector3 rightSpawnPos = transform.position + (rightDirection * offset);

        GameObject meteor1 = Instantiate(meteorPrefab, leftSpawnPos, Quaternion.identity);
        GameObject meteor2 = Instantiate(meteorPrefab, rightSpawnPos, Quaternion.identity);

        meteor1.transform.localScale = transform.localScale / 2f;
        meteor2.transform.localScale = transform.localScale / 2f;

        meteor1.GetComponent<Meteor>().targetVector = leftDirection;
        meteor2.GetComponent<Meteor>().targetVector = rightDirection;
        Destroy(gameObject);
    }
}
