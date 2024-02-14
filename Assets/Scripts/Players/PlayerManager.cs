using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public MonoBehaviour taggerState;
    public MonoBehaviour runnerState;
    public MonoBehaviour deadState;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BecomeTagger()
    {
        gameObject.GetComponent<StateManager>().ChangeState(taggerState);
    }
    
    public void BecomeRunner()
    {
        gameObject.GetComponent<StateManager>().ChangeState(runnerState);
    }
    
    public void BecomeDead()
    {
        gameObject.GetComponent<StateManager>().ChangeState(deadState);
    }
}
