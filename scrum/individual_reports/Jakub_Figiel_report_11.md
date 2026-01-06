# Raport indywidualny – Sprint 11

**Imię i nazwisko:** Jakub Figiel  
**Zespół:** Loopers  
**Numer sprintu:** 11  
**Okres:** 2025-12-17 – 2026-01-07  

## Zakres moich działań:
1. Modyfikacja EnemyEncounter.cs - zmiana podejścia z walk 1v1 na 3v3.
2. Utworznie skryptu PlayerParty.cs dającego możliwość ustawiana stworków gracza z overworldu i przenoszenie ich do sceny walki wraz ze statystykami.
3. Modyfikaja PokemonData.cs tak, aby każdy stworek miał dodatkowo swoją manę i animator. 
4. Utworzenie obiektów 6 startowych stworków wraz ze statystykami.
5. Utworzenie skryptu BattleInstructionInterpreter w celu parsowania formatu instrukcji z edytora do interpretera.
6. Utworzenie skryptu BattleEditorController.cs - do edytora w widoku walki, tak aby każdy stworek miał swój własny edytor (3 edytory) i swój kod.
7. Modyfikacja i refaktoryzacja skryptów BattleUI.cs oraz BattleManager.cs w celu zapewnienia synchronizacji UI z stanem logicznym walki.
8. Dodanie soundtracku do widoku walki.
9. Modyfikacje i nowe funkcjonalności UI: 
-dodanie nowej czcionki  
-dodanie customowych pasków many i HP  
-dodanie customowych assetów edytora, buttonów oraz statystyk stworków  
-zapewnienie responsywności UI  
-dodanie tła   
-dodanie animacji Idle oraz animacji ataku dla 6 podstawowych stworków  
-dodanie ekranu końcowego po wygranej/przegranej oraz powrotu do sceny overworld  

## Wkład w projekt:
Mój wkład w projekcie koncentrował się głównie na rozwoju systemu walki, logiki gry oraz interfejsu użytkownika. Zrealizowałem kluczową zmianę architekturalną, polegającą na przejściu z modelu walk 1v1 na 3v3, co wymagało modyfikacji istniejących skryptów oraz dostosowania logiki zarządzania stworkami po obu stronach konfliktu.
Dodatkowo przeprowadziłem refaktoryzację kluczowych skryptów zarządzających walką i interfejsem (BattleManager.cs, BattleUI.cs), zapewniając ich poprawną synchronizację ze stanem logicznym rozgrywki.
Równolegle odpowiadałem za znaczną część warstwy wizualnej walki, obejmującą nowe elementy UI, animacje stworków, oprawę graficzną oraz integrację dźwięku i ekranu końcowego.

## Samoocena:

| Obszar | Ocena (1–5) | Komentarz |
|--------|--------------|-----------|
| Zaangażowanie | 5 | Aktywnie realizowałem wszystkie przypisane zadania, obejmujące zarówno programowanie, jak i elementy graficzne oraz UI.  |
| Wkład merytoryczny | 5 | Zaimplementowałem kluczowe systemy gry, w tym logikę walk 3v3, edytor kodu oraz interpretację instrukcji, które stanowią fundament mechaniki rozgrywki.  |
| Komunikacja | 5 | Na bieżąco informowałem zespół o postępach prac i konsultowałem istotne zmiany techniczne, kilkukrotnie na przestrzeni 2 tygodni zgadywałem się z Mateuszem, jako że obaj byliśmy odpowiedzialni za walkę. |
| Terminowość | 5 | Wszystkie zadania zrealizowałem w terminie. Pracowałem regularnie i większość kluczowych funkcjonalności wykonałem z wyprzedzeniem. |

## Refleksja:
Sprint 11 pozwolił mi w znacznym stopniu rozwinąć zarówno część logiczną, jak i wizualną systemu walki. Największym wyzwaniem było połączenie edytora kodu, interpretera instrukcji oraz synchronizacji UI z przebiegiem walki, jednak finalnie udało się osiągnąć spójne i stabilne rozwiązanie.
Uważam ten sprint za bardzo efektywny - zrealizowane zadania stworzyły solidną podstawę pod dalszy rozwój projektu, szczególnie w kontekście rozbudowy mechanik walki oraz dalszego balansu rozgrywki.
