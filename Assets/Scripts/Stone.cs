using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stone : MonoBehaviour
{
    public UnityEvent onClick;
    public GameManager manager;
    public List<Sprite> Images;
    public List<Material> materials;
    public ParticleSystem StoneParticles;
    public ParticleSystem NewSkinParticles;
    int goal = 100;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(manager.stones>=goal && manager.stones<999999999)
        {
            goal *= 100;
            GetComponent<SpriteRenderer>().sprite = Images[0];
            StoneParticles.GetComponent<ParticleSystemRenderer>().material = materials[0];
            NewSkinParticles.Play();
            Images.RemoveAt(0);
            materials.RemoveAt(0);
        }
        if(transform.localScale.x > 1f)
        {
            transform.localScale -= Time.deltaTime * 0.5f * Vector3.one;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, 0f),45*Time.deltaTime);
        }
    }
    void OnMouseDown()
    {
        onClick.Invoke();
        transform.localScale = Vector3.one*1.2f;
        transform.Rotate(0,0,Random.Range(-20,20));
    }
}
