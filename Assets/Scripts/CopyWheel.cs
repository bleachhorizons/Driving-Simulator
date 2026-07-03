using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyWheel : MonoBehaviour
{
    
    [SerializeField] private Transform wheel;
    private Transform obj;
    
    // Start is called before the first frame update
    void Start()
    {
        obj = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //obj.rotation.y = wheel.rotation.y;
    }
}
