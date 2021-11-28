using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public bool IsReversed;
    private float minSpeed = 3f;
    private float maxSpeed = 6f;
    private Camera mainCamera;
    private float spriteHeight = 100f;

    public CyclingMiniGame Road;

    public Sprite alternateSprite;

    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;

        Speed = Random.Range(minSpeed,maxSpeed);
        if(IsReversed){
            Speed = (Speed+Road.RoadSpeed) * -1f;
        }
        else {
            Speed -= Road.RoadSpeed;
            if(Speed < 0f){
                //Slow car, put it in the front
                var renderer = this.GetComponent<SpriteRenderer>();
                renderer.sprite = alternateSprite;
                var myPosition = this.transform.position;
                myPosition.y = Road.ReverseCarYPosition;
                this.transform.position = myPosition;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        var myPosition = this.transform.position;
        myPosition.y += Time.deltaTime * Speed;
        this.transform.position = myPosition;

        var carPositionOnScreen = mainCamera.WorldToScreenPoint(myPosition);
        var isOutOffScreen = Speed < 0 ? carPositionOnScreen.y + spriteHeight < 0f : carPositionOnScreen.y - spriteHeight > Screen.height;

        if(isOutOffScreen){
            Destroy(this.gameObject);
        }
    }
}
