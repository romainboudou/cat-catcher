using UnityEngine;

public class Cat : MonoBehaviour
{
    public CatType catData;
    private float fallSpeed;

    void Start()
    {
        fallSpeed = catData.fallSpeed;
    }

    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"[Cat] Touched something: {other.name} (Tag: {other.tag})");
        
        if (other.CompareTag("Player"))
        {
            Debug.Log($"OnTriggerEnter: catData = {catData?.name}");
            AudioManager.Instance.PlayCollect();
            ScoreManager.Instance.AddScore(catData.value);
            InventoryManager.Instance.AddToInventory(catData);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Ground"))
        {
            Debug.Log("Cat hit the ground!");
            Destroy(gameObject);
        }
    }
}