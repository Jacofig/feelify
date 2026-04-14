# Raport indywidualny – Sprint 20

**Imię i nazwisko:** Mateusz Kopala   
**Zespół:** Loopers  
**Numer sprintu:** 20   
**Okres:** 2026-04-08 – 2026-04-15  

## Zakres moich działań:

Naprawa bugów. Przygotowanie się do ewentualnych pytań podczas rabita.

---

## Wkład w projekt

W ramach prac naprawczych w projekcie wykonano następujące zmiany:

- Skorygowano pozycje colliderów na mapie, co umożliwiło poprawne poruszanie się postaci po ścieżkach.
- Naprawiono mechanikę wejścia do forga, przywracając jego prawidłowe działanie.
- Zaimplementowano lokalny system dialogów (własny DialogueManager dla forga), eliminując konflikty z globalnym systemem dialogowym.
- Rozwiązano problem konfliktu podczas ładowania scen (LoadSceneMode.Additive), co zapewniło poprawne przełączanie scen bez błędów UI i EventSystem.

Dodatkowo przeanalizowano oraz uporządkowano główny pipeline systemu (parser → interpreter → command handler → wykonanie logiki w scenie), stanowiący kluczowy element działania mechanik gry.

## Samoocena:

| Obszar | Ocena (1–5) | Komentarz |
|--------|--------------|-----------|
| Zaangażowanie | 5 | Zlokalizowanie błędów w kodzie oraz infrastrukturze powiązanej z innymi modułami kolegi z zespołu było czasochłonne i momentami frustrujące. |
| Wkład merytoryczny | 5 | Naprawiono wszystkie bugi (znalezione na ten moment) powiązane z moimi modułami. |
| Komunikacja | 3.5 | Jeden z moich bugów był spowodowany nowym featurem kolegi z zespołu, o którym nie miałem pojęcia. |
| Terminowość | 3.5 | Task skońoczny w nocy przed upływem terminu, Nie spodziewałem się, że task okaże się tak czasochłonny. |


## Refleksja:
Znalezienie przyczyny błędu i zrozumienie powiązań miedzy infrastrukturami/modułami zajmuje 90% czasu naprawy bugów.
