using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShipCannon : MonoBehaviour
{
    private bool isHeated = false;
    [SerializeField] private float cannonCoolDown = 10f;
    private float cannonHeat = 0f;


    private AudioSource cannonSound = null;
    [SerializeField] private ParticleSystem cannonPS = null;
    [SerializeField] private Slider HeatSlider = null;
    [SerializeField] private Text CannonSatutsText = null;

    // Start is called before the first frame update
    void Start()
    {
        cannonSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        

        CannonTemperatureMangement();
        CannonStatusTextManagemnt();
        SliderManagement();
    }

    public void ShootRound()
    {
        if (!isHeated && cannonHeat < 99f && !ShipControls.hasCrashed)
        {
            cannonSound.Play();
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
