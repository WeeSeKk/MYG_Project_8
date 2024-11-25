using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using Oculus.Platform;


public class APIManager : MonoBehaviour
{
    public List<CarSO> scriptableObjects;

    async void Start()
    {
        string urlGet = "http://localhost/MYG8/index.php?carsspecs=get";
        await GetRequestAsync(urlGet);
    }

    public async Task GetRequestAsync(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            var operation = webRequest.SendWebRequest();

            while (!operation.isDone)
            {
                await Task.Yield();
            }

            string[] pages = url.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    return; 
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    return; 
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }

            JArray jArray = JArray.Parse(webRequest.downloadHandler.text);

            foreach (JObject keys in jArray)
            {
                CarSpecs carSpecs = new CarSpecs
                {
                    modelID = (int)keys.GetValue("ID"),
                    modelManufacturer = keys.GetValue("Model_Manufacturer").ToString(),
                    modelName = (string)keys.GetValue("Model_Name"),
                    horsePower = (int)keys.GetValue("Horse_Power"),
                    modelWeight = (int)keys.GetValue("Model_Weight")
                };
                SetupSO(carSpecs);
            }
        }
    }

    void SetupSO(CarSpecs carSpecs)
    {
        CarSO carSO = ScriptableObject.CreateInstance<CarSO>();
        carSO.id = carSpecs.modelID;
        carSO.modelManufacturer = carSpecs.modelManufacturer;
        carSO.modelName = carSpecs.modelName;
        carSO.horsePower = carSpecs.horsePower;
        carSO.modelWeight = carSpecs.modelWeight;

        scriptableObjects.Add(carSO);
    }
}
public class CarSpecs
{
    public int modelID { get; set; }
    public string modelManufacturer { get; set; }
    public string modelName { get; set; }
    public int horsePower { get; set; }
    public int modelWeight { get; set; }
}