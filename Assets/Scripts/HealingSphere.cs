using UnityEngine;
using System.Collections;

public class HealingSphere : MonoBehaviour
{

    public int raycast_layer;
    public string enemy_tag;
    public string ally_tag;
    public Collider target;
    public bool target_aquired;
    //public LineRenderer beam;
    Color c1 = Color.magenta;
    //Color c2 = Color.cyan;
    Color c2 = Color.red;
    //	Color c4 = Color.cyan;
    public int lengthOfLineRenderer = 2;
    public LineRenderer lineRenderer;
    public Material lineRendererMaterial;
    public bool time_to_reload;
    public float second;
    public int damage;
    public float shot_delay_factor;
    public TextMesh textobject;
    public bool use_linerenderer;
    private Ray sight;
    public int heal_amount;


    //private AICharacterControl target_script;

    // Use this for initialization
    void Start()
    {

        target_aquired = false;
        time_to_reload = true;
        second = 0;
        gameObject.GetComponentInParent<MeshRenderer>().enabled = false;
        gameObject.GetComponentInParent<ParticleSystem>().Pause();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }


    void OnTriggerStay(Collider coll)
    {

        if (coll.tag == ally_tag)
        {
            if (time_to_reload == false)
            {
                second += Time.deltaTime + UnityEngine.Random.Range(-shot_delay_factor, 1f);
                if (second >= 7)
                {
                    gameObject.GetComponentInParent<MeshRenderer>().enabled = false;
                }
                //Debug.Log(second.ToString());
                if (second >= 10)
                {
                    //Debug.Log(second.ToString());
                    time_to_reload = true;
                    second = 0;
                }
            }

            if (time_to_reload == true)
            {
                target = coll;


                if (target.tag == ally_tag)
                {

                    //do the heal
                    if (coll.GetComponentInParent<HealthPool>().HitPoints < coll.GetComponentInParent<HealthPool>().MaxHitPoints)
                    {
                        coll.GetComponentInParent<HealthPool>().HitPoints += heal_amount;
                        gameObject.GetComponentInParent<MeshRenderer>().enabled = true;
                        gameObject.GetComponentInParent<ParticleSystem>().Play();
                        time_to_reload = false;
                    }
                    else {
                        gameObject.GetComponentInParent<ParticleSystem>().Stop();
                    }
                }
            }
        }
    }
}