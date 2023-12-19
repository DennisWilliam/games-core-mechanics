using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponBehavior
{
    void AIShooting();
    void Shooting();
    void Reload();
    void ChangeAmmo();
    void BulletsVelocity();
    void Special();
}
