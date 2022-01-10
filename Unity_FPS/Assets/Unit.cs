using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    protected float moveSpeed;

    [SerializeField] float hp;
    public float Hp
    {
        get 
        {
            return hp; 
        }
        set
        {
            if (value <= 0)
                Dead();
            else if (value < hp)
                Hit();
        }
    }
    /// <summary>
    /// 피격당했을 때 호출
    /// </summary>
    protected abstract void Hit();
    /// <summary>
    /// 죽을 데미지를 입었을때 호출
    /// </summary>
    protected abstract void Dead();
}
