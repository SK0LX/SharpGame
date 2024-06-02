using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;

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
        if (isInDialog && triggetDialogue.end)
        {
            triggetDialogue.end = false;
            animator.SetTrigger(Go);
            transform.eulerAngles = new Vector3(0, 0, 0);
            if (transform.position.x  > -70)
                transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            else
            {
                player.canvasDefault.enabled = true;
                Destroy(gameObject);
            }
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
    }
}
