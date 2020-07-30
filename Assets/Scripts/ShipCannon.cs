﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShipCannon : MonoBehaviour
{
    public bool playerDied = false;
    private bool isHeated = false;
    [SerializeField] private float cannonCoolDown = 10f;
    private float cannonHeat = 0f;


    private ParticleSystem cannonPS = null;
    [SerializeField] private Slider HeatSlider = null;
    [SerializeField] private Text CannonSatutsText = null;

    // Start is called before the first frame update
    void Start()
    {
        cannonPS = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !playerDied)
        {
            ShootRound();
        }

        CannonTemperatureMangement();
        CannonStatusTextManagemnt();
        SliderManagement();
    }

    private void ShootRound()
    {
        if (!isHeated && cannonHeat < 99f)
        {
            cannonHeat = 100.0f;
            isHeated = true;
            cannonPS.Play();
        }
    }

    private void CannonTemperatureMangement()
    {
        if(isHeated && cannonHeat >= 1.0f)
        {
            cannonHeat -= cannonCoolDown * Time.deltaTime;
        }
        else if (cannonHeat <= 1.0f)
        {
            cannonHeat = 0.0f;
            isHeated = false;
        }
    }

    private void SliderManagement()
    {
        HeatSlider.value = cannonHeat/100.0f;
    }

    private void CannonStatusTextManagemnt()
    {
        if(!isHeated)
        {
            CannonSatutsText.text = "COOLED";
            CannonSatutsText.color = new Color(0, 218, 255);
        }
        else
        {
            CannonSatutsText.text = "HEATED";
            CannonSatutsText.color = new Color(255, 0 , 0 );
        }
    }
}