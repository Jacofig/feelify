# Raport indywidualny – Sprint tygodniowy 13

**Imię i nazwisko:**  Mateusz Kopala  
**Zespół:** Loopers  
**Numer sprintu tygodniowego:** 13  
**Okres:** 2025-01-07 – 2026-01-014  

## Zakres moich działań

W trakcie bieżącego sprintu skupiłem się na **implementacji kompletnego systemu Forge**, mechanice **łapania Pokémonów wraz z automatycznym zarządzaniem Pokédexem**, a także na **przygotowaniu pierwszych elementów interfejsu użytkownika oraz warstwy graficznej**. Prace miały charakter zarówno **implementacyjny**, jak i **architektoniczny**, z naciskiem na poprawność logiki oraz testowalność kodu.

---

## 1. Implementacja systemu Forge (mechanika kucia)

W ramach sprintu zaprojektowałem i zaimplementowałem **system Forge**, który symuluje proces kucia przedmiotów w sposób aktywny i zależny od działań gracza, a nie jako jednorazową akcję.

### Zasady działania Forge

System Forge opiera się na **sekwencji komend wykonywanych przez gracza**, które odpowiadają realnym etapom procesu kucia metalu. Kluczowe komendy dostępne w systemie to:

- `heat()` – podgrzewanie metalu w piecu,
- `hit()` – uderzanie rozgrzanego metalu młotem,
- `cast()` – finalizacja procesu i odlanie / uformowanie przedmiotu.

Proces kucia **nie może zostać wykonany na zimnym metalu**. Gracz jest zobowiązany do:
- regularnego podgrzewania materiału,
- utrzymywania odpowiedniej temperatury w trakcie całego procesu,
- wykonywania akcji w poprawnej kolejności.

Jeżeli metal ostygnie:
- dalsze komendy `hit()` nie przynoszą efektu,
- proces kucia zostaje wstrzymany do momentu ponownego użycia `heat()`.

Dzięki temu Forge nie jest „jednym kliknięciem”, lecz **aktywną mechaniką wymagającą ciągłej interakcji gracza**.

---

### System receptur

Forge wykorzystuje **system receptur**, który definiuje, co i w jaki sposób może zostać wykute.

Każda receptura zawiera:
- listę wymaganych składników,
- minimalną liczbę akcji `hit()` potrzebnych do ukończenia kucia,
- warunki temperaturowe (konieczność podgrzewania w trakcie procesu),
- reguły kończące proces (`cast()` jako akcja finalizująca).

Receptury są przechowywane w **centralnej bazie receptur**, co umożliwia:
- łatwe dodawanie nowych przedmiotów bez modyfikowania logiki Forge,
- balansowanie gry poprzez zmianę liczby wymaganych akcji,
- kontrolę stopnia złożoności procesu kucia.

---

### Przebieg procesu kucia

1. Gracz inicjuje proces Forge z wybraną recepturą.
2. Materiał startuje w stanie zimnym.
3. Gracz musi użyć `heat()` w celu podgrzania metalu.
4. Po osiągnięciu odpowiedniego stanu możliwe jest wykonywanie akcji `hit()`.
5. Metal stopniowo traci temperaturę, co wymusza ponowne użycie `heat()`.
6. Po wykonaniu wymaganej liczby poprawnych uderzeń gracz używa `cast()`,
   co kończy proces i tworzy przedmiot.

System na bieżąco weryfikuje:
- kolejność wykonywanych komend,
- aktualny stan temperatury,
- zgodność z wymaganiami receptury.

---

### Architektura i testowalność

- System Forge został zaimplementowany jako **niezależny moduł logiki gry**.
- Logika temperatury, licznik akcji oraz obsługa receptur są rozdzielone na osobne komponenty.
- Dla kluczowych elementów Forge przygotowano **testy jednostkowe**, obejmujące:
  - poprawne przejście pełnego procesu kucia,
  - próby uderzeń na zimnym metalu,
  - niepoprawną kolejność komend,
  - brak wymaganych składników.

Testy jednostkowe potwierdzają poprawność mechaniki oraz zabezpieczają system przed regresjami przy dalszej rozbudowie.

---

## 2. Mechanika łapania Pokémonów

W ramach sprintu zaimplementowałem **turową mechanikę łapania Pokémonów**, ściśle zintegrowaną z systemem walki oraz Pokédexem.

### Zasady działania

- Gracz może użyć komendy `catch()` **maksymalnie raz na turę**.
- Mechanika łapania opiera się na **szansach procentowych**, zależnych od aktualnego stanu Pokémona.

Szansa powodzenia obliczana jest dynamicznie:
- im **niższy procent punktów życia (HP)** Pokémona,
- tym **większe prawdopodobieństwo jego złapania**.

Dzięki temu system:
- nagradza strategiczne osłabianie przeciwnika,
- zapobiega łatwemu łapaniu Pokémonów na początku walki.

---

### Integracja z Pokédexem

W przypadku udanej próby złapania:
- Pokémon jest **automatycznie dodawany do kolekcji gracza**,
- system sprawdza, czy dany gatunek istnieje już w Pokédexie,
- jeśli Pokémon jest nowy:
  - tworzony jest nowy wpis w Pokédexie,
- jeśli Pokémon był już wcześniej zarejestrowany:
  - Pokédex nie tworzy duplikatu wpisu.

Cała aktualizacja Pokédexu odbywa się **automatycznie**, bez dodatkowych akcji ze strony gracza.

---

## 3. Generowanie UI Forge

W ramach sprintu zaprojektowałem **pierwszą wersję interfejsu użytkownika dla Forge**.

### Zakres UI
- UI umożliwia:
  - interakcję z systemem Forge,
  - prezentację aktualnego stanu procesu „kucia”,
  - czytelne oddzielenie warstwy wizualnej od logiki.
- Interfejs został zaprojektowany w sposób:
  - prosty,
  - czytelny,
  - gotowy do dalszego rozwijania wraz z kolejnymi funkcjonalnościami.

---

## 4. Przygotowanie pierwszych grafik

W tym sprincie wygenerowałem również **pierwsze elementy graficzne**, które stanowią fundament spójnej warstwy wizualnej gry.

### Wykonane grafiki
- **Tło główne** – wykorzystywane w kluczowych widokach aplikacji.
- **Mini tło** – wersja uproszczona, przeznaczona do paneli i okien pomocniczych.
- **Tekstury nagłówków** – graficzne elementy wykorzystywane w UI (np. tytuły, sekcje).

---

## Wkład w projekt

Mój wkład w tym sprincie obejmował:

- zaprojektowanie i implementację **systemu Forge**,
- przygotowanie **testów jednostkowych** dla kluczowej logiki Forge,
- implementację **mechaniki łapania Pokémonów**,
- pełną integrację tej mechaniki z **automatycznym Pokédexem**,
- wygenerowanie **UI Forge**,
- stworzenie **pierwszych grafik (tła, mini tła, tekstury nagłówków)**.

Prace te stanowią solidny fundament pod dalszy rozwój zarówno mechanik gry, jak i warstwy wizualnej.
