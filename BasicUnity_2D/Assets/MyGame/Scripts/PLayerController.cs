using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerController : MonoBehaviour
{
    public GameObject player;
    public Animator anm;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<GameObject>();
        anm = GetComponent<Animator>(); 
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayIdle()
    {
        anm.SetBool("isWalk", false);
        anm.SetBool("isAtk", false) ;
    }

    public void PlayWalk()
    {
        anm.SetBool("isWalk", true);
        anm.SetBool("isAtk", false);
    }

    public void PlayAtk()
    {
        anm.SetBool("isAtk", true);
        anm.SetBool("isWalk", false);
    }
    }

