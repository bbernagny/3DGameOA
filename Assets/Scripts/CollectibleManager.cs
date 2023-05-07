using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using AudioManagerNM;
using CoinRand;
using System.Linq;
using System;
using TMPro;

public class CollectibleManager : MonoBehaviour
{
    private SpawnCoin spawner;
    [SerializeField] private GameObject _spawner;
    [SerializeField] private TMP_Text _text;
    private int count = 0;

    private GameObject _music;
    private AudioManager _audioManager;
    private int totalSpawnPoints;
    int y;

    private ThirdPersonController thirdPersonController;

    [SerializeField] private GameObject powerUp;

    private void Start()
    {
        _music = GameObject.Find("AudioManagement");
        _audioManager = _music.GetComponent(typeof(AudioManager)) as AudioManager;

        thirdPersonController = GetComponent<ThirdPersonController>();
        spawner = _spawner.GetComponent(typeof(SpawnCoin)) as SpawnCoin;

        totalSpawnPoints = spawner.spawnPoints.Count();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("coin"))
        {
            //Destroy(other.gameObject);
            //other.gameObject.SetActive(false);
            //AudioSource.PlayClipAtPoint(_audioManager.audioClips[1], gameObject.transform.position);
            count++;
            _text.text = count.ToString();
            
            other.gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(_audioManager.audioClips[1], gameObject.transform.position);
            StartCoroutine(Spawn(other.gameObject));
        }

        if (other.gameObject.CompareTag("powerup"))
        {
            powerUp.SetActive(true);
            Destroy(other.gameObject);
            AudioSource.PlayClipAtPoint(_audioManager.audioClips[0], gameObject.transform.position);
            StartCoroutine(JumpHeightTime());
        }

    }

    IEnumerator JumpHeightTime()
    {
        thirdPersonController.JumpHeight = 10f;
        yield return new WaitForSecondsRealtime(3f);
        powerUp.SetActive(false);
        thirdPersonController.JumpHeight = 1.2f;
    }

    IEnumerator Spawn(GameObject gameObject)
    {

        int x = spawner.spawnPoints.IndexOf(gameObject.transform.parent.transform);
        
        spawner.randomValues.Remove(x);
        while (spawner.randomValues.Count < Math.Ceiling(totalSpawnPoints/2.0f))
        {
            y = spawner.r.Next(0, spawner.spawnPoints.Count() - 1);

            spawner.randomValues.Add(y);
        }
        yield return new WaitForSecondsRealtime(3f);

        gameObject.transform.position = spawner.spawnPoints[y].transform.position;
        gameObject.SetActive(true);
    }
}
