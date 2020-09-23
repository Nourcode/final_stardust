using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] MonsterBase _base;
    [SerializeField] int level;
    [SerializeField] bool isPlayerUnit;

    public Monster Monster { get; set; }

    Image image;
    Vector3 originalPos;

    [SerializeField] private Animator myAnimationController;
    [SerializeField] private Animator enemyAnimationController;

    private void Awake() {
        image = GetComponent<Image>();
        originalPos = image.transform.localPosition;
    }

    public void Setup()
    {
        Monster = new Monster(_base, level);
        if(isPlayerUnit)
        {
            image.sprite = Monster.Base.BackSprite;
        } else
        {
            image.sprite = Monster.Base.FrontSprite;
        }

        PlayEnterAnimation();
    }

    public void PlayEnterAnimation()
    {
        if(isPlayerUnit)
        {
            image.transform.localPosition = new Vector3(-500f, originalPos.y);
        } else
        {
            image.transform.localPosition = new Vector3(500f, originalPos.y);
        }

        image.transform.DOLocalMoveX(originalPos.x, 1f);
    }

    public IEnumerator PlayAttackAnimation()
    {

        myAnimationController.SetBool("playAirCut", true);

        yield return new WaitForSeconds(0.1f);

        myAnimationController.SetBool("playAirCut", false);

        yield return null;
        
    }

    public IEnumerator PlayEnemyAttackAnimation()
    {
        
        enemyAnimationController.SetBool("playAquaCut", true);

        yield return new WaitForSeconds(0.1f);

        enemyAnimationController.SetBool("playAquaCut", false);

        yield return null;
        
    }
}
