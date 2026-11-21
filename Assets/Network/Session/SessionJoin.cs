using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Multiplayer;
using UnityEngine;

public class SessionJoin : MonoBehaviour
{
    private ISession _sessionJoiner;

    private bool _isJoining;

    /// <summary>
    /// マッチメイキングボタン
    /// </summary>
    public async void JoinButton()
    {
        if (_isJoining) return;

        _isJoining = true;

        try
        {
            await JoinServer();
        }
        catch (System.Exception e)
        {
            Debug.LogError($"セッション参加失敗: {e}");
        }
        finally
        {
            _isJoining = false;
        }
    }

    /// <summary>
    /// ゲームモードに合ったセッションに参加
    /// </summary>
    private async Task JoinServer()
    {
        string gameType =
            SessionManager.instance.GetGameTypeString();

        var queryOptions = new QuerySessionsOptions
        {
            Count = 20,

            FilterOptions = new List<FilterOption>
            {
                new FilterOption(
                    FilterField.NumberIndex1,
                    gameType,
                    FilterOperation.Equal
                )
            }
        };
        //セッションの検索
        var result = await MultiplayerService.Instance
        .QuerySessionsAsync(queryOptions);

        Debug.Log($"検索結果: {result.Sessions.Count}件");

        if (result.Sessions.Count == 0)
        {
#if UNITY_EDITOR
            Debug.Log("参加できるセッションがありません");
#endif
            return;
        }

        //条件に合ったセッションに参加
        foreach (var sessionInfo in result.Sessions)
        {
            try
            {
                _sessionJoiner =
                    await MultiplayerService.Instance
                    .JoinSessionByIdAsync(sessionInfo.Id);

                Debug.Log($"参加成功: {_sessionJoiner.Id}");

                // 参加者はホストのネットワークシーン同期を待つ
                return;
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(
                    $"参加失敗: {sessionInfo.Id}\n{e}"
                );
            }
        }

        Debug.Log("すべてのセッションへの参加に失敗しました");
    }
}