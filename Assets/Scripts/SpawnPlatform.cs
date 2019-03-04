using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPlatform : MonoBehaviour
{

    public GameObject platform = null;
    public GameObject GameTimer = null;
    public Text LevelText;
    private int level = 0;
    private TimeSpan ThisTimeSpan;

    public float SpawnInterval = 0.5f;
    public float SpawnIncrementer = 0.2f;
    public int SecondsToIncrement = 5;
    private int IncrementSTI;
    
    private float MaxY = 0f;
    private float TopY = 30f;    
    private float MinY = -0f;
    private float BottomY = -4.0f;
    public float YIncrementer = 0.25f;
    private Transform ThisTransform = null;

    //--------------
    //Set the origin for the platforms
    void Awake()
    {
        ThisTransform = GetComponent<Transform>();
        if(LevelText != null)
        {
            LevelText.text = "Level: " + level;
        }
        
        //IncrementSTI will increment SecondsToIncrement each time it is called
        IncrementSTI = SecondsToIncrement;
        //Adjust MaxY and MinY to this transform
        MaxY = ThisTransform.position.y + MaxY;
        MinY = ThisTransform.position.y + MinY;
    }
    // Use this for initialization
    void Start()
    {
        InvokeRepeating("MakePlatform", 1.5f, SpawnInterval);
    }
    void Update()
    {
        ThisTimeSpan = GameTimer.GetComponent<GameTimerScript>().TimeElapsed;

        if ((int)ThisTimeSpan.TotalSeconds >= SecondsToIncrement && (int)ThisTimeSpan.TotalSeconds % SecondsToIncrement == 0)
        {            
            SpawnInterval += SpawnIncrementer;
            MaxY = (MaxY < TopY)? MaxY + YIncrementer: TopY;
            MinY = (MinY > BottomY)? MinY - YIncrementer: BottomY;
            CancelInvoke();
            Start();
            SecondsToIncrement += IncrementSTI;
            level++;
            LevelText.text = "Level: " + level;
        }
    }

    void MakePlatform()
    {
        Instantiate(platform, GetRandomPosition(MinY, MaxY), Quaternion.identity);
    }

    private Vector3 GetRandomPosition(float min, float max)
    {
        Vector3 position = new Vector3(ThisTransform.position.x, UnityEngine.Random.Range(min, max), ThisTransform.position.z);
        return position;
    }
}
