using System.Collections.Generic;
using UnityEngine;

public class SmallBarnacleCluster : MonoBehaviour, IsClickableBarnacle
{
    // Variables for class logic
    private int remainingChildren;

    // Variables for cluster logic
    private List<GameObject> allSmallInCluster = new List<GameObject>();
    private List<GameObject> smallLeftInCluster = new List<GameObject>();

    // Variables for spawner logic
    public BarnacleSpawner spawner;
    public Transform occupiedSpot;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (Transform child in transform)
        {
            var smallBarnacle = child.GetComponent<SmallBarnacle>();
            if (smallBarnacle != null)
            {
                smallBarnacle.parentCluster = this;
            }

            allSmallInCluster.Add(child.gameObject);
        }

        allSmallInCluster.Reverse();
        // Make a copy to track barnacles left for removal in cluster
        smallLeftInCluster = new List<GameObject>(allSmallInCluster);

        remainingChildren = allSmallInCluster.Count;
    }

    // remove 3 small barnacles each click until 4th click then destory cluster object ONLY if using Pick Tool.
    public void OnClickByPick()
    {
        int toRemove = Mathf.Min(3, smallLeftInCluster.Count);

        for (int i = 0; i < toRemove; i++)
        {
            GameObject barnacle = smallLeftInCluster[0];
            smallLeftInCluster.RemoveAt(0);
            remainingChildren--;
            Destroy(barnacle);
            JobUI.Instance.IncrementSmallBarnacleCount();
        }

        if (smallLeftInCluster.Count == 0)
        {
            DestroySelf();
        }
    }

    public void DestroySelf()
    {
        spawner?.FreeUpSpot(occupiedSpot);
        Destroy(gameObject);
    }

    public void RemovedFromParentCluster(SmallBarnacle barnacle)
    {
        if (!smallLeftInCluster.Contains(barnacle.gameObject)) return;

        smallLeftInCluster.Remove(barnacle.gameObject);
        remainingChildren--;

        Destroy(barnacle.gameObject);
        JobUI.Instance.IncrementSmallBarnacleCount();

        if (remainingChildren <= 0)
        {
            DestroySelf();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (smallLeftInCluster == null)
        //{
        //    DestroySelf();
        //}
    }
}
