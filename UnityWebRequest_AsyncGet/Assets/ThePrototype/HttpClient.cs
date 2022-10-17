using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class HttpClient
{
    public async Task<T> Get<T>(string url)
    {
        using var www = UnityWebRequest.Get(url);

        www.SetRequestHeader("Content-Type", "application/json");

        var operation = www.SendWebRequest();

        while (!operation.isDone)
            await Task.Yield();

        var jsonResponse = www.downloadHandler.text;

        if (www.result != UnityWebRequest.Result.Success)
            Debug.LogError($"Failed: {www.error}");

        try
        {
            var result = JsonConvert.DeserializeObject<T>(jsonResponse);
            Debug.Log($"Success: {www.downloadHandler.text}");
            return result;
        }
        catch (Exception ex)
        {
            Debug.LogError($"{this}Could not parse response {jsonResponse}. {ex.Message}");
            return default;
        }
    }
}