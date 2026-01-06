# Raport indywidualny – Sprint tygodniowy 11

**Imię i nazwisko:**  Mateusz Kopala  
**Zespół:** Loopers  
**Numer sprintu tygodniowego:** 11  
**Okres:** 2025-12-17 – 2026-01-07

## Zakres moich działań

W trakcie sprintu zajmowałem się rozwojem oraz refaktoryzacją kluczowych elementów systemu rozgrywki, ze szczególnym naciskiem na walkę, komunikację interpretera oraz architekturę umożliwiającą dalszą rozbudowę projektu.

1. **Implementacja obsługi bloków `attack` i `block` oraz systemu obrażeń**
   - Zaprojektowałem i zaimplementowałem mechanizm ataku, umożliwiający interakcję kodu gracza z przeciwnikiem.
   - Komenda `attack` powoduje zadanie obrażeń przeciwnikowi i aktualizację jego punktów życia.
   - Zaimplementowałem komendę `block`, która pozwala graczowi zredukować lub całkowicie zablokować obrażenia otrzymywane w danej turze.
   - System obrażeń uwzględnia stan defensywny gracza, co pozwala na dalszą rozbudowę mechanik obronnych.
   - Logika ataku i obrony została wydzielona w sposób umożliwiający dalsze rozszerzenia (np. różne typy ataków, tarcze, efekty statusowe).

2. **Stworzenie turowego systemu walki**
   - Zaprojektowałem system walki oparty na turach, w którym gracz i przeciwnik wykonują akcje naprzemiennie.
   - System rozdziela fazę decyzji gracza od reakcji przeciwnika, co upraszcza kontrolę przebiegu walki.
   - Architektura walki została przygotowana pod przyszłe mechaniki (np. umiejętności, przedmioty, wielu przeciwników).

3. **Refaktoryzacja komunikacji interpretera z systemem walki**
   - Przeprowadziłem refaktoryzację komunikacji pomiędzy interpreterem kodu gracza a systemem walki.
   - Rozdzieliłem obsługę komend eksploracyjnych i komend bojowych na osobne komponenty.
   - Interpreter jest w stanie rozróżniać kontekst (eksploracja vs walka), co zwiększa czytelność i stabilność kodu.

4. **Projekt i implementacja AI przeciwnika**
   - Stworzyłem prosty, ale rozszerzalny system AI przeciwnika.
   - AI działa na bazie gotowych scenariuszy zachowań, które można łatwo podmieniać lub rozbudowywać.
   - Obecna implementacja pozwala na szybkie dodawanie nowych typów przeciwników bez ingerencji w logikę walki.

5. **Projektowanie systemu z myślą o dalszym rozwoju**
   - Całość rozwiązań była tworzona z naciskiem na modularność i możliwość przyszłych inkrementów.
   - Klasy i komponenty zostały celowo rozdzielone, aby ułatwić testowanie, refaktoryzację oraz rozbudowę systemu.

## Zakres niezrealizowany w tym sprincie

Poniższe elementy nie zostały zrealizowane w trakcie sprintu, ponieważ miały **niższy priorytet** w porównaniu do prac nad architekturą systemu walki oraz refaktoryzacją kluczowych komponentów:

- interakcja z jednym NPC,
- wywołanie menu dialogowego z NPC,
- implementacja GUI ekwipunku (inventory),
- implementacja karty gracza z jego statystykami.

Zadania te zostały świadomie odłożone, aby skupić się na stabilnych fundamentach systemu walki oraz elementach krytycznych dla dalszego rozwoju projektu.

## Wkład w projekt

Mój wkład w projekt miał charakter zarówno **implementacyjny**, jak i **architektoniczny**.  
Byłem odpowiedzialny za:

- zaprojektowanie i implementację systemu walki (atak, blok, obrażenia),
- integrację walki z interpreterem kodu gracza,
- refaktoryzację istniejącej komunikacji pomiędzy komponentami,
- stworzenie podstaw AI przeciwników,
- przygotowanie architektury pod dalszy rozwój gry.

Dostarczone przeze mnie rozwiązania stanowią fundament dla kolejnych funkcjonalności związanych z rozgrywką.

## Samoocena

| Obszar | Ocena (1–5) | Komentarz |
|------|-------------|-----------|
| Zaangażowanie | 4 | Wymagana była gruntowna refaktoryzacja interpretera oraz zaprojektowanie nowej architektury walki, co było czasochłonne. Niestety nie zrealizowałem mniej priorytetowych tasków.|
| Wkład merytoryczny | 5 | Zaprojektowanie systemu walki, AI przeciwnika oraz komunikacji pomiędzy modułami. |
| Komunikacja | 4 | Regularne raportowanie postępów z członkiem zespołu odpowiedzialnym za GUI systemu walki. Brak regularnej komunikacji z innymi członkami zespołu. |
| Terminowość | 4 | Core machanika (system walki) został zrealizowany w terminie. Niestety nie udało się wykonać reszty mniej priorytetowych zadań.|

## Refleksja

Na początku tworzenie wielu klas, nadmiarowych (pozornie) abstrakcji oraz rozdzielenie komunikacji na osobne komponenty może sprawiać wrażenie **przerostu formy nad treścią**. Jednak wraz z rozrostem projektu okazało się, że takie podejście znacząco **upraszcza rozwój, testowanie i refaktoryzację** systemu.

Obecny stan projektu jasno pokazuje, że tzw. *vibe coding* przestaje być wystarczający przy większej skali kodu. Modularność i świadome projektowanie architektury pozwalają na szybszą realizację kolejnych funkcjonalności oraz ograniczają chaos w kodzie w dłuższej perspektywie.
