using UnityEngine;
using M2MqttUnity;
using System.Text;

public class MQTTConnector : M2MqttUnityClient
{
    public void SendPositionToBroker(Vector2 position)
    {
        if(client is null) { return; }

        string json = JsonUtility.ToJson(position);

        client.Publish("escapadinha", Encoding.UTF8.GetBytes(json));
    }

    protected override void OnConnected()
    {
        base.OnConnected();
    }
}
