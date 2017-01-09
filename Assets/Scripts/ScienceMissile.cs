using UnityEngine;
using System.Collections;

public class ScienceMissile : MonoBehaviour
{

    public float xoffset;
    public float yoffset;
    public float zoffset;
    public int raycast_layer;
    public string enemy_tag;
    public string ally_tag;
    public Collider target;
    public bool target_aquired;
    Color c1 = Color.magenta;
    //Color c2 = Color.cyan;
    Color c2 = Color.red;
    //Color c4 = Color.cyan;
    public int lengthOfLineRenderer = 2;
    public LineRenderer lineRenderer;
    public Material lineRendererMaterial;
    public float explosionRadius = 3;
    public Vector3 force;
    public bool time_to_reload;
    public float second;
    public int damage;
    public GameObject missile;
    public float shot_delay_factor;
    private Vector3 missile_offset_launch_position;
    public float missile_speed;
    public TextMesh textobject;
    public bool use_linerenderer;
    private Ray sight;


    //private AICharacterControl target_script;

    // Use this for initialization
    void Start()
    {
        if (use_linerenderer)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
            lineRenderer.material = lineRendererMaterial;
            lineRenderer.SetColors(c1, c2);
            lineRenderer.SetWidth(0.2F, .3F);
            lineRenderer.SetVertexCount(lengthOfLineRenderer);
            lineRenderer.enabled = false;
        }
        target_aquired = false;
        time_to_reload = true;
        second = 0;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    void OnTriggerExit(Collider coll)
    {
        gameObject.GetComponentInParent<NavMeshAgent>().speed = 1f;
    }
   
    void OnTriggerStay(Collider coll)
    {
        
               if (coll.tag == enemy_tag)
        {
            if (time_to_reload == false) {
                
                second += Time.deltaTime + UnityEngine.Random.Range(-shot_delay_factor, 1f);
                //Debug.Log(second.ToString());
                if (second >=1 && use_linerenderer)
                {
                    lineRenderer.enabled = false;
                }
                if (second >= 10) {
                    //Debug.Log(second.ToString());
                    time_to_reload = true;
                    second = 0;
                }
            }
            
            if (time_to_reload == true)
            {
                target = coll;         
                //gameObject.GetComponentInParent<NavMeshAgent>().speed = .8f;
                //check line of sight

                RaycastHit hitInfo = new RaycastHit();

                missile_offset_launch_position = gameObject.GetComponentInParent<Rigidbody>().transform.position + new Vector3(xoffset, yoffset, zoffset);

                Vector3 raycast_origin = gameObject.GetComponentInParent<Rigidbody>().transform.position + new Vector3(xoffset, yoffset, zoffset);
                sight = new Ray (raycast_origin, (target.transform.position + new Vector3(xoffset, yoffset, zoffset) - missile_offset_launch_position).normalized);
                bool hitf = Physics.Raycast(sight, out hitInfo);
                if (hitf)
                {
                    if (use_linerenderer)
                    {
                        lineRenderer.SetPosition(0, raycast_origin);
                        //lineRenderer.SetPosition(1, hitInfo.point);
                        lineRenderer.SetPosition(1, target.transform.position + new Vector3(xoffset, yoffset, zoffset));
                    }
                    if (textobject)
                    {
                        //textobject.text = hitInfo.point.ToString();
                        //textobject.text = target.name;
                        textobject.text = hitInfo.collider.tag + " " + hitInfo.collider.name;
                    }
                    if (use_linerenderer)
                    {
                        //lineRenderer.enabled = true;
                    }
                }
                if (hitf && hitInfo.collider.tag != "shot_blocker") { 
                    if (hitInfo.collider.tag != ally_tag) {
                        
                            gameObject.GetComponentInParent<NavMeshAgent>().speed = .8f;
                            //fire missile
                            GameObject new_missile = (GameObject)Instantiate(missile, missile_offset_launch_position, transform.rotation);

                            new_missile.GetComponentInParent<Rigidbody>().velocity = (target.transform.position + new Vector3(xoffset, yoffset, zoffset) - missile_offset_launch_position).normalized * missile_speed;

                            time_to_reload = false;

                            //initiate pursuit
                            //gameObject.GetComponentInParent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>().target = target.transform;

                            //lineRenderer.SetPosition(0, gameObject.GetComponentInParent<Rigidbody>().transform.position);
                            //lineRenderer.SetPosition(0, raycast_origin);
                            //lineRenderer.SetPosition(1, target.transform.position);

                            //lineRenderer.enabled = true;
                        }
                }
            }
        }
    }
}