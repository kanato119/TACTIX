using System.Threading.Tasks;
using Unity.Services.Multiplayer;
using UnityEngine;

public class SelectGameType : MonoBehaviour
{


    public void SelectDeathmatch()
    {

        Debug.Log("デスマッチ");
       
        SessionManager.instance.SaveGameType(eGameType.Deathmatch);
       
    }

    public void SelectUnrated()
    {

        SessionManager.instance.SaveGameType(eGameType.Unrated);
      
    }

 

}
