using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetFuel : MonoBehaviour
{

    private RectTransform ThisTransform = null;
    private RobotBoyController player;
    private float FuelChange;
	private float BarMaxWidth;
	private float BarHeight;

    void Awake()
    {
        ThisTransform = GetComponent<RectTransform>();
        player = GameObject.Find("ROBOT-BOY").GetComponent<RobotBoyController>();
		BarMaxWidth = ThisTransform.sizeDelta.x;
		BarHeight = ThisTransform.sizeDelta.y;
    }


    // Setting Size
    void Start()
    {
        ThisTransform.sizeDelta = new Vector2(Mathf.Clamp(player.FuelLevel, 0, BarMaxWidth), BarHeight);
    }

    // Update is called once per frame
    void Update()
    {
        FuelChange = 0f;

        FuelChange = Mathf.MoveTowards(ThisTransform.rect.width, player.FuelLevel, 25f);


        ThisTransform.sizeDelta = new Vector2(Mathf.Clamp(FuelChange, 0, BarMaxWidth), BarHeight);
    }
}
