using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin : MonoBehaviour
{
    [SerializeField] private float lerpSpeed = 10f;
    [SerializeField] private Transform player = null;
    [SerializeField] private Collider coinTrigger = null;
    [SerializeField] private Ship CoinCollector = null;

    private void Start()
    {
        

        StartCoroutine(MoveCoinToPlayer());
    }

    private IEnumerator MoveCoinToPlayer()
    {
        while (true)
        {
            Vector3.Lerp(transform.position , player.position , lerpSpeed);
            yield return new WaitForSeconds(.5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other == coinTrigger)
        {
            CoinCollector.AddToScore(1);
        }
    }
}
