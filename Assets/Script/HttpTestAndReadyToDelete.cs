using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Net;
using System.IO;

public class HttpTestAndReadyToDelete : MonoBehaviour
{
    private static HttpTestAndReadyToDelete instance;
    public static HttpTestAndReadyToDelete GetInstance()
    {
        if (instance == null)
        {
            instance = new HttpTestAndReadyToDelete();
        }
        return instance;
    }
    // Start is called before the first frame update
    //     IEnumerator Start()
    //     {
    //         Debug.Log("HttpTestGo");
    //         var url = "https://www.google.com/?hl=zh_tw";
    //         // var www = UnityWebRequest.Get(url);
    //         var www = UnityWebRequestTexture.GetTexture(url);

    //         yield return www.SendWebRequest();
    // Debug.Log(www.responseCode);
    //         if (www.isHttpError || www.isNetworkError)
    //         {
    //             Debug.Log(www.error);
    //         }
    //         else
    //         {
    //             Debug.Log(www.downloadHandler.text);
    //         }
    //         var tex = DownloadHandlerTexture.GetContent(www);
    //         Debug.Log(tex);

    //     }

    private void Start()
    {
        var request = WebRequest.Create("http://twsiyuan.com");
        using (var response = request.GetResponse())
        {
            using (var stream = response.GetResponseStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    var html = reader.ReadToEnd();
                    Debug.Log(html);
                }
            }
        }
    }

    IEnumerator DoRequest(object data)
{
   var json = JsonUtility.ToJson(data);
   var jsonRaw = System.Text.Encoding.UTF8.GetBytes(json);
   
   var uploader = new UploadHandlerRaw(jsonRaw);
   uploader.contentType = "application/json; charset=utf-8";
   
   var downloader = new DownloadHandlerBuffer();
   
   var request = new UnityWebRequest("http://example.com", UnityWebRequest.kHttpVerbPUT);
   request.uploadHandler = uploader;
   request.downloadHandler = downloader;
   request.SetRequestHeader("Accept", "application/json");
   request.SetRequestHeader("Accept-Charset", "utf-8");
   
   yield return request.Send();
   
   var rjson = System.Text.Encoding.UTF8.GetString(downloader.data);
   // TODO: Response Json Unmarshal
}

}
