using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceSkill : ActiveSkill
{
    CameraController cameraCtrl;
    [SerializeField] LayerMask enemy;
    GameObject targetObject;
    [SerializeField] GameObject skillObject;

    Vector2 skillDrection;

    private void Awake()
    {
        cameraCtrl = GetComponent<CameraController>();
    }

    private void Update()
    {
        CoolDown();
        Execute();
    }
    ForceSkillObject obj;
    protected override void Execute()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (CanUse)
            {
                RaycastHit hit;
                Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward) * 10, Color.blue, 3);
                if (Physics.SphereCast(Camera.main.transform.position, 1, Camera.main.transform.forward, out hit, 10, enemy))
                {
                    skillDrection = new Vector2(cameraCtrl.currentRotateX, cameraCtrl.currentRotateY);
                    targetObject = hit.transform.gameObject;
                    skillCoolTimeIndex = skillCoolTime;
                    obj = Instantiate(skillObject, hit.transform).GetComponent<ForceSkillObject>();
                    obj.transform.localPosition = Vector3.zero;
                    Debug.Log(hit.transform.name);
                }
            }
        }

        else if (Input.GetKey(KeyCode.C) && targetObject != null)
        {
            if(skillDrection.x - cameraCtrl.currentRotateX > 20)
            {
                Rigidbody enemyRigid = targetObject.GetComponent<Rigidbody>();
                enemyRigid.velocity = Vector3.zero;
                enemyRigid.AddForce(transform.right * -8, ForceMode.Impulse);
                Destroy(obj.gameObject);
                targetObject = null;
            }
            else if (skillDrection.x - cameraCtrl.currentRotateX < -20)
            {
                Rigidbody enemyRigid = targetObject.GetComponent<Rigidbody>();
                enemyRigid.velocity = Vector3.zero;
                enemyRigid.AddForce(transform.right * 8, ForceMode.Impulse);
                Destroy(obj.gameObject);
                targetObject = null;
            }
            else if (skillDrection.y - cameraCtrl.currentRotateY > 20)
            {
                Rigidbody enemyRigid = targetObject.GetComponent<Rigidbody>();
                enemyRigid.velocity = Vector3.zero;
                enemyRigid.AddForce(transform.up * 8, ForceMode.Impulse);
                Destroy(obj.gameObject);
                targetObject = null;
            }
            else if (skillDrection.y - cameraCtrl.currentRotateY < -20)
            {
                Rigidbody enemyRigid = targetObject.GetComponent<Rigidbody>();
                enemyRigid.velocity = Vector3.zero;
                enemyRigid.AddForce(transform.forward * -8, ForceMode.Impulse);
                Destroy(obj.gameObject);
                targetObject = null;
            }
        }

        else if (Input.GetKeyUp(KeyCode.C) && targetObject != null)
        {
            Rigidbody enemyRigid = targetObject.GetComponent<Rigidbody>();
            enemyRigid.velocity = Vector3.zero;
            enemyRigid.AddForce(transform.forward * 8, ForceMode.Impulse);
            targetObject = null;
            Destroy(obj.gameObject);
        }
    }


}
