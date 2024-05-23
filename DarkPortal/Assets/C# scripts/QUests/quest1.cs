using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;

public class quest1 : MonoBehaviour
{
    [SerializeField] private Canvas CanvasForDialog;
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Canvas canvasForBtn;
    [SerializeField] private int speed;
    
    private Player player;
    private triggetText triggetDialogue;
    private Animator animator;
    
    private int beginDilogue;
    private bool isInDialog;
    
    private static readonly int Go = Animator.StringToHash("go");

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        var btn = canvasForBtn.GetComponentInChildren<Button>();
        btn.onClick.AddListener(TaskOnClick);
        animator = GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isInDialog && triggetDialogue.end)
        {
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
        CanvasForDialog.enabled = true;
        player.canvasDefault.enabled = false;
        player.speed = 0f;
        isInDialog = true;
    }
}
