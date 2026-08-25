# Dark Portal

2D-RPG на Unity: мир с квестами, диалогами, торговцами и боссом. Игрок качает
характеристики, дерётся с мобами, берёт задания у NPC и в какой-то момент
доходит до казино.

![Unity](https://img.shields.io/badge/Unity-2022.3.23f1-000000?style=flat-square&logo=unity&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=flat-square&logo=csharp&logoColor=white)
![Cinemachine](https://img.shields.io/badge/Cinemachine-camera-1E90FF?style=flat-square)
![URP](https://img.shields.io/badge/URP-2D%20Renderer-2E8B57?style=flat-square)

## Что есть в игре

- **Бой** — противники с характеристиками, урон, смерть, полоски здоровья над юнитами
- **Босс** — отдельный сценарий боя со своими событиями и фазой гибели
- **Квесты** — цепочка заданий с отслеживанием прогресса и наградой
- **Диалоги** — система реплик с NPC и мобами, триггеры по входу в зону
- **Магазин** — покупка предметов, отдельная витрина за квестовую валюту
- **Казино и колесо фортуны** — мини-игры на внутриигровую валюту
- **NPC** — жители мира с собственными репликами и поведением
- **Меню и настройки** — стартовый экран, пауза, переключение сцен
- **Режим крови** — включаемые визуальные эффекты боя

## Как устроено

Характеристики вынесены в общий базовый класс `Entity`, от которого наследуются
и игрок, и противники, и босс:

```csharp
public class Entity : MonoBehaviour
{
    [SerializeField] public int maxHealth;
    [SerializeField] public int dexterity;   // ловкость
    [SerializeField] public int power;       // сила

    public void TakeDamage(int damage) { ... }
    public bool IsDead => health <= 0;
    public void UseSkills() { ... }
}
```

Всё, что нужно балансировать, вынесено в инспектор через `SerializeField` —
правки не требуют перекомпиляции. Состояние между сценами переносит `DataHolder`,
переключением занимается `SceneChanger`, а камерой — Cinemachine.

### Основные скрипты

```
Assets/C# scripts/
├── Entity.cs              # базовые характеристики и урон
├── Fight.cs               # боевая логика
├── DataHolder.cs          # данные между сценами
├── SceneChanger.cs        # переходы
├── Settings.cs            # настройки
├── MenuInGame.cs          # пауза
├── Casino.cs              # мини-игра
├── FortuneWhiil.cs        # колесо фортуны
├── Boss/                  # босс и события его гибели
├── Quests/                # квесты, витрина наград, UI
├── Dialoges/              # менеджер диалогов, триггеры реплик
├── Enemies/               # спавн и анимации мобов
└── NPC/                   # жители мира
```

## Сцены

| Сцена | Назначение |
|---|---|
| `_Menu` | Главное меню |
| `Game` | Основной игровой мир |

Остальные сцены в проекте — примеры из пакета Cinemachine.

## Стек

- **Unity 2022.3.23f1 LTS**
- **URP** с 2D Renderer
- **Cinemachine** — следование камеры и boss-камера
- **AI Navigation** — перемещение мобов
- **Feature Set 2D** — тайлмапы, спрайты, 2D-физика

## Запуск

1. Установи Unity **2022.3.23f1** через Unity Hub
2. Клонируй репозиторий:

```bash
git clone https://github.com/SK0LX/SharpGame.git
```

3. Открой папку `DarkPortal` как проект в Unity Hub
4. Загрузи сцену `_Menu` и нажми Play

При первом открытии Unity пересоберёт библиотеку импорта — это займёт несколько минут.
