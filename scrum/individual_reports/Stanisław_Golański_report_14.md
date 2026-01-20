# Raport indywidualny – Sprint 14

**Imię i nazwisko:** Stanisław Golański  
**Zespół:** Loopers  
**Numer sprintu:** 14  
**Okres:** 2025-01-14 – 2026-01-21  

## Zakres moich działań:
# Dokumentacja funkcjonalności gry

1. **Stworzenie questa eksploracyjnego samouczkowego**


## Wkład w projekt

W tym sprincie skoncentrowałem się na stworzeniu systemu questów i mechanik związanych z interakcją gracza z NPC oraz walką.

Stworzyłem pierwszego questa, w którym gracz poznaje swojego rywala. Dodatkowo opracowałem:

- quest eksploracyjny, polegający na pokonaniu trzech przeciwników,  
- quest walki z bossem, jako wyzwanie końcowe w danym obszarze.

Aby te funkcjonalności działały, dodałem następujące skrypty:

### Skrypty kontrolujące gracza i NPC

- **BattlePause** – tymczasowo blokuje ruch gracza i NPC w trakcie walki, co pozwala na bezpieczne przeprowadzenie pojedynków bez ingerencji w logikę ruchu.  
- **NPCMovement** – kontroluje ruch NPC w świecie gry, np. przemieszczanie się po wykonaniu misji lub podążanie za graczem.

### Skrypty dialogowe

- **Dialogue** – odpowiada za wyświetlanie interfejsu dialogowego w grze.  
- **DialogueManager** – zarządza przebiegiem dialogów, koordynując wyświetlanie kolejnych etapów i warunków.  
- **DialogueStage** – definiuje poszczególne etapy dialogu, umożliwiając rozgałęzianie rozmów w zależności od decyzji gracza.  
- **DialogueTrigger** – inicjuje dialog lub misję w momencie spełnienia określonych warunków w grze.  
- **PlayerDialogueTrigger** – uruchamia odpowiednie etapy dialogów dla gracza, np. po wykonaniu określonej akcji.

### Akcje dialogowe

- **IDialogueAction** – interfejs bazowy dla wszystkich akcji związanych z dialogami, pozwalający łatwo dodawać nowe efekty.  
- **DialogueAction_DestroySelf** – usuwa obiekt (np. NPC lub element otoczenia) po zakończeniu dialogu, co ułatwia dynamiczne zarządzanie światem gry.  
- **DialogueAction_EnemyEncounter** – inicjuje walkę z przeciwnikiem w momencie dialogu lub wydarzenia fabularnego.  
- **DialogueAction_MoveNPC** – powoduje ruch NPC w określonym kierunku lub do punktu docelowego, często używany po zakończeniu dialogu.  
- **DialogueAction_ProgressQuest** – aktualizuje postęp questa w zależności od działań gracza.  
- **DialogueAction_StartQuest** – uruchamia nową misję w świecie gry, łącząc fabułę z mechaniką questów.

### Skrypty questów

- **ConditionalQuest** – pozwala na uruchomienie misji tylko wtedy, gdy spełnione są określone warunki (np. ukończenie wcześniejszego questa).  
- **ObjectiveData** – przechowuje dane dotyczące zadań, takie jak liczba przeciwników do pokonania czy wymagane przedmioty.  
- **ObjectiveUI** – odpowiada za wyświetlanie informacji o aktualnym stanie misji w interfejsie gracza.  
- **PlayerQuestStageTrigger** – obecnie prawdopodobnie nieużywany, planowany do inicjowania kolejnych etapów questa.  
- **QuestData** – zawiera pełen opis questa, jego cele i wymagania, ułatwiając organizację misji w grze.  
- **QuestManager** – centralny system zarządzania questami, który kontroluje uruchamianie, postęp i zakończenie wszystkich misji w grze.

Dzięki tym skryptom udało się stworzyć **spójny system questów**, który pozwala na dynamiczne uruchamianie misji, kontrolę NPC, dialogów oraz interakcję z przeciwnikami, co w znaczący sposób podnosi immersję i jakość rozgrywki.

## Samoocena:

| Obszar | Ocena (1–5) | Komentarz |
|--------|--------------|-----------|
| Zaangażowanie | 5 | Regularna praca, angażowałem się w pracę i realizację wszystkich obowiązków. Aktywnie uczestniczyłem w wykonywaniu zadań. Zadania wykynywałem regularnie, gdy tylko miałem czas. Prawie wszystko zostało wykonane|
| Wkład merytoryczny | 5 | Stworzyłem kilka questów, które sa w fazie prototypowej i wyglądają dosyć prosto ale już działają odpowiednio trzeba je tylko rozszerzyć i poprawić wygląd|
| Komunikacja | 4 | Komunikacja przebiegała bez wiekszych problemów. Każdy wkładał coś od siebie. Uzgodnilismy z Kubą, że zrobi mape, a w następnym tygodniu ja zrobię questa walki z jego pomocą.|
| Terminowość | 4.5 | Zadanie zostało wykonane w terminie, jednak jest w fazie prototypowej i nie wygląda super dobrze, ponieważ nie miałem więcej czasu, żeby polepszyć wygląd, jest to jednak do zrobienia w kolejnym tygodniu. |

## Refleksja:
Udało mi się zrealizować wszystkie cele sprintu, jednak dało się je wykonać lepiej i ładniej. Nastepnym razem musimy wziąć pod uwagę, że zaczynają się egzaminy oraz, że pod koniec semestru wszyscy nawarstwia się ilośc zadań
