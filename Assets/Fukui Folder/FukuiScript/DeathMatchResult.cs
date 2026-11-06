using UnityEngine;

public class DeathMatchResult : MonoBehaviour
{
    public RulesDeathMatch[] players;

    public void CheckWinner()
    {
        int maxKills = 0;

        // 最大キル数を取得
        foreach (RulesDeathMatch player in players)
        {
            if (player.Kills > maxKills)
            {
                maxKills = player.Kills;
            }
        }

        // 最大キル数の人を探す
        foreach (RulesDeathMatch player in players)
        {
            if (player.Kills == maxKills)
            {
                Debug.Log("勝ち！");
            }
        }
    }
}
