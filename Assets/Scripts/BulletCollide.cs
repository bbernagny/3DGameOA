using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollide : MonoBehaviour
{
    private int health = 3;
    [SerializeField] GameObject[] _healthUI;
    


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            health--;
            _healthUI[health].gameObject.SetActive(false);
        }
    }
}
