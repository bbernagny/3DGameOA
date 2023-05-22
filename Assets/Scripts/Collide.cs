using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collide : MonoBehaviour
{
    private int health = 3;
    [SerializeField] GameObject[] _healthUI;
    [SerializeField] GameObject _gameOver;
    [SerializeField] GameObject _panel;
    


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            health--;
            _healthUI[health].gameObject.SetActive(false);
        }
        if(health is 0)
        {
            _gameOver.SetActive(true);
            _panel.SetActive(true);
            StartCoroutine(Fade());
        }

        if (other.gameObject.CompareTag("uı"))
        {
            other.gameObject.SetActive(true);
            Debug.Log("ui activated");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("uı"))
        {
            other.gameObject.SetActive(false);
            Debug.Log("ui deactivated");
        }
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(1.6f);
        Time.timeScale = 0; 
    }
}
