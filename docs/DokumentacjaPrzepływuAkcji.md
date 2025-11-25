# Dokumentacja przepływu akcji

## 1. PlayerMovement (Ruch gracza)

**Składniki:**
- `Rigidbody2D`
- Keyboard Input (WASD) — Unity Input System
- `canMove` – flaga blokująca ruch podczas edycji kodu

**Przepływ akcji:**
1. `Update()`:
    - Jeśli `canMove == false`, ustawia `moveInput = Vector2.zero` i blokuje ruch.
    - W przeciwnym razie:
        - Odczytuje wejścia z klawiatury (W/A/S/D).
        - Tworzy wektor `moveInput`.
        - Normalizuje go, aby uniknąć szybszego ruchu po skosie.
2. `FixedUpdate()`:
    - Oblicza nową pozycję:  
      `newPos = rb.position + moveInput * speed * Time.fixedDeltaTime`.
    - Aktualizuje pozycję gracza przez `rb.MovePosition(newPos)`.

**Uwagi:**
- `canMove` jest ustawiane na `false` przez `EditorController`, by zablokować ruch w trakcie edycji tekstu.

---

## 2. ChestInteract (Interakcja ze skrzynką)

**Składniki:**
- `openChestPrefab` – prefab otwartej skrzyni
- `interactText` – UI informujące o możliwości interakcji
- `playerNear` – flaga wykrycia gracza w triggerze

**Przepływ akcji:**
1. `OnTriggerEnter2D(Collider2D collision)`:
    - Jeśli `collision` to gracz:
        - `playerNear = true`
        - Pokazuje `interactText`.
2. `OnTriggerExit2D(Collider2D collision)`:
    - Jeśli `collision` to gracz:
        - `playerNear = false`
        - Ukrywa `interactText`.
3. `Update()`:
    - Jeśli `playerNear == true` i wciśnięto `E`, wywołuje `OpenChest()`.
4. `OpenChest()`:
    - Tworzy obiekt `openChestPrefab` w pozycji skrzynki.
    - Ukrywa `interactText`.
    - Usuwa obecną skrzynkę (`Destroy(gameObject)`).

---

## 3. StatueInteractor + Editor Interaction (Interakcja z posągiem i edytorem kodu)

**Składniki:**
- `interactText` – informacja UI
- `editorController` – referencja do systemu edytora
- `playerNear` – flaga obecności gracza

**Przepływ akcji:**
1. `OnTriggerEnter2D(Collider2D col)`:
    - Jeśli `col` to gracz:
        - `playerNear = true`
        - Pokazuje `interactText`.
2. `OnTriggerExit2D(Collider2D col)`:
    - Jeśli `col` to gracz:
        - `playerNear = false`
        - Ukrywa `interactText`.
3. `Update()`:
    - Jeśli `playerNear == true` i wciśnięto `E`:
        - Jeśli edytor jest zamknięty (`!editorController.IsOpen()`):
            - Wywołuje `editorController.OpenEditor()`.
            - Ukrywa `interactText`.

---

## 4. EditorController (Obsługa edytora kodu)

**Składniki:**
- `TMP_InputField inputField`
- `PlayerMovement playerMovement`
- `isOpen` – flaga stanu edytora

**Przepływ akcji:**
1. `OpenEditor()`:
    - Włącza canvas edytora.
    - Aktywuje `inputField` i ustawia focus.
    - `isOpen = true`
    - Blokuje ruch gracza: `playerMovement.canMove = false`.
2. `CloseEditor()`:
    - Wyłącza canvas edytora.
    - `isOpen = false`
    - Odblokowuje ruch gracza: `playerMovement.canMove = true`.
3. `SubmitCode()`:
    - Pobiera tekst z `inputField`.
    - Tworzy obiekt `SimpleParser`.
    - Próbuje sparsować kod:
        - W przypadku sukcesu wypisuje listę instrukcji.
        - W przypadku błędu wyświetla `Debug.LogError`.
4. `IsOpen()`:
    - Zwraca, czy edytor jest aktualnie otwarty.
5. `Start()`:
    - Edytor domyślnie jest ukryty (`SetActive(false)`).

---

## 5. SimpleParser (Parser kodu użytkownika)

**Zadanie:**
- Analizuje tekst wpisany przez użytkownika.
- Tworzy strukturę instrukcji (`ParsedInstruction`).
- Obsługuje:
    - `if` bloki
    - komendy gry (`attack()`, `block()`)
    - wcięcia (indentation)
- Tworzy strukturę drzewiastą (bloki wewnątrz bloków).

**Główne mechanizmy:**
1. Normalizacja tekstu (`\r\n` → `\n`).
2. Podział na linie.
3. Liczenie wcięć (musi być wielokrotnością 4 spacji).
4. Tworzenie instrukcji na podstawie linii:
   - `if ...:`
   - komendy gry `attack()`, `block()`
5. Budowanie drzewa instrukcji za pomocą stosu bloków (`blockStack`).

**Błędy zgłaszane przy:**
- Nieprawidłowych wcięciach.
- Braku nawiasów w komendzie.
- Nieznanej komendzie.

---

## 6. GameCommandLibrary

**Zadanie:**
- Definicje komend gry możliwych do użycia w edytorze.
- Na ten moment obsługiwane komendy:
    - `attack()`
    - `block()`

**Funkcje:**
- `IsGameCommand(name)` – sprawdza, czy komenda istnieje.
- `Create(name, args, line)` – tworzy `ParsedInstruction`.

---

## 7. TabToSpaces (Zamiana TAB → spacje)

**Zadanie:**
- Umożliwia komfortowe pisanie kodu w edytorze.
- Każde naciśnięcie TAB wprowadza *4 spacje*.
- Blokuje systemowe działanie klawisza TAB w Unity.

---

## 8. Ogólny przepływ interakcji

1. Gracz wchodzi w trigger skrzynki lub posągu.
2. Wyświetla się `interactText`.
3. Gracz naciska `E`:
    - Skrzynka → natychmiast się otwiera.
    - Posąg → uruchamia edytor kodu.
4. Edytor:
    - blokuje ruch gracza
    - przyjmuje kod użytkownika
    - parsuje go po wciśnięciu przycisku „Submit”
5. Zamknięcie edytora:
    - przywraca ruch gracza
    - ukrywa UI

---
