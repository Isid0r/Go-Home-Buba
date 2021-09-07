using System.Collections;
using UnityEngine;

public class ShooterLogic : MonoBehaviour
{
    [SerializeField] GameObject _ammo;
    [SerializeField] float _fireRate = 1;
    private void Start()
    {
        StartCoroutine(DoShot());
    }
    IEnumerator DoShot()
    {
        while (true)
        {
            Instantiate(_ammo, new Vector2(transform.position.x,transform.position.y + .8f), transform.rotation, gameObject.transform);
            yield return new WaitForSeconds(_fireRate);
        }
    }
}
