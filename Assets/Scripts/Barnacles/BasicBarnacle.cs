using UnityEngine;

public class BasicBarnacle : MonoBehaviour, IsClickableBarnacle
{

    public Sprite[] frames;
    private int currentFrame = 0;
    private SpriteRenderer sr;

    public BarnacleSpawner spawner;
    public Transform occupiedSpot;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = frames[currentFrame];
    }
    
    // Cycle through sprite sheet until last frame then destory object ONLY if using Pick Tool.
    public void OnClickByPick()
    {
        if (currentFrame >= frames.Length - 1)
        {
            DestroySelf();
            return;
        }

        currentFrame++;
        sr.sprite = frames[currentFrame];
    }

    public void DestroySelf()
    {
        spawner?.FreeUpSpot(occupiedSpot);
        Destroy(gameObject);
        JobUI.Instance.IncrementBasicBarnacleCount();
        JobManager.Instance.BarnacleCleared();
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
