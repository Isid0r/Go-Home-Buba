using System.Collections;
using UnityEngine;

public class MirrorPlatformLogic : MonoBehaviour
{
    [SerializeField] Transform _point;
    [SerializeField] Transform _origin;
    void Start()
    {
        StartCoroutine("DoReflect");
    }
    IEnumerator DoReflect()
    {
        while (true)
        {
            HouseholderTransformation.LocalReflect(transform, _point.localPosition);
            yield return new WaitForSeconds(5);
            //transform.Translate(new Vector2(0, 1));    // для отзеркаливания движения
            //yield return new WaitForSeconds(5);
        }
    }
    private void OnDrawGizmos()
    {
         Gizmos.DrawLine(_origin.position, _point.position);
    }
}
