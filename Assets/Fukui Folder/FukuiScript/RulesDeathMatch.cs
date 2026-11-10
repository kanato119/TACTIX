using UnityEngine;
using UnityEngine.SceneManagement;

public class RulesDeathMatch : MonoBehaviour
{
    // 残機
    public int LivesLeft = 0;
    // キル数
    public int Kills = 0;


    // デスマッチと攻防戦を分けるため
    // シーンを取得
    private string sceneName;

    void Start()
    {
        // 現在のシーン名を取得し、sceneNameに入れる
        sceneName = SceneManager.GetActiveScene().name;
        ResurrectionRules();
    }

    void Update()
    {
        Test();
    }

    public void ResurrectionRules()
    {
        if (sceneName == "DeathMatch")
        {
            LivesLeft = 9999;
            Debug.Log(LivesLeft);
        }
      
    }

    private void Test()
    {
        if (Input.GetKeyDown(KeyCode.D) && gameObject.CompareTag("FukuiTextPlayer1") && !DeathMatchResult.GameEnded)
        {
            LivesLeft--;
            Kills++;

            Debug.Log(gameObject.name + "残機：" + LivesLeft);
            Debug.Log(gameObject.name + "キル数：" + Kills);
         
        }
        if (Input.GetKeyDown(KeyCode.K) && gameObject.CompareTag("FukuiTestPlayer2") && !DeathMatchResult.GameEnded)
        {
            LivesLeft -=2;
            Kills += 2;

            Debug.Log(gameObject.name + "残機：" + LivesLeft);
            Debug.Log(gameObject.name + "キル数：" + Kills);
          
        }
        if (Input.GetKeyDown(KeyCode.F) && gameObject.CompareTag("FukuiTestPlayer3") && !DeathMatchResult.GameEnded)
        {
            LivesLeft -= 3;
            Kills += 3;

            Debug.Log(gameObject.name + "残機：" + LivesLeft);
            Debug.Log(gameObject.name + "キル数：" + Kills);
         
        }

    }
}