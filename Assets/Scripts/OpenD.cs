using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

public class OpenD: MonoBehaviour
{
    [SerializeField]
    GameObject poiPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(GetOpenData());
    }
    public void DownLoadData()
    {
        StartCoroutine(GetOpenData());
    }
    IEnumerator GetOpenData()
    {
        //UnityWebRequest www = UnityWebRequest.Get("https://api.bsmsa.eu/ext/api/bsm/chargepoints/locations");
        //www = UnityWebRequest.Get("https://www.bcn.cat/tercerlloc/files/NP-NASIA/opendatabcn_NP-NASIA_area-esbarjo-gossos-js.json");
        UnityWebRequest www = UnityWebRequest.Get("https://www.bcn.cat/tercerlloc/files/NP-NASIA/opendatabcn_NP-NASIA_Piscines-js.json");
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Or retrieve results as binary text
            // JObject obj = JObject.Parse(www.downloadHandler.text);
            String arrayObject = "{\"punts\":" + www.downloadHandler.text+'}';
            JObject obj = JObject.Parse(arrayObject);
            JArray chargePoints = (JArray)obj["punts"];

            int numPuntos = chargePoints.Count;

            for (int i = 0; i < numPuntos; i++)
            {
                JObject location = (JObject)chargePoints.GetItem(i)["geo_epgs_4326"];
                JObject ob = (JObject)chargePoints.GetItem(i);
                GameObject poi = Instantiate(poiPrefab);
                poi.GetComponent<PoiScript>().latitude = Convert.ToDouble(location["x"]);
                poi.GetComponent<PoiScript>().longitude = Convert.ToDouble(location["y"]);
                poi.GetComponent<PoiScript>().description = (string)ob["name"];
                UnityEngine.Debug.Log((string)ob["name"]);
                poi.GetComponent<PoiScript>().SendMessage("MapLocation");
            }
        }
    }
}

