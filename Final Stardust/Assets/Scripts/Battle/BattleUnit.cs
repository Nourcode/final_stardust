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
    Color originalColor;

    [SerializeField] private Animator myAnimationController;
    [SerializeField] private Animator enemyAnimationController;

    private void Awake() {
        image = GetComponent<Image>();
        originalPos = image.transform.localPosition;
        originalColor = image.color;
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


        image.color = originalColor;
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

    public void PlayHitAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(image.DOColor(Color.grey, 0.2f));
        sequence.Append(image.DOColor(originalColor, 0.2f));
    }

    public void PlayBoostAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(image.DOColor(Color.red, 0.8f));
        sequence.Append(image.DOColor(originalColor, 0.8f));
    }

    public void PlayFaintAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(image.transform.DOLocalMoveY(originalPos.y - 150f, 0.5f));
        sequence.Join(image.DOFade(0f, 0.5f));
    }

}
