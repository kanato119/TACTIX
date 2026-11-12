using UnityEngine;
using Unity.Netcode;

public class NetworkDebug : MonoBehaviour
{
    private NetworkManager manager;

    private void Start()
    {
        manager = NetworkManager.Singleton;

        if (manager == null)
        {
            Debug.LogError("NetworkManagerがありません");
            return;
        }

        Debug.Log("監視開始");
        Debug.Log("IsListening : " + manager.IsListening);

        manager.OnClientConnectedCallback += OnConnected;
        manager.OnClientDisconnectCallback += OnDisconnected;

        foreach (ulong id in manager.ConnectedClientsIds)
        {
            Debug.Log("現在の接続プレイヤー : " + id);
        }
    }

    private void OnConnected(ulong clientId)
    {
        var manager = NetworkManager.Singleton;

        Debug.Log("========== 接続確認 ==========");
        Debug.Log("ClientID : " + clientId);
        Debug.Log("IsListening : " + manager.IsListening);
        Debug.Log("IsServer : " + manager.IsServer);
        Debug.Log("IsClient : " + manager.IsClient);
        Debug.Log("接続人数 : " + manager.ConnectedClientsIds.Count);

        if (manager.ConnectedClients.TryGetValue(
            clientId, out var client))
        {
            Debug.Log("PlayerObject : " + client.PlayerObject);
        }
    }

    private void OnDisconnected(ulong clientId)
    {
        Debug.LogError("【切断】ClientID : " + clientId);
        Debug.LogError("IsListening : " + manager.IsListening);
        Debug.LogError("IsServer : " + manager.IsServer);
    }

    private void OnDestroy()
    {
        if (manager == null) return;

        manager.OnClientConnectedCallback -= OnConnected;
        manager.OnClientDisconnectCallback -= OnDisconnected;
    }
}