using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPBar : MonoBehaviour
{
    [SerializeField] GameObject mana;

    public void SetMP(float mpNormalized)
    {
        mana.transform.localScale = new Vector3(mpNormalized, 1f);
    }

    public IEnumerator SetMPSmooth(float newMana)
    {
        float curMana = mana.transform.localScale.x;
        float changeAmt = curMana - newMana;

        while (curMana - newMana > Mathf.Epsilon)
        {
            curMana -= changeAmt * (Time.deltaTime / 2);
            mana.transform.localScale = new Vector3(curMana, 1f);
            yield return null;
        }
        mana.transform.localScale = new Vector3(newMana, 1f);
            
    }
    
}
