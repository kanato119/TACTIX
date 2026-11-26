using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public static Respawn instance { get; private set; }

    [SerializeField] Transform[] spawnPoints;
    [SerializeField] float respawnTime = 2f;

    [SerializeField] GameObject PlayerPrefab;

   [SerializeField] private eGameType gameType;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Respawn_Mode(GameObject obj)
    {

        switch(gameType)
        {
            case eGameType.Deathmatch:
                StartCoroutine(RespawnPosi(obj));
                break;

            case eGameType.TeamBattle:
                Debug.Log("アンレート");
                break;
        }    
    }

    IEnumerator RespawnPosi(GameObject obj)
    {
        HP PlayerHP = obj.GetComponent<HP>();

        yield return new WaitForSeconds(respawnTime);

        Transform spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];

        obj.transform.position = spawnPoint.position;

        PlayerHP.NOW_HP = 150;

        obj.SetActive(true);

    }
}
