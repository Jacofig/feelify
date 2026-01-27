# Raport indywidualny – Sprint tygodniowy 15

**Imię i nazwisko:**  Mateusz Kopala  
**Zespół:** Loopers  
**Numer sprintu tygodniowego:** 15  
**Okres:** 2025-01-21 – 2026-01-28  

## Zakres moich działań

W trakcie bieżącego sprintu moim głównym zadaniem było **przygotowanie questa tutorialowego do Forga** i synchronizacja Forga z ekwipunkiem.

---

## Rozbudowa systemu Forge o tryb tutorialowy i system ładowania

W ramach bieżącego sprintu rozbudowałem mechanikę Forge o **tryb tutorialowy**, który wprowadza gracza w podstawy systemu kucia, oraz o mechanizm automatycznego wyboru trybu gry na podstawie postępu w zadaniach.

---

### Dwa tryby działania Forge

System Forge został rozszerzony o dwa niezależne tryby:

- **Tryb standardowy**
  - dostępny po ukończeniu zadania tutorialowego,
  - umożliwia pełne korzystanie z mechaniki Forge,
  - nie narzuca ograniczeń ani podpowiedzi.

- **Tryb tutorialowy**
  - aktywowany przy pierwszym kontakcie gracza z Forge,
  - prowadzi gracza krok po kroku przez proces kucia

Dzięki temu nowi gracze są stopniowo wprowadzani w system. 

---

### Rozbudowa questa tutorialowego

Quest tutorialowy został rozszerzony i zintegrowany bezpośrednio z mechaniką Forge.

W ramach zadania:

- gracz uczy się:
  - zasad nagrzewania metalu,
  - poprawnego używania `hit()`,
  - momentu użycia `cast()`,
- interfejs wyświetla kontekstowe instrukcje,
- system reaguje na błędne działania,
- postęp questa jest aktualizowany na bieżąco.

Quest pełni rolę interaktywnego samouczka, a nie jedynie zestawu komunikatów tekstowych. Rownież uczy podstaw programowani jak tworzenia i przypisywania wartości do zmiennych. Petle while i ifa.  

---

### System automatycznego ładowania Forge (Forge Loader)

Zaimplementowano specjalny moduł **Forge Loader**, odpowiedzialny za wybór odpowiedniego trybu działania Forge.

Jego zadaniem jest:

- sprawdzenie, czy gracz ukończył quest tutorialowy,
- analiza zapisanych danych postępu,
- wybór trybu uruchomienia Forge:
  - tryb standardowy — po ukończeniu tutoriala,
  - tryb tutorialowy — jeśli zadanie nie zostało wykonane.

Proces ten odbywa się automatycznie przy wejściu do Forge i nie wymaga dodatkowej interakcji gracza.

---

### Integracja systemu z logiką questów

System Forge został powiązany z systemem questów w taki sposób, aby:

- ukończenie tutoriala było zapisywane w stanie gry,
- Forge reagował na zmiany statusu zadania,
- możliwa była dalsza rozbudowa kolejnych tutoriali,
- zachowana została spójność pomiędzy UI, logiką i progresją gracza.

Pozwala to na łatwe zarządzanie dostępnością funkcji w zależności od postępu rozgrywki.

---

## Wkład w projekt

Mój wkład w tym sprincie obejmował:

- rozbudowę mechaniki Forge o **dwa tryby działania**,
- implementację i integrację **questa tutorialowego**,
- stworzenie systemu **Forge Loader**,
- połączenie Forge z systemem zapisu postępu,
- obsługę automatycznego wyboru trybu,
- testowanie i walidację poprawności działania systemu,

---


## Samoocena:

| Obszar | Ocena (1–5) | Komentarz |
|--------|--------------|-----------|
| Zaangażowanie | 5 | Poświęcałem każda wolna chwilę, aby zrealizować powierzone taski. |
| Wkład merytoryczny | 4 | Udało się wykonać questa tutorialowego, ale nie udała się synchronizacja Forga z ekwipunkiem. |
| Komunikacja | 5 | W tym tygodniu komunikacja była regularnie przeprowadzana. |
| Terminowość | 3 | W tym tygodniu ostatni zrobiłem commita o 23 we wt. |

## Refleksja:
Niestety nie udało się zrobić synchronzacji z eq, ale narazie to nie ma zbytnio dużego znaczenia. Natomiast największe znaczenie ma teraz naprawienie wszystkich błędów i połączenie wsyzstkego co zespoł wytowrzył, aby stworzyć grywalną gierkę.
