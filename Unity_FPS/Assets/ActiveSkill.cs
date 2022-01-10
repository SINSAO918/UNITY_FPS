using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveSkill : Skill
{
    public float skillCoolTime;
    float skillCoolTimeIndex;
    bool CanUse
    {
        get
        {
            return skillCoolTime < 0;
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
