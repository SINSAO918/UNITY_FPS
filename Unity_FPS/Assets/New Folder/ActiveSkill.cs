using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveSkill : Skill
{
    public float skillCoolTime;
    [SerializeField] protected float skillCoolTimeIndex;
    protected bool CanUse
    {
        get
        {
            return skillCoolTimeIndex < 0;
        }
        set { }
    }
    protected void _Start()
    {
        skillCoolTimeIndex = skillCoolTime;
    }

    protected void CoolDown()
    {
        skillCoolTimeIndex -= Time.deltaTime;
    }
}
