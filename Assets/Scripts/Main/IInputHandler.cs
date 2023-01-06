using UnityEngine;
using UnityEngine.UI;

public enum CharacterType { Satyr, Minotaur, Cerberus,Enemy }
public enum GameState { Startup, InGame, Pause,Dialogue,End }
public enum AttackType { Melee, Ranged,Rage }
public abstract class Health:MonoBehaviour
{
    [SerializeField] protected int maxHealth ;
    [SerializeField] protected int currentHealth ;
    [SerializeField] protected Slider healthSlider;

    public delegate void HealthDelegate(int val);
    public static HealthDelegate SetupHealth;
    public abstract void TakeDamage(int val);

    public abstract void HealHealthByPercentage(float percentage);
}
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
    Health GetHealthController();
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

