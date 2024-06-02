using UnityEngine;
using UnityEngine.UI;

public class Quest2 : MonoBehaviour
{
    [SerializeField] private Canvas canvasForBtn;
    // Start is called before the first frame update
    void Start()
    {
        var btn = canvasForBtn.GetComponentInChildren<Button>();
        btn.onClick.AddListener(TaskOnClick);
        gameObject.SetActive(false);
    }
    
    private void TaskOnClick()
    {
        FindObjectOfType<Quests>().FinishQuest(2);
        Destroy(gameObject);
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            canvasForBtn.enabled = true;
    }
    
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            canvasForBtn.enabled = false;
    }
}
