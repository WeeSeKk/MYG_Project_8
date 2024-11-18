using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] List<GameObject> previewCarList;
    [SerializeField] List<Material> carMaterials;
    
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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCarColorChange(string carName, Color newColor)
    {
        Debug.Log("Color Change");
        for (int i = 0; i < previewCarList.Count - 1; i ++)
        {
            if (previewCarList[i].name == carName)
            {
                carMaterials[i].color = newColor;
                break;
            }
        }
    }
}
