using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //instance varibles
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private BoxCollider2D boxCollider;
    private Animator anim;
    private float shotTimer;
    
    // Start is called before the first frame update
    private void Awake()
    {
        //initializes the animator of the bullet
        anim = GetComponent<Animator>();
        //initializes the bullet collider
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0, Space.World);

        //bullet.is more that 10 seconds ? yes : no;
        if (6 < shotTimer) {
            gameObject.SetActive(false);
            shotTimer = 0;

        }
        shotTimer += Time.deltaTime;
    }
    //if the bullet collided with something, play animation 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explode");
    }
    //changes the direction of the fire point, so that if player turns, it fires in the
    //same direction
    public void SetDirection(float _direction) {
        direction = _direction;
        gameObject.SetActive(true);

        hit = false;
        boxCollider.enabled = true;
        shotTimer = 0;
        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction){
            localScaleX = -localScaleX;
        }
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);

    }
    //deativates the bullet when it collides with something
    private void Deactivate() {
        gameObject.SetActive(false);
    }
    
}
