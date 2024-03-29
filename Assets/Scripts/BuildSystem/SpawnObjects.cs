﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus;

public class SpawnObjects : MonoBehaviour
{
    [Header("Эмуляторы")]
    public GameObject[] _emki;

    [Header("Объекты")]
    public GameObject[] _objs;

    [Header("Материал эмулятора")]
    public Material[] _materials;

    [Header("Меню")]
    public GameObject MainMenu;

    private LineRenderer lr;
    private int dn;
    private MainMenu mainMenu;


    private void Start()
    {
        dn = -2;
        mainMenu = GetComponent<MainMenu>();
  //      dn = MainMenu.gameObject.GetComponent<ObjectInt>();
        lr = GetComponent<LineRenderer>();
        
        for (int i = 0; i < _emki.Length; i++)
        {
            _emki[i].SetActive(false);
            _emki[i].transform.GetComponent<MeshRenderer>().material = _materials[0];
        }
       
        Null();
    }
    public void Null()
    { }
    private void Update()
    {
        if (dn == -2)
        {
            lr.enabled = false;
            Start();
        }
        if (dn >= 0)
        {
            SpawnSss();
        }
    }

    public void SpawnSss()
    {
        dn = mainMenu.HitInt;
        for (int i = 0; i < _emki.Length; i++)
        {
            _emki[i].SetActive(false);
        }

        lr.enabled = true;
        lr.SetPosition(0, transform.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);
            }
            if (hit.collider.tag == "floor" && hit.collider.tag != "unit")
            {
                _emki[dn].transform.position = new Vector3(hit.point.x, (Vector3.Scale(_emki[dn].GetComponent<MeshFilter>().sharedMesh.bounds.size, _emki[dn].transform.lossyScale).y / 2), hit.point.z);
                //  _emki[dn.num].transform.GetComponent<MeshRenderer>().material = _materials[0];
                if (OVRInput.GetDown(OVRInput.Button.One)|| Input.GetKey(KeyCode.T)) //начало проектировки, инпут стима
                {
                    Instantiate(_objs[dn], _emki[dn].transform.position, _emki[dn].transform.rotation);
                }
                _emki[dn].SetActive(true);

            }
            else
            {
                //  _emki[dn.num].transform.GetComponent<MeshRenderer>().material = _materials[1];
            }
        }
        else
        {
            //  _emki[dn.num].SetActive(false);
            for (int i = 0; i < _emki.Length; i++)
            {
                _emki[i].SetActive(false);
            }

            lr.SetPosition(1, transform.forward * 5000);
        }
        if (OVRInput.GetDown(OVRInput.Button.Two)|| Input.GetKey(KeyCode.R)) //конец проектировки инпут стима
        {
            dn = -2;
        }
    }
}
