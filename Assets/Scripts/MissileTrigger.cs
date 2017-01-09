using UnityEngine;
using System.Collections;

public class MissileTrigger : MonoBehaviour
{
    public int damage;
    public string enemy_tag;
    public string friend_tag;
    public GameObject explosion;
    //private AudioSource audio;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag != friend_tag)
        {
            if (coll.tag != "combat_vision") {
                if (coll.tag != "combat_projectile")
                {
                    //Debug.Log(coll.tag);
                    if (coll.tag == enemy_tag)
                    {
                        int hp = coll.GetComponentInParent<HealthPool>().HitPoints;
                        coll.GetComponentInParent<HealthPool>().HitPoints = hp - damage;
                        //coll.GetComponentInParent<Rigidbody>().AddForce(new Vector3 (0, 10, 0), ForceMode.Impulse);
                    }
                    
                    //audio = GetComponent<AudioSource>();
                    //audio.volume = 10f;
                    //audio.Play();
                    GameObject ExplosionHit = (GameObject)Instantiate(explosion, transform.position, transform.rotation);
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
