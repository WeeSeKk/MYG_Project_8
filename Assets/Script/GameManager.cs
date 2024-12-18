using System.Collections.Generic;
using UnityEngine;

namespace GameManagerNamespace
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public List<CarSO> scriptableObjects;
        [SerializeField] List<GameObject> previewCarList;
        [SerializeField] List<Material> carMaterials;
        [SerializeField] GameObject carSpawnPoint;
        bool spawnedCar;

        void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this);
            }
            else
            {
                instance = this;
            }
            DontDestroyOnLoad(this.gameObject);
        }

        public void OnCarColorChange(string carName, Color newColor)
        {
            Debug.Log("Color Change to : " + newColor);
            for (int i = 0; i < previewCarList.Count; i++)
            {
                if (previewCarList[i].name == carName)
                {
                    carMaterials[i].color = newColor;
                    break;
                }
            }
        }

        public void OnCarSelected(string carName)
        {
            for (int i = 0; i < previewCarList.Count - 1; i++)
            {
                if (previewCarList[i].name == carName)
                {
                    if (spawnedCar == true)
                    {
                        Destroy(carSpawnPoint.transform.GetChild(0).gameObject);
                    }
                    GameObject newCar = Instantiate(previewCarList[i], carSpawnPoint.transform.position, Quaternion.identity);
                    newCar.transform.SetParent(carSpawnPoint.transform);
                    spawnedCar = true;
                }
            }
        }

        public void ResetCarRotation()
        {
            foreach (GameObject car in previewCarList)
            {
                car.transform.rotation = new Quaternion(0, 0, 0, 0);
            }
        }

        public GameObject SelectedCar(string carName)
        {
            for (int i = 0; i < previewCarList.Count - 1; i++)
            {
                if (carName == previewCarList[i].name)
                {
                    return previewCarList[i];
                }
            }
            return previewCarList[0];//not supposed to happen 
        }

        public CarSpecs GetCarSpecs(string name)
        {
            for (int i = 0; i < scriptableObjects.Count - 1; i++)
            {
                if (scriptableObjects[i].modelName == name)
                {
                    CarSpecs carSpecs = new CarSpecs
                    {
                        modelID = scriptableObjects[i].id,
                        modelManufacturer = scriptableObjects[i].modelManufacturer,
                        modelName = scriptableObjects[i].modelName,
                        horsePower = scriptableObjects[i].horsePower,
                        modelWeight = (int)scriptableObjects[i].modelWeight
                    };
                    return carSpecs;
                }
            }
            CarSpecs carSpecs1 = new CarSpecs
            {
                modelID = scriptableObjects[0].id,
                modelManufacturer = scriptableObjects[0].modelManufacturer,
                modelName = scriptableObjects[0].modelName,
                horsePower = scriptableObjects[0].horsePower,
                modelWeight = (int)scriptableObjects[0].modelWeight
            };
            return carSpecs1;//not supposed to happen
        }
    }
}