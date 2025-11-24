# Dokumentacja skryptów

---

## 1. PlayerMovement.cs — Skrypt ruchu postaci

**Opis:**  
Skrypt odpowiada za sterowanie ruchem postaci gracza. Pobiera dane z klawiatury i przesuwa obiekt w świecie gry z wykorzystaniem `Rigidbody2D`.  
Posiada również system blokowania ruchu (np. podczas korzystania z okna dialogowego).

**Kluczowe elementy działania:**
- Odczyt klawiszy **W, A, S, D** przy użyciu Unity Input System.
- Normalizacja wektora ruchu, aby uniknąć przyspieszenia przy ruchu po skosie.
- Poruszanie postaci w `FixedUpdate()` przy pomocy `Rigidbody2D.MovePosition`.
- Flaga `canMove` umożliwia włączanie i wyłączanie ruchu.

**Najważniejsze funkcje:**
- `Awake()` – pobiera referencję do Rigidbody2D.
- `Update()` – zbiera input i blokuje ruch, jeśli `canMove == false`.
- `FixedUpdate()` – wykonuje fizyczny ruch postaci.

---

## 2. ChestInteract.cs — Interakcja ze skrzynką

**Opis:**  
Skrypt obsługuje interakcję gracza z obiektem typu skrzynka.  
Gdy gracz podejdzie, pojawia się komunikat. Po wciśnięciu **E** skrzynka zostaje otwarta — stary obiekt jest niszczony, a w jego miejscu tworzony jest prefab otwartej skrzyni.

**Kluczowe elementy działania:**
- Wykrywanie gracza przy pomocy triggera (`OnTriggerEnter2D`, `OnTriggerExit2D`).
- Wyświetlanie lub ukrywanie tekstu interakcji.
- Obsługa wciśnięcia klawisza **E**.
- Tworzenie otwartej wersji skrzyni poprzez `Instantiate()` i niszczenie starej.

**Najważniejsze funkcje:**
- `Update()` – nasłuchuje klawisza E, gdy gracz stoi obok skrzyni.
- `OpenChest()` – tworzy otwartą wersję skrzyni i usuwa starą.
- `OnTriggerEnter2D()` – wykrywa podejście gracza i aktywuje tekst interakcji.
- `OnTriggerExit2D()` – wykrywa odejście gracza i ukrywa tekst.

---

## 3. npc.cs — Edytor tekstu i zapis do pliku

**Opis:**  
Skrypt umożliwia interakcję z NPC, który otwiera okno edytora tekstowego.  
Gracz może wpisać tekst (np. kod), a następnie zapisać go do pliku `.txt`.  
W czasie edycji ruch postaci jest blokowany.

**Kluczowe elementy działania:**
- Wyświetlanie komunikatu o interakcji, gdy gracz znajduje się w pobliżu NPC.
- Otwieranie edytora po wciśnięciu klawisza **E**.
- Blokowanie ruchu postaci (`playerMovement.canMove = false`).
- Zapisywanie treści `TMP_InputField` do pliku `user_code.txt`.
- Automatyczne zamykanie edytora, gdy gracz odejdzie od NPC.

**Najważniejsze funkcje:**
- `OpenEditor()` – aktywuje edytor, ustawia input field oraz blokuje ruch gracza.
- `SubmitText()` – zapisuje kod do folderu `Assets/Input/` jako `user_code.txt`.
- `ExitEditor()` – zamyka edytor i przywraca możliwość ruchu.
- `OnTriggerEnter2D()` – pokazuje tekst interakcji po podejściu gracza.
- `OnTriggerExit2D()` – ukrywa UI i zamyka edytor, jeśli był otwarty.
- `Update()` – reaguje na wciśnięcie klawisza E.

