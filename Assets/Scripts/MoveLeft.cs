using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    //--------------------------------
    public float Speed = 10f;
    private Transform ThisTransform = null;
    //--------------------------------
    // Use this for initialization
    void Awake()
    {
        ThisTransform = GetComponent<Transform>();
    }
    //--------------------------------
    // Update is called once per frame
    void Update()
    {
        //Update object position
        ThisTransform.position -= ThisTransform.right * Speed * Time.deltaTime;
    }
    //--------------------------------
}
