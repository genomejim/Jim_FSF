using UnityEngine;
using System.Collections;

public class ScienceBeam : MonoBehaviour
{

    bool fire_beam;
    public float xoffset;
    public float yoffset;
    public float zoffset;
    public int raycast_layer;
    public string enemy_tag;
    public string ally_tag;
    public float raycast_distance;
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
    public float explosionRadius = 3;
    public Vector3 force;
    public GameObject smoke;
    public bool time_to_reload;
    public float second;
    public int damage;
    private Vector3 beam_offset_launch_position;
    private Ray sight;

    // Use this for initialization
    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.material = lineRendererMaterial;
        lineRenderer.SetColors(c1, c2);
        lineRenderer.SetWidth(0.2F, .3F);
        lineRenderer.SetVertexCount(lengthOfLineRenderer);
    
        target_aquired = false;
        time_to_reload = true;
        second = 0;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lineRenderer.enabled = false;
    }

   
    void OnTriggerStay(Collider coll)
    {
        
        if (coll.tag == enemy_tag)
        {
            if (time_to_reload == false) {
                lineRenderer.enabled = false;
                second += Time.deltaTime + UnityEngine.Random.Range(-1f, 1f);
                //Debug.Log(second.ToString());
                if (second > 2)
                {
                    lineRenderer.enabled = false;
                } else {
                    lineRenderer.enabled = true;
                }
                if (second >= 7) {
                    //Debug.Log(second.ToString());
                    time_to_reload = true;
                    second = 0;
                }
            }
            if (time_to_reload == true)
            {
                target = coll;

                beam_offset_launch_position = gameObject.GetComponentInParent<Rigidbody>().transform.position + new Vector3(xoffset, yoffset, zoffset);

                Vector3 raycast_origin = gameObject.GetComponentInParent<Rigidbody>().transform.position + new Vector3(xoffset, yoffset, zoffset);
                sight = new Ray(raycast_origin, (target.transform.position + new Vector3(xoffset, yoffset, zoffset) - beam_offset_launch_position).normalized);
                RaycastHit hitInfo = new RaycastHit();
                bool hitf = Physics.Raycast(sight, out hitInfo);

                int hp = target.GetComponentInParent<HealthPool>().HitPoints;
                    target.GetComponentInParent<HealthPool>().HitPoints = hp - damage;
                    GetComponent<AudioSource>().Play();
                    GameObject smokeHit = (GameObject)Instantiate(smoke, target.transform.position, target.transform.rotation);
                    time_to_reload = false;
                    //lineRenderer.SetPosition(0, gameObject.GetComponentInParent<Rigidbody>().transform.position);
                    lineRenderer.SetPosition(0, beam_offset_launch_position);
                    lineRenderer.SetPosition(1, target.transform.position + new Vector3(xoffset, yoffset, zoffset));

                    lineRenderer.enabled = true;
                //}
                //second = 0;
            }
        }
        //second = second + Time.deltaTime % 10;

    }
    
}