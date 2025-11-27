using Unity.VisualScripting;
using UnityEngine;

public class ExperienceGem : MonoBehaviour
{
    [SerializeField] private GameObjectEvent onPlayerPickedGem;

    private float collectionRadius = 3f; // 被玩家吸引的半径

    private int experienceValue;
    private float followSpeed = 8f;
    
    private Transform playerTrans;
    private bool isFollowing = false;

    void Start()
    {
        Player player = GameServices.Get<Player>();
        playerTrans = player.transform;
    }

    void Update()
    {
        if(isFollowing)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTrans.position, followSpeed * Time.deltaTime);
        }
        else
        {
            if(Vector3.Distance(transform.position, playerTrans.position) < collectionRadius)
            {
                isFollowing = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player playerExp))
        {
            playerExp.AddExperience(experienceValue);

            onPlayerPickedGem?.Raise(this.gameObject);
        }
    }

    public void SetExperienceValue(int experienceValue) => this.experienceValue = experienceValue;
}