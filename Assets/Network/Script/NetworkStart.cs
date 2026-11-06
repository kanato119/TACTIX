using Unity.Netcode;
using UnityEngine;


public class NetworkStart : MonoBehaviour
{

    /// <summary>
    /// サーバー作成
    /// </summary>
    /// <returns></returns>
    public void StartHost()
    {
        Debug.Log("作成しました");
        //すでに通信中なら処理しない
        if (NetworkManager.Singleton.IsListening) return;
        Unity.Netcode.NetworkManager.Singleton.StartHost();
    }

    /// <summary>
    /// 参加
    /// </summary>
    /// <returns></returns>
    public void StartClient()
    {
        Debug.Log("参加しました");
        //すでに通信中なら処理しない
        if (NetworkManager.Singleton.IsListening) return;
        Unity.Netcode.NetworkManager.Singleton.StartClient();
    }

    /// <summary>
    /// 接続解除
    /// </summary>
    public void Shutdown()
    {
      
        Debug.Log("切断しました");
        //通信中じゃないなら処理しない
        if (!NetworkManager.Singleton.IsListening) return;
        NetworkManager.Singleton.Shutdown();
    }


}
