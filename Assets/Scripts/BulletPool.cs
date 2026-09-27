using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    
    public GameObject bulletPrefab;
    public int poolSize = 5;
    private List<GameObject> bulletList;

    //singleton
    private static BulletPool instance;
    public static BulletPool Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        bulletList = new List<GameObject>();
        AddBulletsToPool(poolSize);
    }

    private void AddBulletsToPool(int size)
    {
        for (int i = 0; i < size; i++)
        {   
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletList.Add(bullet);
            bullet.transform.parent = transform;
        }
    }

    public GameObject GetBullet()
    {
        int i;
        for (i = 0; i < bulletList.Count; i++)
        {
            if (!bulletList[i].activeSelf)
            {
                bulletList[i].SetActive(true);
                return bulletList[i];
            }
        }
        AddBulletsToPool(1);
        bulletList[bulletList.Count - 1].SetActive(true);
        return bulletList[bulletList.Count - 1];
    }

}
