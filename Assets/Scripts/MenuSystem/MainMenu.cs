using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus;

public class MainMenu : MonoBehaviour
{
    [Header("Меню")]
    public GameObject MainMenus;

    [Header("Материал выбора")]
    public Material matvibor;

    public LineRenderer lr;
    private bool isActive;
    public int HitInt;
    private SpawnObjects spwobj;



    private void Start()
    {
   
       MainMenus.SetActive(false);
        spwobj = GetComponent<SpawnObjects>();
        isActive = false;
        lr.enabled = false;
    }

    private void FixedUpdate()
    {
        if (OVRInput.Get(OVRInput.Button.Three)|| Input.GetKey(KeyCode.E))
        {
            MainMenus.SetActive(true);
            isActive = true;
        }
        else
        {
         MainMenus.SetActive(false);
            isActive = false;
        }

        if (isActive == true)
        {
          
            lr.enabled = true;
            lr.SetPosition(0, transform.position);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                if (hit.collider)
                {
                    lr.SetPosition(1, hit.point);
                }
                if (hit.collider.tag == "Button")
                {

               
                    if (OVRInput.GetDown(OVRInput.Button.One)) {
                        HitInt = hit.collider.gameObject.GetComponent<ObjectInt>().id; 
                        spwobj.SpawnSss();
                        lr.enabled = false;
                      
                    
                    }
                }
                else
                {
               
                }
            }
            else
            {
                lr.SetPosition(1, transform.forward * 5000);
            }
            
        }
        else
        {
            lr.enabled = false;
        }
    }

}
