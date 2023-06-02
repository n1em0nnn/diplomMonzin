using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineEnd : MonoBehaviour
{
    public PlayableDirector director;
    public bool end;

    // Update is called once per frame
    void Update()
    {
        if(end)
        {
            director.Play();
        }
        
    }
}
