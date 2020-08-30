using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{

    public GameObject _sphere;
    public GameObject _cube;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Instantiate(_cube);
    
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
          
            Instantiate(_sphere);
        }
    }
}
