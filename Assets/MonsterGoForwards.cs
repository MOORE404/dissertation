using UnityEngine;

public class MonsterGoForwards : MonoBehaviour
{
    public float distance = 5f; 
    public float duration = 5f;

    public Transform target;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float elapsedTime;

    void Start()
    {
        
        startPosition = target.transform.position;

       
        targetPosition = startPosition + target.transform.forward * distance;

        
        elapsedTime = 0f;
    }

    void Update()
    {
        if (elapsedTime < duration)
        {
            
            elapsedTime += Time.deltaTime;

            
            float progress = elapsedTime / duration;

            
            float easedProgress = Mathf.SmoothStep(0, 1, progress);

            
            target.transform.position = Vector3.Lerp(startPosition, targetPosition, easedProgress);
        }
    }
}