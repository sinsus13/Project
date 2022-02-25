using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject MainCam, AimCam;

    void Start()
    {
        //would be better to disable this camera in the scene 
        AimCam.SetActive(false);
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {

            if (MainCam.activeInHierarchy)
            {
                AimCam.SetActive(true);
                MainCam.SetActive(false);
            }
            
        }
        else if(Input.GetMouseButtonUp(1))
        {

            MainCam.SetActive(true);
            AimCam.SetActive(false);

        }
    }

}

