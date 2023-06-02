using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineFix : MonoBehaviour
{
    bool fix = false;
    public PlayerMove player;
    
    public PlayableDirector director;
    // Start is called before the first frame update
    void OnEnable()
    {

        player.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(director.state != PlayState.Playing&&!fix)
        {
            fix = true;
            player.enabled = true;
        }
    }
    
}
