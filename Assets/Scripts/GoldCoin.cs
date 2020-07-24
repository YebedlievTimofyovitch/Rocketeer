using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin : MonoBehaviour
{
    private bool hasCollidedWithPlayer = false;

    [SerializeField] private Transform childSparkle = null;
    [SerializeField] private float lerpSpeed = 10f;
    [SerializeField] private Transform player = null;
    [SerializeField] private Collider coinTrigger = null;
    [SerializeField] private Ship CoinCollector = null;

    private void Start()
    {
    }

    private void Update()
    {
        MoveCoinToPlayer();
    }

    private void CoinCollectionEvent()
    {
        hasCollidedWithPlayer = true;
        CoinCollector.AddToScore(1);
        childSparkle.parent = null;
        ParticleSystem childPS = childSparkle.GetComponent<ParticleSystem>();
        var cpsMain = childPS.main;
        cpsMain.loop = false;
        cpsMain.startSpeed = 25f;
        Destroy(gameObject);
    }

    private void MoveCoinToPlayer()
    {
        transform.position = Vector3.Lerp(transform.position, player.position, lerpSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other == coinTrigger && !hasCollidedWithPlayer)
        {
            CoinCollectionEvent();
        }
    }

    
}
