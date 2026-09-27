
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float thrustForce = 5f;
    public float rotationSpeed = 120f;
    public float recoilForce = 5f;

    public GameObject gun;

    private Rigidbody _rigid;

    public static int SCORE = 0;
    public static int initialHP = 4;
    private static int HP = initialHP;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        SCORE = 0;
        GameObject go = GameObject.FindGameObjectWithTag("UI_HP");
        go.GetComponent<Text>().text = "HP: " + Player.initialHP;
        
        go = GameObject.FindGameObjectWithTag("UI_SCORE");
        go.GetComponent<Text>().text = "SCORE: " + Player.SCORE;

        _rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        float thrust = 0f;
        float rotation = 0f;
        

        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
                thrust = 1f * Time.deltaTime;
            else if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
                thrust = -1f * Time.deltaTime;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
                rotation = -1f * Time.deltaTime;
            else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
                rotation = 1f * Time.deltaTime;
        }

        Vector3 thrustDirection = transform.right;
        _rigid.AddForce(thrustDirection * thrust * thrustForce);

        transform.Rotate(Vector3.forward, rotation * rotationSpeed);

        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            GameObject bullet = BulletPool.Instance.GetBullet();
            bullet.transform.position = gun.transform.position;

            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.targetVector = thrustDirection;

            _rigid.AddForce(thrustDirection * -1f * recoilForce);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Meteor")
        {
            DecreaseHP();
            if (SCORE != 0) SCORE--;
            if (HP == 0) Restart();
            Destroy(collision.gameObject);
        }
    }

    private static void DecreaseHP ()
    {
        HP--;
        UpdateHPText();
    }

    private static void UpdateHPText ()
    {
        GameObject go = GameObject.FindGameObjectWithTag("UI_HP");
        go.GetComponent<Text>().text = "HP: " + Player.HP;
    }

    public static void IncreaseScore ()
    {
        SCORE++;
        UpdateScoreText();
    }

    private static void UpdateScoreText ()
    {
        GameObject go = GameObject.FindGameObjectWithTag("UI_SCORE");
        go.GetComponent<Text>().text = "SCORE: " + Player.SCORE;
    }

    public static void Restart ()
    {
        SCORE = 0;
        HP = initialHP;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
