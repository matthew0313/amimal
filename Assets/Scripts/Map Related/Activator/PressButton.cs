using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButton : MonoBehaviour
{
    [SerializeField] Activated[] activation;
    [SerializeField] Animator anim;
    [SerializeField] bool once = true;
    [SerializeField] float pressCooldown = 1.0f;
    bool canUse = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canUse && collision.tag == "Player")
        {
            for(int i = 0; i < activation.Length; i++)
            {
                activation[i].OnActivation();
            }
            anim.SetBool("Pressed", true);
            canUse = false;
            if (!once) StartCoroutine(ReturnDelay());
        }
    }
    IEnumerator ReturnDelay()
    {
        yield return new WaitForSeconds(pressCooldown);
        anim.SetBool("Pressed", false);
        canUse = true;
    }
}
