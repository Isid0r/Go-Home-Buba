using System.Collections;
using UnityEngine;

public class FragilePlatformLogic : MonoBehaviour
{
    [SerializeField] float _destroyTime = 3;
    [SerializeField] GameObject _parent;
    Animator _a;

    private void Start()
    {
        _a = _parent.GetComponent<Animator>();
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Timer(_destroyTime));
        }
    }
    IEnumerator Timer(float timeInSec)
    {
        _a.SetBool("isDestroy", true);
        _a.SetFloat("speedMultiplier", 1/_destroyTime);
        yield return new WaitForSeconds(timeInSec);
        Destroy(_parent);
    }
}
