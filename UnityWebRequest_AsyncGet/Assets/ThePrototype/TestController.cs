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
        var postUrl = "https://postman-echo.com/post";
        
        HttpClient httpClient = new HttpClient(new JsonSerializationOption());
        var result = await httpClient.Get<User>(url);
        Debug.Log(result);
        
        User user = new User() { Id = 501,UserId = 100, Completed = true, Title = "Boss" };
        httpClient.Post(postUrl,user );
    }
}