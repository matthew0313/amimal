using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretTarget : MonoBehaviour
{
    public List<Turret> targetted;
    [SerializeField] Animator targetMark;
    [SerializeField] GameObject trackArrow, parent;
    List<PointArrow> arrows = new List<PointArrow>();
    public void TargetAdd(Turret shooter)
    {
        if (!targetted.Contains(shooter))
        {
            targetted.Add(shooter);
            TargetUpdate();
            GameObject obj = Instantiate(trackArrow, parent.transform);
            obj.transform.localPosition = Vector3.zero;
            PointArrow p = obj.GetComponent<PointArrow>();
            p.pointTarget = shooter.transform;
            arrows.Add(p);
        }
    }
    public void TargetRemove(Turret shooter)
    {
        if (targetted.Contains(shooter))
        {
            targetted.Remove(shooter);
            TargetUpdate();
            for(int i = 0; i < arrows.Count; i++)
            {
                if (arrows[i].pointTarget == shooter.transform)
                {
                    GameObject tmp = arrows[i].gameObject;
                    arrows.Remove(arrows[i]);
                    Destroy(tmp);
                    break;
                }
            }
        }
    }
    public void TargetUpdate()
    {
        targetMark.SetBool("Targetted", targetted.Count>0);
    }
}
