# Dokumentacja przepływu akcji

## 1. PlayerMovement (Ruch gracza)

**Składniki:**
- Rigidbody2D
- Keyboard Input (WASD)
- `canMove` – flaga blokująca ruch

**Przepływ akcji:**
1. `Update()`:
    - Sprawdza `canMove`. Jeśli `false`, ruch jest zablokowany.
    - Odczytuje wejścia z klawiatury (WASD) i tworzy `moveInput`.
    - Normalizuje `moveInput`, aby zapobiec szybszemu ruchowi po skosie.
2. `FixedUpdate()`:
    - Oblicza nową pozycję: `rb.position + moveInput * speed * Time.fixedDeltaTime`.
    - Porusza gracza za pomocą `rb.MovePosition(newPos)`.

**Uwagi:**
- `canMove = false` jest ustawiane podczas otwierania edytora NPC, aby zablokować gracza.

---

## 2. ChestInteract (Interakcja ze skrzynką)

**Składniki:**
- `openChestPrefab` – prefabrykat otwartej skrzynki
- `interactText` – UI wskazujące możliwość interakcji
- `playerNear` – flaga obecności gracza w triggerze

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
    - Jeśli `playerNear` i wciśnięto `E`, wywołuje `OpenChest()`.
4. `OpenChest()`:
    - Tworzy instancję `openChestPrefab` w miejscu skrzynki.
    - Ukrywa `interactText`.
    - Niszczy obiekt skrzynki (`Destroy(gameObject)`).

---

## 3. NPC / Editor Interaction (Interakcja z NPC i edytorem)

**Składniki:**
- `TMP_InputField inputField`
- `editorCanvas`
- `interactText`
- `playerMovement` – referencja do gracza
- `playerNear` – flaga obecności gracza
- `editorOpen` – flaga stanu edytora

**Przepływ akcji:**
1. `OnTriggerEnter2D(Collider2D col)`:
    - Jeśli `col` to gracz:
        - `playerNear = true`
        - Pokazuje `interactText`.
2. `OnTriggerExit2D(Collider2D col)`:
    - Jeśli `col` to gracz:
        - `playerNear = false`
        - Ukrywa `interactText`.
        - Jeśli edytor był otwarty, wywołuje `ExitEditor()`.
3. `Update()`:
    - Jeśli gracz jest blisko, edytor zamknięty i wciśnięto `E`, wywołuje `OpenEditor()`.
4. `OpenEditor()`:
    - Aktywuje `editorCanvas`.
    - Czyści i aktywuje `inputField`.
    - Ukrywa `interactText`.
    - `editorOpen = true`
    - Blokuje ruch gracza: `playerMovement.canMove = false`.
5. `SubmitText()`:
    - Pobiera tekst z `inputField`.
    - Tworzy folder `/Input` w `Application.dataPath` (jeśli nie istnieje).
    - Zapisuje tekst do `user_code.txt`.
    - Wywołuje `ExitEditor()`.
6. `ExitEditor()`:
    - Ukrywa `editorCanvas`.
    - `editorOpen = false`
    - Odblokowuje ruch gracza: `playerMovement.canMove = true`.

---

## 4. Ogólny przepływ interakcji

1. Gracz zbliża się do skrzynki/NPC → `playerNear = true` → pojawia się `interactText`.
2. Gracz naciska `E`:
    - Jeśli skrzynka → otwiera skrzynkę (`OpenChest`) i usuwa ją.
    - Jeśli NPC → otwiera edytor (`OpenEditor`) i blokuje ruch gracza.
3. Gracz może wpisać kod w edytorze i zatwierdzić (`SubmitText`) → zapis pliku → zamknięcie edytora i odblokowanie ruchu.
4. Gracz oddala się → `playerNear = false` → ukrycie `interactText`.
    - Jeśli edytor był otwarty, zamyka go automatycznie (`ExitEditor`).

---
