using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defense : MonoBehaviour
{
    public Transform target;
    public Transform head;
    
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<Pathfinder>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        head.LookAt(target);
    }
}
