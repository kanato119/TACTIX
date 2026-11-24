using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerNameColor : MonoBehaviour
{
    [System.Serializable]
    public class PlayerSlot
    {
        [Tooltip("この枠のプレイヤー名（例: TAROU, ANNA, AI）")]
        public string playerName;

        [Tooltip("背景色を変えるImage")]
        public Image backgroundImage;

        [Tooltip("任意：名前やスコアを表示するテキスト（文字色も変えたい場合）")]
        public TMP_Text labelText; // 標準の UI.Text を使う場合は public Text labelText; に変更
    }

    [Header("自分のプレイヤー名（実行中に変更しても反映可能）")]
    [SerializeField] private string myPlayerName = "TAROU";

    [Header("色の設定")]
    [SerializeField] private Color selfColor = new Color32(46, 125, 50, 255);   // 自分：緑
    [SerializeField] private Color otherColor = new Color32(255, 0, 0, 255);    // 自分以外：赤

    [Tooltip("文字色も変える場合（例：緑背景なら白、赤背景なら白や黒など）")]
    [SerializeField] private bool changeTextColor = false;
    [SerializeField] private Color selfTextColor = Color.white;
    [SerializeField] private Color otherTextColor = Color.white;

    [Header("横並びのプレイヤー枠リスト")]
    [SerializeField] private List<PlayerSlot> playerSlots = new List<PlayerSlot>();

    private void Start()
    {
        RefreshColors();
    }

    // Inspector上で値を変えたときにもすぐプレビューできるようにする（任意）
    private void OnValidate()
    {
        RefreshColors();
    }

    /// <summary>
    /// 現在の myPlayerName に応じて全枠の色を更新する
    /// </summary>
    public void RefreshColors()
    {
        foreach (var slot in playerSlots)
        {
            if (slot == null || slot.backgroundImage == null) continue;

            bool isMe = !string.IsNullOrEmpty(slot.playerName) && slot.playerName == myPlayerName;

            // 背景色の切り替え
            slot.backgroundImage.color = isMe ? selfColor : otherColor;

            // 文字色の切り替え（有効時のみ）
            if (changeTextColor && slot.labelText != null)
            {
                slot.labelText.color = isMe ? selfTextColor : otherTextColor;
            }
        }
    }

    /// <summary>
    /// 外部（ゲーム管理スクリプトやUIボタンなど）から自分の名前を変更して色更新する用
    /// </summary>
    public void SetMyPlayerName(string newName)
    {
        myPlayerName = newName;
        RefreshColors();
    }
}
