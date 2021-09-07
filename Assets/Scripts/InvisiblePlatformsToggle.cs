using System.Collections;
using UnityEngine;

public class InvisiblePlatformsToggle : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] _platformsSR;
    [SerializeField] private float _givenTime = 3f;
    private bool _used = false;
    void Start()
    {
        for (int i = 0; i < _platformsSR.Length; i++)
        {
            _platformsSR[i].enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_used == false)
        {
            _used = true;
            StartCoroutine(Timer());
        }
    }
    IEnumerator Timer() // переименовать
    {
        for (int i = 0; i < _platformsSR.Length; i++)
        {
            _platformsSR[i].enabled = true;
        }

        yield return new WaitForSeconds(_givenTime);

        for (int i = 0; i < _platformsSR.Length; i++)
        {
            _platformsSR[i].enabled = false;
        }
    }
}
