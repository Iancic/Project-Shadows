using UnityEngine;

public class ToolSelector : MonoBehaviour
{

    public AudioSource flashlightClick;

    private void Awake()
    {
        flashlightClick = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (ShootBullet.Instance.isSelected == false)
            {
                flashlightClick.Play();
                ShootBullet.Instance.isSelected = true;
                Flashlight.Instance.isOn = false;
                PlayerController.Instance.canShoot = true;

                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject enemy in enemies)
                    enemy.GetComponent<EnemyController>().isStunned = false;
            }
            else if (PlayerController.Instance.batteryCurrent > 0)
            {
                flashlightClick.Play();
                ShootBullet.Instance.isSelected = false;
                Flashlight.Instance.isOn = true;
                PlayerController.Instance.canShoot = false;
            }
        }
    }

}
