using UnityEngine;

public class Bullet : MonoBehaviour
{

    private void OnCollisionEnter(Collision objectWeHit)
    {
        Debug.Log("Collision detected with " + objectWeHit.gameObject.name);

        if (objectWeHit.gameObject.CompareTag("Target"))
        {
            Debug.Log("Bullet hit the target: " + objectWeHit.gameObject.name + "!");
            CreateBulletImpactEffect(objectWeHit);
            Destroy(gameObject);
        }
        if (objectWeHit.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Bullet hit the wall: " + objectWeHit.gameObject.name + "!");
            CreateBulletImpactEffect(objectWeHit);
            Destroy(gameObject);
        }
    }

    void CreateBulletImpactEffect(Collision objectWeHit)
    {

        if (GlobalReferences.Instance == null)
        {
            Debug.LogError("GlobalReferences instance is null!");
            return;
        }

        if (GlobalReferences.Instance.bulletImpactEffectPrefab == null)
        {
            Debug.LogError("bulletImpactEffectPrefab is not assigned!");
            return;
        }

        ContactPoint contact = objectWeHit.contacts[0];

        GameObject hole = Instantiate(
            GlobalReferences.Instance.bulletImpactEffectPrefab,
            contact.point,
            Quaternion.LookRotation(contact.normal)
            );


        hole.transform.SetParent(objectWeHit.gameObject.transform);

    }




}