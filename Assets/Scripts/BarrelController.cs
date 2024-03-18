using System.Collections;
using UnityEngine;

public class BarrelController : MonoBehaviour
{
    public ParticleSystem muzzle; //Particles
    public GameObject flashObject; //Light
    public GameObject range; //Range of explosion effect

    public int damage = 10;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            muzzle.Play();
            StartCoroutine(Flash());
        }
    }

    public IEnumerator Flash()
    {
        flashObject.SetActive(true);
        range.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        flashObject.SetActive(false);
        Destroy(this.gameObject);
    }
}
