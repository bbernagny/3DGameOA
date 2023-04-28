using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] private float speed;
    public static Coins Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
