using UnityEngine;

public class Attack : MonoBehaviour
{

    public static Attack instance { get; private set; }

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
            //0以下にならないようにする
            PlayerHP.NOW_HP = 0;
            Debug.Log("PlayerのHPが0");


            //オブジェクト破棄
            Destroy(obj);
        }
    }
}
