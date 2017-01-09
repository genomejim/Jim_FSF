using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthPool : MonoBehaviour
{
    public int HitPoints;
    public int MaxHitPoints;
    //public TextMesh HPDisplay;
    public Text HPDisplay;

    //public GameObject RubblePrefab;
    public GameObject smoke;

    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HPDisplay.text = HitPoints.ToString();

        if (HitPoints <= 0)
        {
            Vector3 spawnpoint_one;
            spawnpoint_one = gameObject.transform.position;
            Quaternion rotation = new Quaternion(0, 0, 0, 0);
            //Instantiate(RubblePrefab, spawnpoint_one, rotation);
            GameObject smokeHit = (GameObject)Instantiate(smoke, gameObject.transform.position, gameObject.transform.rotation);
            gameObject.SetActive(false);
        }

    }
    void OnCollisionEnter(Collision coll)
    {
        //GetComponent<AudioSource>().Play();
        //Debug.Log (coll.relativeVelocity.magnitude.ToString());
        if (coll.relativeVelocity.magnitude > 12)
        {
            float RelVol;
            RelVol = coll.relativeVelocity.magnitude;
            //HitPoints = HitPoints - System.Convert.ToInt32(RelVol);

        }
        else if (coll.gameObject.tag == "zonewall")
        {
            DestroyObject(gameObject);
        }
    }
}
