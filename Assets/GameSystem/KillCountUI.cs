using TMPro;
using UnityEngine;

public class KillCountUI : MonoBehaviour
{
    public RulesDeathMatch player;
    public TextMeshProUGUI killText;

    void Update()
    {
        if (player == null || killText == null)
            return;

        killText.text = player.Kills.ToString();
    }
}
