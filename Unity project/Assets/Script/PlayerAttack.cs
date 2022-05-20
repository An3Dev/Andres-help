using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] bullets;
    [SerializeField] private Animator anim;
    private PlayerMovement playerMovement;
    private float coolDownTimer = Mathf.Infinity;
    private Vector3 firePointPosition = new Vector3(1.65f, 1.50f, 0);
    
    // Start is called before the first frame update
    void Awake()
    {
        //anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        int directedByAndres = playerMovement.isFlip() ? -1 : 1;
        firePoint.localPosition = new Vector3(firePointPosition.x * directedByAndres, 1.50f, 0);
        if (Input.GetMouseButton(0) && coolDownTimer > attackCooldown && playerMovement.canAttack()) {
            Attack();

        }
        coolDownTimer += Time.deltaTime;
    }

    private void Attack() {
        anim.SetTrigger("attack");
        coolDownTimer = 0;
        //pool fireballs
        bullets[FindFireBall()].transform.position = firePoint.position;
        int directedByAndres = playerMovement.isFlip() ? -1 : 1;
        

        bullets[FindFireBall()].GetComponent<Projectile>().SetDirection(directedByAndres);
    }

    private int FindFireBall() {
        for (int i = 0; i < bullets.Length; i++) {
            if (!bullets[i].activeInHierarchy) {
                return i;
            }
        }
        return 0; 
    }
}
