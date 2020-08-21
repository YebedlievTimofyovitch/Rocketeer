using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin : MonoBehaviour
{
    private bool hasCollidedWithPlayer = false;

    [SerializeField] private Transform childSparkle = null;
    [SerializeField] private float lerpSpeed = 10f;


    [SerializeField] private AudioSource pointCollectionSound = null;
    private GameObject player = null;
    private Ship CoinCollector = null;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        CoinCollector = FindObjectOfType<Ship>();
    }

    private void Update()
    {
        MoveCoinToPlayer();
    }

    private void CoinCollectionEvent()
    {
        pointCollectionSound.Play();
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
        

        transform.position = Vector3.Lerp(transform.position, player.transform.position, lerpSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(!hasCollidedWithPlayer)
            CoinCollectionEvent();
        
    }

    
}
