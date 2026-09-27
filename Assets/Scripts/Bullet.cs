
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 10f;
    public float lifeTime = 3f;
    public Vector3 targetVector;

    private float timer;
    
    private void OnEnable()
    {
        timer = lifeTime; // Reiniciamos el temporizador
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * targetVector * Time.deltaTime, Space.World);
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Meteor")
        {
            Player.IncreaseScore();

            Meteor meteor = collision.gameObject.GetComponent<Meteor>();

            meteor.Split(targetVector);
            
            gameObject.SetActive(false);
        }
    }

}
