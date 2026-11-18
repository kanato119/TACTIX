using Unity.Netcode;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public static Attack instance { get; private set; }

    [SerializeField] HP hpText;

    private void Awake()
    {
        instance = this;
    }
    public void AttackDamage(int damage, GameObject obj)
    {
        if(obj == null)
        {
            Debug.LogWarning("攻撃対象がいません");
            return;
        }

        HP PlayerHP = obj.GetComponent<HP>();

        if (PlayerHP == null)
        {
            return;
        }

        PlayerHP.NOW_HP -= damage;

        Debug.Log("敵にダメージ: " + damage);
        Debug.Log("敵の残りHP: " + PlayerHP.NOW_HP);

        //HPが0になったら
        if (PlayerHP.NOW_HP <= 0)
        {
            PlayerHP.isDead = true;

            //0以下にならないようにする
            PlayerHP.NOW_HP = 0;
            Debug.Log("PlayerのHPが0");

            PlayerHP.ShowHP();

            Destroy(obj);

            Respawn.instance.Respawn_Mode();
        }
    }
}
