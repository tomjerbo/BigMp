using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCasting : MonoBehaviour
{
    [SerializeField] private Transform castPoint;
    [SerializeField] private GameObject ice;
    [SerializeField] private GameObject iceShard;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) CastSpell();
    }

    private void CastSpell()
    {
        if (Physics.Raycast(castPoint.position, castPoint.forward, out RaycastHit hit,  20f))
        {
            Instantiate(iceShard, transform.position, Quaternion.identity).GetComponent<IceSpellHandler>().Setup(hit.point);
            return;
            Instantiate(ice, hit.point, Quaternion.identity);
            Debug.DrawLine(castPoint.position, hit.point, Color.green, 2f);
            RaycastHit[] sphereHit = Physics.SphereCastAll(hit.point, 5f, Vector3.up);
            foreach (var targets in sphereHit)
            {
                if (targets.collider.TryGetComponent(out Rigidbody rb))
                {
                    rb.AddExplosionForce(10f, hit.point, 20f, .3f, ForceMode.Impulse);
                    Debug.DrawLine(rb.position, rb.position + rb.transform.up * 2, Color.red, 4f);
                }
            }
        }
    }


}
