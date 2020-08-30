using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntController : MonoBehaviour
{
    [Header("Не менять")]
    public int num;
    private void Start()
    {
        num = -1;
    }
    public void SetValue(int i)
    {
        num = i;
    }
}

