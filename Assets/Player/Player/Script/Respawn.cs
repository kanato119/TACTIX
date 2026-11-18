using System.Collections;
using Unity.Netcode;
using UnityEditor.PackageManager;
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

    public void Respawn_Mode()
    {

        switch(gameType)
        {
            case eGameType.Deathmatch:
                StartCoroutine(RespawnPosi());
                break;

            case eGameType.Unrated:
                Debug.Log("アンレート");
                break;
        }    
    }

    IEnumerator RespawnPosi()
    {

        yield return new WaitForSeconds(respawnTime);

        Transform spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];

        Instantiate(PlayerPrefab, spawnPoint.position, spawnPoint.rotation);

    }
}
