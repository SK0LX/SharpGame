using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;

// review(30.06.2024): Просто суффикс в виде номера квеста - не лучший нейминг. Стоит уточнить, что за квест
public class Quest1 : MonoBehaviour
{
    public Canvas CanvasForDialog;
    public TextMeshProUGUI name;
    public TextMeshProUGUI text;
    public Canvas canvasForBtn;
    public Button btnForDed;
    [SerializeField] private int speed;
    
    private Player player;
    public TriggetText triggetDialogue;
    private Animator animator;
    
    private int beginDilogue;
    private bool isInDialog;
    
    private static readonly int Go = Animator.StringToHash("go");

    void Start()
    {
        canvasForBtn.enabled = false;
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        btnForDed.onClick.AddListener(TaskOnClick);
        animator = GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isInDialog)
        {
            animator.SetTrigger(Go);
            transform.eulerAngles = new Vector3(0, 0, 0);
            if (transform.position.x  > 125) // review(30.06.2024): Что за магическая константа?
                transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            else
                Destroy(gameObject);
        }

        if (triggetDialogue.end)
        {
            triggetDialogue.end = false;
            player.canvasDefault.enabled = true;
            player.speed = 5f;
            CanvasForDialog.enabled = false;
        }
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
    
    private void TaskOnClick()
    {
        triggetDialogue.TriggerDialog(CanvasForDialog, name, text);
        beginDilogue = 0;
        player.canvasDefault.enabled = false;
        player.speed = 0f;
        isInDialog = true;
        FindObjectOfType<Quests>().FinishQuest(1); // review(30.06.2024): Магическая константа
    }
}
