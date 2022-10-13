using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerberusAttacker : MonoBehaviour, IFighter, IAttackable
{

    //// Test Variables
    public Transform Target;
    ///
    [SerializeField] List<CerberusHead> headList;
    [SerializeField] private bool isRageBarFull = false;
    [SerializeField] private CerberusHead meleeHead;
    [SerializeField] private AttackType attackType;
    [SerializeField] private CharacterType characterType = CharacterType.Cerberus;

    private Animator anim;
    private ICharacterManager characterManager;

    private bool isAttacking = false;
    private bool isInvincible;
    private bool isMele = false;

    public delegate void Damage(int val);
    public static Damage TakeDamage;
    public void OnEnable()
    {
        CerberusHead.OnDeath += OnDeath;
        CerberusHead.TakeAttack += OnAttacked;
    }
    void Start()
    {
        Target = Gamemanager.ActiveIInputHandler.GetTransform();
        anim = GetComponent<Animator>();
        characterManager = GetComponent<ICharacterManager>();
    }

    /// <summary>
    /// Range Check
    /// </summary>
    /// <returns></returns>
    public bool CheckEnemyInRange()
    {

        bool val = false;

        for (int i = 0; i < headList.Count; i++)
        {
            if (headList[i].CheckRange())
            {
                val = true;
            }
        }

        return val;
    }

    public void PrimaryAttack()
    {
        if (!isRageBarFull && !isMele)
        {
            StartRangeAttack();
        }
    }

    public void MeleeAttack()
    {
        if (!isRageBarFull)
        {
            StartMeleeAttack();
        }
    }

    public void SpecialAttack()
    {

    }

    private void StartMeleeAttack()
    {
        StopAllCoroutines();

        if (!isAttacking && isMele)
        {
            isAttacking = true;
            attackType = AttackType.Melee;

            // StartCoroutine(LungeAttack(meleeHead)); under discussion
        }
    }

    private void StartRangeAttack()
    {
        //StopAllCoroutines();
        if (!isAttacking)
        {
            isAttacking = true;
            attackType = AttackType.Ranged;
            StartCoroutine("RangeAttack");

        }
    }

    /// <summary>
    /// Lunge attack(still have a lot of issues)
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    private IEnumerator LungeAttack(CerberusHead head)
    {
        yield return new WaitForSeconds(2);

        head.TakePosForLunge();
        yield return new WaitForSeconds(0.5f);

        head.LungeAttack();
        yield return new WaitForSeconds(1f);

        head.ResetPos();
        isMele = false;

        isAttacking = false;
    }

    /// <summary>
    /// Range attack (Fireball attack)
    /// </summary>
    /// <returns></returns>
    public IEnumerator RangeAttack()
    {
        Target = Gamemanager.ActiveIInputHandler.GetTransform();
        List<CerberusHead> cb = new List<CerberusHead>();
        cb = ShuffleList(headList);

        for (int i = 0; i < cb.Count; i++)
        {
            FireBallAttack(Target, cb[i]);
            yield return new WaitForSeconds(0.5f);
        }

        isAttacking = false;


    }
    [ContextMenu("RageAttack")]
    public void RageAttack()
    {
        attackType = AttackType.Rage;
        StopAllCoroutines();
        StartCoroutine("Rage");
    }
    public IEnumerator Rage()
    {
        List<CerberusHead> cb = new List<CerberusHead>();
        for (int i = 0; i < 2; i++)
        {
            cb = ShuffleList(headList);
        }
        if (cb != null)
        {
            for (int i = 0; i < cb.Count - 1; i++)
            {
                cb[i].Shake();
            }

            cb[cb.Count - 1].PullBack();
            yield return new WaitForSeconds(1f);
            for (int i = 0; i < cb.Count - 1; i++)
            {
                cb[i].ResetPos();
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < cb.Count - 1; i++)
            {
                cb[i].ThrowLaserbeam();
            }
            yield return new WaitForSeconds(3f);
            for (int i = 0; i < cb.Count; i++)
            {
                cb[i].ResetPos();
            }
        }
        isAttacking = false;
        OnRageBarEmptied();

    }
    private void FireBallAttack(Transform target, CerberusHead head)
    {

        head.ThrowFireball(target);

    }

    public void OnRageBarEmptied()
    {
        isRageBarFull = false;
        characterManager.GetRageController().ResetRage();
    }

    /// <summary>
    /// Run when character get hit not using this for cerberus made new one
    /// </summary>
    /// <param name="ctype"></param>
    /// <param name="atype"></param>
    /// <param name="head"></param>
    public void OnAttacked(CharacterType ctype, AttackType atype)
    {
        //if (isInvincible) return;

        //RageController rageController = characterManager.GetRageController();

        //if (ctype == CharacterType.Minotaur)
        //{
        //    switch (atype)
        //    {
        //        case AttackType.Melee:
        //            rageController.IncreaseRage(rageController.attackedWithMeleePoints);
        //            break;

        //        case AttackType.Ranged:
        //            rageController.IncreaseRage(rageController.attackedWithRangePoints);
        //            break;
        //    }

        //}
        //else if (ctype == CharacterType.Satyr)
        //{
        //    switch (atype)
        //    {
        //        case AttackType.Melee:
        //            rageController.IncreaseRage(rageController.attackedWithMeleePoints);
        //            break;

        //        case AttackType.Ranged:
        //            rageController.IncreaseRage(rageController.attackedWithRangePoints);
        //            break;
        //    }
        //}
    }

    /// <summary>
    /// Run when cerberus get hit
    /// </summary>
    /// <param name="ctype"></param>
    /// <param name="atype"></param>
    /// <param name="head"></param>
    public void OnAttacked(CharacterType ctype, AttackType atype, CerberusHead head)
    {
        if (isInvincible) return;

        RageController rageController = characterManager.GetRageController();

        if (ctype == CharacterType.Minotaur)
        {
            switch (atype)
            {
                case AttackType.Melee:
                    Debug.Log("Minataur melee");
                    rageController.IncreaseRage(rageController.attackedWithMeleePoints);
                    TakeDamage(10);
                    MeleeAttack();
                    break;

                case AttackType.Ranged:
                    rageController.IncreaseRage(rageController.attackedWithRangePoints);
                    TakeDamage(10);
                    break;
            }

        }
        else if (ctype == CharacterType.Satyr)
        {
            switch (atype)
            {
                case AttackType.Melee:
                    rageController.IncreaseRage(rageController.attackedWithMeleePoints);
                    TakeDamage(10);
                    break;

                case AttackType.Ranged:
                    rageController.IncreaseRage(rageController.attackedWithRangePoints);
                    TakeDamage(10);
                    break;
            }
        }
    }

    public void OnRageBarFilled()
    {
        isInvincible = true;
        Debug.Log("rage");
        RageAttack();
    }

    private void OnDeath(CerberusHead head)
    {
        //Not implemente4d (first correct the hardcode attacks)
        headList.Remove(head);
        if (headList.Count == 0)
        {
            Debug.Log("dead doggie");
            this.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Shuffle Elements of given list
    /// </summary>
    /// <param name="givenList"></param>
    /// <returns></returns>
    private List<CerberusHead> ShuffleList(List<CerberusHead> givenList)
    {

        for (int i = 0; i < givenList.Count; i++)
        {
            CerberusHead temp = givenList[i];
            int randomIndex = Random.Range(i, givenList.Count);
            givenList[i] = givenList[randomIndex];
            givenList[randomIndex] = temp;
        }

        return givenList;

    }

    public void OnPrimaryAtttackLanded()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Return attack Type
    /// </summary>
    /// <returns></returns>
    public AttackType GetAttackType()
    {
        return attackType;
    }

    /// <summary>
    /// return character type
    /// </summary>
    /// <returns></returns>
    public CharacterType GetCharacterType()
    {
        return characterType;
    }
}
