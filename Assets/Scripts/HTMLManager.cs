using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;
using UnityEngine;

public class HTMLManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }
    private NetworkService _network;

    private string _webPage;

    public void Startup(NetworkService service)
    {
        Debug.Log("HTML manager starting...");

        _network = service;
        //StartCoroutine(_network.GetWeatherXML(OnXMLDataLoaded));
        // StartCoroutine(_network.GetWeatherJSON(OnJSONDataLoaded));

        status = ManagerStatus.Started;
    }

    public void OnXMLDataLoaded(string data)
    {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(data);
        XmlNode root = doc.DocumentElement;

        // look for <clouds value="40" name="scattered clouds"/>
        XmlNode node = root.SelectSingleNode("clouds");
        string value = node.Attributes["value"].Value;
        //cloudValue = Convert.ToInt32(value) / 100f;
        //Debug.Log("Value: " + cloudValue);

        Messenger.Broadcast(GameEvent.WEATHER_UPDATED);

        status = ManagerStatus.Started;
    }

    public void OnJSONDataLoaded(string data)
    {
        Dictionary<string, object> dict;
        //dict = Json.Deserialize(data) as Dictionary<string, object>;

        // look for "clouds":{"all":40}
        //Dictionary<string, object> clouds = (Dictionary<string, object>)dict["clouds"];
        //cloudValue = (long)clouds["all"] / 100f;
        //Debug.Log("Value: " + cloudValue);

        Messenger.Broadcast(GameEvent.WEATHER_UPDATED);

        status = ManagerStatus.Started;
    }

    public void GetHTML(Action<string> callback, string weburl)
    {
        if (_webPage == null)
        {
            StartCoroutine(_network.DownloadPage((string webstring) => {
                _webPage = webstring;
                callback(_webPage);
            }, weburl));
        }
        else
        {
            callback(_webPage);
        }
    }

    public void LogWeather(string name)
    {
        //StartCoroutine(_network.LogWeather(name, cloudValue, OnLogged));
    }
    private void OnLogged(string response)
    {
        Debug.Log(response);
    }
}
