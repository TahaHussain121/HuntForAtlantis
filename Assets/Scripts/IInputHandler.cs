using UnityEngine;

public interface IInputHandler
{
    Transform GetTransform();
    void Jump();
    void MoveHorizontally(float horizontal);
    void PrimaryAttack();
    void SpecialAttack();
}
