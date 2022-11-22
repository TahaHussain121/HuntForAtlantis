using UnityEngine;

public enum CharacterType { Satyr, Minotaur, Cerberus,Enemy }
public enum GameState { Startup, InGame, Pause,Dialogue,End }
public enum AttackType { Melee, Ranged,Rage }
public interface IInputHandler
{
    Transform GetTransform();
    void Jump();
    void MoveHorizontally(float horizontal);
    void PrimaryAttack();
    void SpecialAttack();
    void Interact();
}
public interface IAttackable
{
    AttackType GetAttackType(); 
    CharacterType GetCharacterType();
  
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
public interface IInteraction
{
    void Interact();
}

