using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class TestController : MonoBehaviour
{
    [ContextMenu("Test Get")]
    public async void GetTest()
    {
        var url = "https://jsonplaceholder.typicode.com/todos/1";

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
            Debug.Log($"Success: {www.downloadHandler.text}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"{this}Could not parse response {jsonResponse}. {ex.Message}");
        }
        
    }
}