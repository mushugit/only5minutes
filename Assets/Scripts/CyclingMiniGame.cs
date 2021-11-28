using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclingMiniGame : MonoBehaviour
{
    public float RoadSpeed = 0.1f;
    public Transform RoadsContainer;
    public GameObject RoadTemplate;
    public int minRoadCount = 3;
    public float deltaY = 20f;
    public GameObject CarReverseTemplate;
    public GameObject CarTemplate;
    public Transform CarsContainer;

    [Range(0.0f, 100.0f)]
    public float ReverseCarProbability;
    
    [Range(0.0f, 100.0f)]
    public float NormalCarProbability;

    private Vector3 screenPosition;
    private Camera mainCamera;
    private float spriteHeight = 100f;
    private float[] ReverseCarXPosition = {-4f,-1.5f};
    private float[] NormalCarXPosition = {1.5f,4f};
    public float ReverseCarYPosition = 10f;
    private float NormalCarYPosition = 1-10f;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    private void UpdateRoads(){
        var roadCount = 0;
        var move = Time.deltaTime * RoadSpeed;
        var highestRoadY = float.MinValue;

        foreach(Transform road in RoadsContainer){
            if(road != RoadsContainer){
                //Move road down
                var newY = road.position.y - move;
                road.position = new Vector3(road.position.x, newY);

                var roadPositionOnScreen = mainCamera.WorldToScreenPoint(road.position);
                var isBelowScreen = roadPositionOnScreen.y + spriteHeight < 0f;

                if(isBelowScreen){
                    Destroy(road.gameObject);
                }
                else{
                    if(newY > highestRoadY){
                        highestRoadY = newY;
                    }
                    roadCount++;
                }
            }
        }

        while(roadCount < minRoadCount){
            var newY = highestRoadY + deltaY;
            var newRoad = Instantiate(RoadTemplate,new Vector3(0f, newY,0f),Quaternion.identity,RoadsContainer);
            SpawnCars();
            if(newY < highestRoadY){
                highestRoadY = newY;
            }
            roadCount++;
        }
    }

    private void SpawnCars(){
        var spawnReverseCar = Random.Range(0f,100f) < ReverseCarProbability;
        var spawnNormalCar = Random.Range(0f,100f) < NormalCarProbability;
        var carPosition = Random.Range(0,2);

        if(spawnReverseCar){
            SpawnACar(true,new Vector3(ReverseCarXPosition[carPosition],ReverseCarYPosition,0f),new Quaternion(0f,0f,180f,0f),CarReverseTemplate);
        }
        if(spawnNormalCar){
            SpawnACar(false, new Vector3(NormalCarXPosition[carPosition],NormalCarYPosition,0f),Quaternion.identity,CarTemplate);
        }
    }

    private GameObject SpawnACar(bool isReversed, Vector3 carPosition, Quaternion carRotation, GameObject template){
        var car = Instantiate(template,CarsContainer,true);
        car.transform.position = carPosition;
        car.transform.rotation = carRotation;
        var carScript = car.GetComponent<Car>();
        carScript.IsReversed = isReversed;
        carScript.Road = this;
        return car;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRoads();
    }
}
