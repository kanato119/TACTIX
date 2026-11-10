using UnityEngine;

public class SelectGameType : MonoBehaviour
{

    private int _id;

    public void SelectDeathmatch()
    {

        Debug.Log("デスマッチ");
        _id = (int)eGameType.Deathmatch;

        SelectServer();
    }

    public void SelectUnrated()
    {

        Debug.Log("アンレート");
        _id = (int)eGameType.Unrated;

        SelectServer();
    }

    private void SelectServer()
    {
        if (SceneSelectManager.instance == null) return;
        SceneSelectManager.instance.ChangeLocalScene(GameConst.SCENE_TEST_NET);
    }

    /// <summary>
    /// 選択したゲームのID
    /// </summary>
    /// <returns></returns>
    public int GetGameType()
    {
        return _id;
    }


}
