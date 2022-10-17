using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class TestController : MonoBehaviour
{
    [ContextMenu("Get Test")]
    public async void GetTest()
    {
        var url = "https://jsonplaceholder.typicode.com/todos/1";
        
        HttpClient httpClient = new HttpClient(new JsonSerializationOption());
        var result = await httpClient.Get<User>(url);
    }
}