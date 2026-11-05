using UnityEngine;
using Unity.Netcode.Components;


public class NetworkStart:MonoBehaviour
{
    
    /// <summary>
    /// サーバー作成
    /// </summary>
    /// <returns></returns>
    private bool StartHost()
    {
        return true;
    }

    /// <summary>
    /// 参加
    /// </summary>
    /// <returns></returns>
    private bool StartClient()
    {
        return true;
    }



}
