using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Unity.Netcode;


public class NetworkStart : MonoBehaviour
{

    /// <summary>
    /// サーバー作成
    /// </summary>
    /// <returns></returns>
    public void StartHost()
    {
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
        //すでに通信中なら処理しない
        if (NetworkManager.Singleton.IsListening) return;

        Unity.Netcode.NetworkManager.Singleton.StartClient();
    }



}
