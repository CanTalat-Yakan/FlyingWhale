using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Entity
{
    [SerializeField]
    private float m_cooldownTime;

    [SerializeField]
    private GameObject m_projectilePrefab;
    [SerializeField]
    private GameObject m_muzzlePrefab;

    bool m_canShoot = true;


    private void Start()
    {
        //StateMashine, um Zielsuche zu unterstützen
        StartCoroutine(Shoot());
    }


    IEnumerator Shoot()
    {
        while (m_canShoot)
        {
            yield return new WaitForSeconds(Random.value);

            Destroy(Instantiate(m_projectilePrefab, transform.position, transform.localRotation, transform.parent.transform), 2);
            Destroy(Instantiate(m_muzzlePrefab, transform.position, transform.localRotation, transform.parent.transform), 2);
            StartCoroutine(SquishScale.Play(gameObject, GameManager.Instance.m_Curves.SquishSquash, 0.4f, 1));

            yield return new WaitForSeconds(m_cooldownTime);
            yield return null;
        }
    }
}
