using UnityEngine;
using System.Collections;

public class EvilScienceBeam : MonoBehaviour
{

    bool fire_beam;
    public float xoffset;
    public float yoffset;
    public float zoffset;
    public int raycast_layer;
    public string enemy_tag;
    public float raycast_distance;
    public Collider target;
    public bool target_aquired;
    //public LineRenderer beam;
    Color c1 = Color.green;
    //Color c2 = Color.cyan;
    Color c2 = Color.blue;
    //	Color c4 = Color.cyan;
    public int lengthOfLineRenderer = 2;
    public LineRenderer lineRenderer;
    //public LineRenderer leftrayline;
    //public LineRenderer rightrayline;
    public Material lineRendererMaterial;
    public float explosionRadius = 3;
    public Vector3 force;
    public GameObject smoke;
    public bool time_to_reload;
    public float second;
    public int damage;
    private Animator anim;
    //private bool hitl;
    //private bool hitf;
    //private bool hitr;

    //private AICharacterControl target_script;

    // Use this for initialization
    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.material = lineRendererMaterial;
        lineRenderer.SetColors(c1, c2);
        lineRenderer.SetWidth(0.3F, .5F);
        lineRenderer.SetVertexCount(lengthOfLineRenderer);
        //StartCoroutine(Wait());
        anim = GetComponent<Animator>();

        target_aquired = false;
        time_to_reload = true;
        second = 0;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    
    void OnTriggerStay(Collider coll)
    {
        
        //Debug.Log(coll.tag.ToString());
        //Debug.Log(coll.name.ToString());
        //Debug.Log(coll.tag.ToString());
        //Debug.Log(coll.contacts[0].otherCollider.name.ToString());
        if (coll.tag == enemy_tag)
        {
            if (time_to_reload == false) {
                //lineRenderer.enabled = false;
                second += Time.deltaTime + UnityEngine.Random.Range(-1f, 1f);
                //Debug.Log(second.ToString());
                if (second >=1)
                {
                    lineRenderer.enabled = false;
                }
                if (second >= 7) {
                    //Debug.Log(second.ToString());
                    time_to_reload = true;
                    second = 0;
                }
            }
            //second = second + Time.deltaTime % 10;
            if (time_to_reload == true)
            {
                target = coll;
                gameObject.GetComponentInParent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>().target = target.transform;
                //anim.Play("Attack", -1, 0);

                //RaycastHit hitInfo = new RaycastHit();
                //Vector3 raycast_origin = new Vector3(GetComponentInParent<Rigidbody>().position.x + xoffset, GetComponentInParent<Rigidbody>().position.y + yoffset, GetComponentInParent<Rigidbody>().position.z + zoffset);
                //bool hitf = Physics.Raycast(raycast_origin, GetComponent<Rigidbody>().transform.forward, out hitInfo, raycast_distance, layerMask);
                //bool hitf = Physics.Raycast(raycast_origin, target.transform.position, out hitInfo);
                //if (hitf && hitInfo.collider.CompareTag(enemy_tag))
                //{
                    int hp = target.GetComponentInParent<HealthPool>().HitPoints;
                    target.GetComponentInParent<HealthPool>().HitPoints = hp - damage;
                    //target.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
                    GetComponent<AudioSource>().Play();
                    GameObject smokeHit = (GameObject)Instantiate(smoke, target.transform.position, target.transform.rotation);
                    time_to_reload = false;
                    lineRenderer.SetPosition(0, gameObject.GetComponentInParent<Rigidbody>().transform.position);
                    lineRenderer.SetPosition(1, target.transform.position);

                    lineRenderer.enabled = true;
                //}
                //second = 0;
            }
        }
        //second = second + Time.deltaTime % 10;

    }
    
}