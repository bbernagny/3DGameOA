using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;


namespace CoinRand
{
    public class SpawnCoin : MonoBehaviour
    {
        public List<Transform> spawnPoints = new List<Transform>();
        [SerializeField] private GameObject coinPrefab;

        public HashSet<int> randomValues = new HashSet<int>(); //Elimizdeki spawnpointlerin sayısını tutmak için.HashSeti doldurana kadar içerisine rastgele sayı girmemiz lazım.Çok büyük bir seçim olsaydı bu yöntem mantıklı olmazdı.
        public Random r = new Random();


        private void Start()
        {
            int a = (int)Math.Ceiling(spawnPoints.Count / 2.0f);

            while (randomValues.Count < a)
            {
                randomValues.Add(r.Next(0, spawnPoints.Count() - 1));
            }


            //Alttaki yöntemi şimdilik commentle. Bir nokta da bir coin üretme için farklı yöntem HashSet kullanımı <
            //var randomValues = Enumerable.Range(0, a)
            //    .Select(e => spawnPoints[r.Next(spawnPoints.Count)]);
            //Debug.Log(randomValues.Count() + " " + a);
            //Spawn noktaları belirlendi. Coinin spawn edilmesi için;

            foreach (var x in randomValues)
            {
                //Debug.Log(spawnPoints[x]);
                Instantiate(coinPrefab, spawnPoints[x]); //Her bir randomvalue için transform noktasında coin instantiate edecek.
            }
        }
    }
}

