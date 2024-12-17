using UnityEngine;

public class Page : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            
            PageManager.instance.CollectPage();

           
            Enemy enemy = FindObjectOfType<Enemy>();
            if (enemy != null)
            {
                PageManager pageManager = PageManager.instance;
                enemy.UpdateEnemyStats(pageManager.GetCollectedPages()); 
            }

            
            Destroy(gameObject);
        }
    }
}
