#  Dokumentacja Skryptów

---

## 1. **PlayerMovement.cs — Sterowanie ruchem postaci**

**Opis:**  
Skrypt odpowiada za obsługę ruchu gracza w oparciu o `Rigidbody2D` oraz Unity Input System.  
Odczytuje wejście z klawiatury, normalizuje kierunek ruchu i porusza obiektem w `FixedUpdate()`.  
Dodatkowo umożliwia blokowanie ruchu poprzez flagę `canMove`.

###  Kluczowe elementy działania
- Odczyt kierunków **W, A, S, D** z Unity Input System.
- Normalizacja wektora ruchu, aby uniknąć szybszego poruszania się po skosie.
- Fizyczny ruch postaci z użyciem `Rigidbody2D.MovePosition`.
- System blokowania ruchu (`canMove = false`).

###  Najważniejsze funkcje
- **Awake()** – pobiera referencję do `Rigidbody2D`.
- **Update()** – obsługuje wejście z klawiatury oraz blokowanie ruchu.
- **FixedUpdate()** – wykonuje przesunięcie obiektu.

---

## 2. **ChestInteract.cs — Interakcja ze skrzynką**

**Opis:**  
Skrypt obsługuje interakcję gracza z obiektem skrzyni.  
Po podejściu wyświetla się tekst interakcji, a po naciśnięciu **E** skrzynia zostaje otwarta, stary obiekt jest niszczony, a w jego miejscu powstaje prefab otwartej skrzyni.

###  Kluczowe elementy działania
- Wykrywanie obecności gracza poprzez `OnTriggerEnter2D` i `OnTriggerExit2D`.
- Wyświetlanie lub ukrywanie tekstu interakcji.
- Obsługa klawisza **E**, gdy gracz jest w zasięgu.
- Tworzenie otwartego modelu skrzyni (`Instantiate`) oraz usuwanie zamkniętej (`Destroy`).

###  Najważniejsze funkcje
- **Update()** – nasłuch klawisza **E**.
- **OpenChest()** – tworzy otwartą skrzynię i niszczy starą.
- **OnTriggerEnter2D()** – wykrywa wejście gracza w zasięg interakcji.
- **OnTriggerExit2D()** – ukrywa UI po odejściu gracza.

---

## 3. **StatueInteractor.cs — Interakcja z NPC / otwieranie edytora**

**Opis:**  
Skrypt pozwala graczowi wchodzić w interakcję z posągiem/NPC, który otwiera edytor tekstu.  
Po naciśnięciu **E** w pobliżu aktywowany jest interfejs edycji tekstu, a ruch gracza zostaje zablokowany.

###  Kluczowe elementy działania
- Wykrywanie obecności gracza w zasięgu interakcji.
- Wyświetlanie komunikatu o możliwości użycia **E**.
- Otwieranie edytora (`EditorController.OpenEditor()`).
- Automatyczne ukrywanie tekstu.

###  Najważniejsze funkcje
- **Update()** – sprawdza klawisz **E** i otwiera edytor.
- **OnTriggerEnter2D()** – włącza komunikat o interakcji.
- **OnTriggerExit2D()** – ukrywa komunikat.

---

## 4. **EditorController.cs — Obsługa edytora kodu**

**Opis:**  
Skrypt zarządza oknem edytora tekstu, w którym gracz może wpisywać kod.  
Odpowiada za otwieranie i zamykanie edytora, blokowanie ruchu gracza oraz wywoływanie parsera (`SimpleParser`) w celu sprawdzenia poprawności kodu.

###  Kluczowe elementy działania
- Włącza i wyłącza UI edytora.
- Automatycznie blokuje i odblokowuje ruch gracza.
- Aktywuje fokus w polu tekstowym po otwarciu.
- Wywołuje parser kodu i obsługuje komunikaty o błędach.
- Przechowuje stan edytora (`isOpen`).

###  Najważniejsze funkcje
- **OpenEditor()**  
  Aktywuje okno edytora, ustawia fokus na `TMP_InputField`, blokuje ruch gracza.

- **CloseEditor()**  
  Ukrywa edytor i przywraca możliwość poruszania się.

- **SubmitCode()**  
  Odczytuje tekst z `inputField`, przekazuje go do `SimpleParser`  
  i wyświetla w konsoli listę instrukcji lub błąd parsowania.

- **IsOpen()**  
  Zwraca informację, czy edytor jest aktualnie otwarty.

- **Start()**  
  Ukrywa edytor przy starcie gry.

---

## 5. **GameCommandLibrary.cs — Rejestr komend gry**

**Opis:**  
Zawiera słownik dostępnych komend gry, które mogą być wywoływane z interpretera (np. z kodu wpisanego przez gracza).  
Każda komenda generuje obiekt `ParsedInstruction`.

###  Kluczowe elementy działania
- Przechowywanie listy komend (`attack`, `block`, …).
- Tworzenie obiektu instrukcji wraz z argumentami i numerem linii.
- Funkcja `IsGameCommand()` sprawdza, czy komenda istnieje.

###  Przykład komendy
- `attack()`
- `block()`

---

## 6. **ParsedInstruction.cs & InstructionType.cs — Model instrukcji**

**Opis:**  
Reprezentuje pojedynczą instrukcję z parsowanego kodu.  
Obsługuje zarówno komendy gry, jak i konstrukcje sterujące (`if`, `while`, `for`, `def`).

###  Skład obiektu
- `Type` – typ instrukcji (wyliczenie `InstructionType`).
- `Name` – nazwa komendy.
- `Arguments` – lista argumentów.
- `Condition` – warunek (dla `if`, `while`).
- `Children` – lista instrukcji wewnątrz bloku.
- `IndentLevel` – poziom wcięcia (4 spacje = 1 poziom).

---

## 7. **PythonKeyWordLibrary.cs — Lista słów kluczowych**

**Opis:**  
Zbiór słów kluczowych stylizowanych na Python (`if`, `while`, `for`, `def`, `else`).  
Używany podczas parsowania do rozpoznawania bloków.

###  Zawiera:
- `IF`
- `WHILE`
- `FOR`
- `DEF`
- `ELSE`

---

## 8. **SimpleParser.cs — Parser kodu**

**Opis:**  
Parser liniowy zamienia kod użytkownika na drzewo instrukcji (`ParsedInstruction`).  
Obsługuje wcięcia (4 spacje = poziom bloku), komendy gry oraz konstrukcje `if:`.

###  Kluczowe elementy działania
- Walidacja wcięć (musi być wielokrotnością 4 spacji).
- Rozpoznawanie bloków (`if` → instrukcje podrzędne).
- Parsowanie komend: `attack()`, `block()`, itp.
- Generowanie błędów z informacją o numerze linii.

###  Najważniejsze funkcje
- **Parse()** – dzieli kod na linie i buduje strukturę blokową.
- **ParseSingleLine()** – analizuje pojedynczą instrukcję.
- **ExtractName()** – analizuje nazwę komendy.
- **ExtractArguments()** – pobiera argumenty z nawiasów.

---

## 9. **TabToSpaces.cs — Zamiana TAB na spacje**

**Opis:**  
Skrypt podłączony do `TMP_InputField` w edytorze tekstu.  
Przechwytuje naciśnięcie klawisza TAB i wstawia zamiast niego 4 spacje, zgodnie z wymogiem parsera.

###  Kluczowe elementy działania
- Przechwytywanie klawisza TAB.
- Wstawianie w miejsce kursora czterech spacji.
- Aktualizacja pozycji kursora (`stringPosition`, `caretPosition`).

---

** Koniec dokumentacji**
```md
