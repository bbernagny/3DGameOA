using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpawnBullet : MonoBehaviour
{
    public GameObject bullet;
    [SerializeField] Transform _npcAgent;
    List<GameObject> bulletList;

    // Start is called before the first frame update
    void Start()
    {
        bulletList = new List<GameObject>();
        for (int i = 0; i < 10; i++)
        {
            GameObject objBullet = (GameObject)Instantiate(bullet);
            objBullet.SetActive(false);
            bulletList.Add(objBullet);
        }
    }

   public void Fire()
    {
        for (int i = 0; i < bulletList.Count; i++)
        {
            if (!bulletList[i].activeInHierarchy)
            {
                bulletList[i].transform.position = transform.position;
                bulletList[i].transform.rotation = transform.rotation;
                bulletList[i].SetActive(true);

                Rigidbody tempRigidbodyBullet = bulletList[i].GetComponent<Rigidbody>();
                tempRigidbodyBullet.AddForce(transform.forward * 25f, ForceMode.Impulse);
                tempRigidbodyBullet.AddForce(transform.up * 7f, ForceMode.Impulse);
                break;
            }
        }
    }
}
