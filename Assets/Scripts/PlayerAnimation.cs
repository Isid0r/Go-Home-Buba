using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator a;
    private void Start()
    {
        a = GetComponent<Animator>();
    }
    public void MoveAnimation(bool check)
    {

        if (check)
        {
            a.SetBool("isRunning", true);
        }
        else
        {
            a.SetBool("isRunning", false);
        }
    }
}
