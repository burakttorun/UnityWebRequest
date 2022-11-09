using System;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class HttpClient
{
    private readonly ISerializationOption _serializationOption;

    public HttpClient(ISerializationOption serializationOption)
    {
        _serializationOption = serializationOption;
    }

    public async Task<T> Get<T>(string url)
    {
        try
        {
            using var www = UnityWebRequest.Get(url);

            www.SetRequestHeader("Content-Type", _serializationOption.ContentType);

            var operation = www.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            if (www.result != UnityWebRequest.Result.Success)
                Debug.LogError($"Failed: {www.error}");


            var result = _serializationOption.Deserialize<T>(www.downloadHandler.text);
            return result;
        }
        catch (Exception ex)
        {
            Debug.LogError($"{nameof(Get)} failed: {ex.Message}");
            return default;
        }
    }

    public async void Post<T>(string url,T type)
    {
        try
        {
            byte[] jsonToSend = Encoding.UTF8.GetBytes(_serializationOption.Serialize(type));
            using var www = UnityWebRequest.Post(url,_serializationOption.Serialize(type));
            
            www.SetRequestHeader("Content-Type",_serializationOption.ContentType);
            UploadHandlerRaw uploader = new UploadHandlerRaw(jsonToSend);
            uploader.contentType = _serializationOption.ContentType;
            www.uploadHandler = uploader;
            var operation = www.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();
            
            if (www.result != UnityWebRequest.Result.Success)
                Debug.LogError($"Failed: {www.error}");
            else
            {
                Debug.Log($"Message: {www.result}");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"{nameof(Post)} failed: {ex.Message}");
        }
    }
}