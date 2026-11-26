using System;
using TMPro;
using UnityEngine;
using Unity.Netcode;

public class HP : NetworkBehaviour
{
    [SerializeField] public int NOW_HP = 150;
    [SerializeField] private int MAX_HP = 150;

    [SerializeField] private TextMeshProUGUI HPText;

    public bool isDead = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;

        ShowHP();
    }

    private void ShowHP()
    {
        HPText.text = NOW_HP.ToString();
    }
}
