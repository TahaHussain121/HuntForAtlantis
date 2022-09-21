using UnityEngine;

public interface IInputHandler
{
    Transform GetTransform();
    void Jump();
    void MoveHorizontally(float horizontal);
    void PrimaryAttack();
    void SpecialAttack();
}
public interface IAttackable
{
    void OnAttacked(CharacterType ctype,AttackType atype);
}
public interface ICharacterManager
{
    IFighter GetCharacterFighter();
    IMovement GetCharacterMovement();
    RageController GetRageController();
}
public interface IFighter
{
    void PrimaryAttack();
    void RageAttack();
    void OnRageBarFilled();
    void OnPrimaryAtttackLanded();
}
public interface IMovement
{
    void Jump();
    void MoveHorizontally(float hor);
}
