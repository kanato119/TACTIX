using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;
public class SceneSelectManager
{

    public static SceneSelectManager instance
    {
        get
        {

            if (_instance == null)
            {
                _instance = new SceneSelectManager();
            }

            return _instance;
        }
    }
    private static SceneSelectManager _instance;

    private SceneSelectManager() { }


    /// <summary>
    /// ネットワーク系統以外のシーン遷移
    /// </summary>
    /// <param name="sceneName"></param>
    public void ChangeLocalScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// ホスト専用すべてのclientシーン遷移
    /// 前のシーンは削除される
    /// </summary>
    /// <param name="sceneName"></param>
    public void ChangeNetSceneSingle(string sceneName)
    {
        NetworkManager.Singleton.SceneManager.LoadScene(sceneName,LoadSceneMode.Single);
    }

    /// <summary>
    /// ホスト専用すべてのclientシーン遷移
    /// 前のシーンが残る
    /// </summary>
    /// <param name="sceneName"></param>
    public void ChangeNetScene(string sceneName)
    {
        NetworkManager.Singleton.SceneManager.LoadScene(sceneName,LoadSceneMode.Additive);
    }
}
