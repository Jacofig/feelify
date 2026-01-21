# Raport indywidualny – Sprint tygodniowy 14

**Imię i nazwisko:**  Mateusz Kopala  
**Zespół:** Loopers  
**Numer sprintu tygodniowego:** 14  
**Okres:** 2025-01-14 – 2026-01-21  

## Zakres moich działań

W trakcie bieżącego sprintu moim głównym zadaniem było **przygotowanie interfejsu użytkownika dla systemu Forge**, jego **synchronizacja z systemem receptur** oraz **podstawowa obsługa logiki**, która weryfikuje, czy gracz wykonuje wymagane akcje w poprawnej kolejności. Prace obejmowały również integrację UI z warstwą wizualną opartą o **własne grafiki** oraz obsługę zwracania wyniku procesu Forge w formie graficznej.

---

## Interfejs użytkownika Forge i integracja z logiką

W ramach sprintu zaprojektowałem i zaimplementowałem **UI Forge**, które stanowi wizualną reprezentację procesu kucia oraz bezpośredni punkt interakcji gracza z mechaniką Forge.

### UI Forge z własnymi grafikami

Interfejs Forge został oparty na **autorskich elementach graficznych**, przygotowanych specjalnie na potrzeby tej mechaniki. Zastosowano:
- dedykowane tła Forge,
- mini tła oraz panele informacyjne,
- tekstury nagłówków i sekcji UI.

Dzięki temu interfejs jest spójny wizualnie z resztą gry i czytelnie prezentuje stan procesu kucia. Niestety jak widać pierwszy raz robiłem własne grafiki i wymagają sporej poprawki.

---

### Synchronizacja UI z systemem receptur

UI Forge jest bezpośrednio połączone z systemem receptur. Po wybraniu receptury:
- interfejs automatycznie **wczytuje jej wymagania**,
- prezentowane są informacje o:
  - wymaganych składnikach,
  - liczbie wymaganych akcji `hit()`,

Pozwala to graczowi na bieżąco kontrolować, jakie warunki muszą zostać spełnione, aby ukończyć proces Forge.

---

### Podstawowa obsługa logiki i walidacja akcji

Interfejs Forge został połączony z podstawową warstwą logiki, która:
- reaguje na akcje wykonywane przez gracza (`heat()`, `hit()`, `cast()`),
- sprawdza, czy akcje:
  - są wykonywane w poprawnej kolejności,
  - spełniają wymagania receptury,
  - uwzględniają aktualny stan procesu (np. temperatura metalu).

System blokuje niepoprawne akcje (np. uderzanie zimnego metalu) oraz na bieżąco aktualizuje stan UI.

---

### Zwracanie wyniku procesu w formie graficznej

Po zakończeniu procesu Forge system:
- weryfikuje, czy wszystkie wymagania receptury zostały spełnione,
- zwraca wynik procesu w postaci **obrazu**:
  - wykutego przedmiotu w przypadku sukcesu,
  - komunikatu wizualnego w przypadku niepowodzenia.

UI automatycznie aktualizuje się, prezentując efekt końcowy bez dodatkowej interakcji gracza.

---

## Wkład w projekt

Mój wkład w tym sprincie obejmował:

- zaprojektowanie i implementację **interfejsu użytkownika Forge**,
- przygotowanie **własnych grafik** wykorzystywanych w UI Forge,
- integrację UI z **systemem receptur**,
- implementację **podstawowej obsługi logiki Forge w UI**,
- walidację poprawności wykonywanych akcji (`heat()`, `hit()`, `cast()`),
- obsługę **zwracania wyniku procesu Forge w formie graficznej**.

## Samoocena:

| Obszar | Ocena (1–5) | Komentarz |
|--------|--------------|-----------|
| Zaangażowanie | 5 | Z tygodnia na tydzień projekt staje się bardziej czasochłonny i wymaga wiecęj zangażowania. Starałem poświęcać każda wolna chwilę, aby spełnić taski z bacloga.|
| Wkład merytoryczny | 3.5 | Choć poświęciłem wiele czasu to wynik końcowy nie zachwyca. UI jest responsywne, ale moje grafiki nie wyglądają za dobrze. UI pochłonęło tyle czasu, że nie udało się nawet zacząć questa tutorialowego. |
| Komunikacja | 4 | Nie komunikowałem się zbyt często w tym tygodniu, ponieważ moje taski nie były powiązane z zadaniami innych członków drużyny. Jedynie raportowałem postępy pracy.|
| Terminowość | 3.5 | UI forga pushnąłem dopiero we wt, chociaż prace zacząłem już w piątek. Jak wspominałem questa tutorialwoego nie zdążyłem. |

## Refleksja:
Pomimo dużego nakładu pracy w grafiki i UI wynik końcowy nie jest za dobry, co pokazuję, że nie nadaję się do pracy z grafika i interfejsem, ale stale będę się doszkalał i poprawiał, żeby finalnie UI w miare dobrze się prezentowało. Dobrze natomiast wyszła synchronizacja receptur z UI i walidacja podstawowych akcji. Następnym krokiem bd synchronizacja z ekwipunkiem gracza.
