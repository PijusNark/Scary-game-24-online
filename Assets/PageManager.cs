using UnityEngine;
using TMPro; 

public class PageManager : MonoBehaviour
{
    public static PageManager instance;
    public int totalPages = 8;     
    private int collectedPages = 0;   

    public TextMeshProUGUI pageText;  

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void CollectPage()
    {
        collectedPages++;
        UpdatePageUI();

        if (collectedPages >= totalPages)
        {
            Debug.Log("All Pages Collected! You Win!");
            
        }
    }

    public int GetCollectedPages()
    {
        return collectedPages;
    }

    void UpdatePageUI()
    {
        pageText.text = $"Pages: {collectedPages} / {totalPages}";
    }
}
