using UnityEngine;

public class SmallBarnacle : MonoBehaviour
{
    public SmallBarnacleCluster parentCluster;

    public void OnBrushedOver()
    {
        parentCluster?.RemovedFromParentCluster(this);
        JobManager.Instance.SmallBarnacleCleared();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
