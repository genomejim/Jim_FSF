using UnityEngine;
using System.Collections;

public class ScienceBeam1 : MonoBehaviour {

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

    //private AICharacterControl target_script;

    // Use this for initialization
    void Start () {
        //lineRenderer = gameObject.AddComponent<LineRenderer>();
        //lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.material = lineRendererMaterial;
        lineRenderer.SetColors(c1, c2);
		lineRenderer.SetWidth(0.2F, .3F);
		lineRenderer.SetVertexCount(lengthOfLineRenderer);
        //gameObject.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>().target =
        force = new Vector3(1, 0, 0);
        //AudioSource audio = GetComponent<AudioSource>();
        target_aquired = false;
        time_to_reload = true;
        
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		//bool fire_beam = Input.GetKey (KeyCode.LeftControl);
		//if (fire_beam) {
					Engage ();
		//		}
	}

	void Engage () {
		
		Vector3 raycast_origin = new Vector3 (GetComponent<Rigidbody>().position.x + xoffset, GetComponent<Rigidbody>().position.y + yoffset, GetComponent<Rigidbody>().position.z + zoffset);
		
		int layerMask = 1 << raycast_layer;
        //Vector3 ahead = GetComponent<Rigidbody>().transform.forward);
        //Vector3 behind = new Vector3 (-1,0);
        RaycastHit hitInfo = new RaycastHit();  
        bool hitf = Physics.Raycast(raycast_origin, GetComponent<Rigidbody>().transform.forward, out hitInfo, raycast_distance,layerMask);
        //bool hitf = Physics.Raycast()
        		//RaycastHit hitb = Physics.Raycast (raycast_origin, GetComponent<Rigidbody>().transform.forward, raycast_distance,layerMask);

		//Debug.DrawLine (raycast_origin,hit.point,Color.red,100f);
		//Debug.Log (hit.collider.name);
		if (hitf) {
            if (time_to_reload == true)
            {
                lineRenderer.enabled = true;
                target = hitInfo.collider;
                target_aquired = true;
                gameObject.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>().target = target.transform;

                target.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
                GetComponent<AudioSource>().Play();
                //GetComponent<AudioSource>().Play(44100);
                GameObject smokeHit = (GameObject)Instantiate(smoke, target.transform.position, target.transform.rotation);
                time_to_reload = false;
            }
        }
        else {
            //lineRenderer.enabled = false;
            time_to_reload = true;

		}
        Vector3 beam_start = new Vector3(transform.position.x + xoffset, transform.position.y + yoffset, transform.position.z + zoffset);
        if (target_aquired)
        {
            lineRenderer.SetPosition(0, beam_start);
            lineRenderer.SetPosition(1, target.transform.position);
            
        }
        //lineRenderer.SetPosition(1, hitInfo.point);
    }



}
