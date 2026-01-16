using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public int s;
    private void Start()
    {
        Destroy(this.gameObject,s);
    }
}
