using System.Linq;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
public class CanvasControllerQuests : MonoBehaviour
{
    public Canvas canvasShopForQuests;

    // review(30.06.2024): Стоило для квестов выделить отдельную модельку и не хардкодить их, а завести в массив
    public Button firstQuest;
    public TextMeshProUGUI firstQuestTextForButton;
    public Button secondQuest;
    public TextMeshProUGUI secondQuestTextForButton;
    public Player player;

    public Button back;
    private void Start()
    {
        firstQuest.onClick.AddListener(ActivateFirstQuest);
        secondQuest.onClick.AddListener(ActivateSecondQuest);
        back.onClick.AddListener(ExitQuest);
    }
    
    public void FinishQuest(int number)
    {
        if (number == 1)
            UpdateButton(firstQuestTextForButton, firstQuest);
        else if (number == 2)
            UpdateButton(secondQuestTextForButton, secondQuest);
    }

    private void UpdateButton(TextMeshProUGUI text, Button button)
    {
        button.enabled = true;
        text.text = "Сдать квест";
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(GetReward);
        button.onClick.AddListener(() => button.enabled = false);
        button.onClick.AddListener(() => button.gameObject.SetActive(false));
    }

    private void GetReward()
    {
        player.GetComponent<PlayerInventory>().coins += 15;
    }
    
    private void ActivateFirstQuest()
    {
        var quest = player.GetComponent<Quests>();
        quest.StartFirstQuest("Спасти деда", new []{"Бедный дедушка потерялся в этом странном и непонятном мире. Нужно его найти",
            "Он мог попасть в беду, к монстрам. Нужно победить мобов",
            "Спасите деда"}); 
        firstQuestTextForButton.text = "Квест взят";
        firstQuest.enabled = false;
    }
    
    private void ActivateSecondQuest()
    {
        var quest = player.GetComponent<Quests>();
        quest.StartSecondQuest("Найти пропавшую бутылку", new []{"Найти бутылку"});
        secondQuestTextForButton.text = "Квест взят";
        secondQuest.enabled = false;
    }
    
    private void ExitQuest()
    {
        canvasShopForQuests.enabled = !canvasShopForQuests.enabled;
        player.speed = 5f; // review(30.06.2024): не хватает метода player.Unfreeze(), который бы устанавливал правильную дефолтную скорость игроку, а то сейчас у вас везде одна и та же магическая константа
    }
}

// review(30.06.2024): что-то типа такого хотелось видеть
public class CanvasControllerQuests_Refactored : MonoBehaviour
{
    public Canvas canvasShopForQuests;
    [SerializeField] private QuestOnBoard[] quests;
    public Player player;
    public Button back;

    private void Start()
    {
        back.onClick.AddListener(ExitQuest);
    }
    
    public void FinishQuest(int number)
    {
        var quest = quests.FirstOrDefault(x => x.Quest.Id == number);
        if (quest is not null)
            quest.FinishQuest();
    }

    private void ExitQuest()
    {
        canvasShopForQuests.enabled = !canvasShopForQuests.enabled;
        player.speed = 5f;
    }

    private static class QuestsRepository
    {
        private static readonly Quest[] Quests = new[]
        {
            new Quest(1,
                "Спасти деда",
                new[]
                {
                    "Бедный дедушка потерялся в этом странном и непонятном мире. Нужно его найти",
                    "Он мог попасть в беду, к монстрам. Нужно победить мобов",
                    "Спасите деда"
                },
                15),
            new Quest(2, "Найти пропавшую бутылку", new[] { "Найти бутылку" }, 15)
        };

        public static Quest GetById(int id)
        {
            return Quests.First(x => x.Id == id);
        }
    }

    private class QuestOnBoard : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private int questId;
        [SerializeField] private Player player;
        
        // review(30.06.2024): По идее, должны заполняться из редактора
        public Quest Quest => QuestsRepository.GetById(questId);

        public void Start()
        {
            button.onClick.AddListener(Take);
        }

        private void Take()
        {
            if (Quest is null)
                return;
            
            var quests = player.GetComponent<Quests>();
            if (Quest.Id == 1)
                quests.StartFirstQuest(Quest.Name, Quest.Content);
            if (Quest.Id == 2)
                quests.StartSecondQuest(Quest.Name, Quest.Content);
            buttonText.text = "Квест взят";
            button.enabled = false;
        }

        public void FinishQuest()
        {
            button.enabled = true;
            buttonText.text = "Сдать квест";
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(GiveReward);
            button.onClick.AddListener(() => button.enabled = false);
            button.onClick.AddListener(() => button.gameObject.SetActive(false));
        }
        
        private void GiveReward()
        {
            player.GetComponent<PlayerInventory>().coins += Quest.Reward;
        }
    }

    private class Quest
    {
        public int Id { get; }
        public string Name { get; }
        public string[] Content { get; }
        public int Reward { get; }

        public Quest(int id, string name, string[] content, int reward)
        {
            Id = id;
            Name = name;
            Content = content;
            Reward = reward;
        }
    }
}
