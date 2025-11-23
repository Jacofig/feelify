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











#  Dokumentacja Sceny

Opis struktury sceny wraz z głównymi obiektami i ich rolą w projekcie Unity.

---

## 1. Global Light 2D
- Typ: **Light 2D**
- Funkcja: Oświetlenie całej sceny.
- Uwagi:
  - Zapewnia globalne oświetlenie 2D w grze.
  - Nie ma interakcji z fizyką ani skryptami gameplayu.

---

## 2. Grid + Tilemap
- Typ: **Grid** zawierający **Tilemap**
- Funkcja:
  - Reprezentuje teren/podłogę w grze.
  - Tilemapy definiują, gdzie gracz może się poruszać.
- Obiekty w warstwie podłogi:
  - **Drzewa** – statyczne przeszkody dla gracza i NPC.
  - **Skrzynka** – obiekt interaktywny (`ChestInteract`).
  - **Statua** – obiekt interaktywny do otwarcia okna tekstowego.
- Uwagi:
  - Grid działa jako nadrzędna struktura dla Tilemapy i obiektów dekoracyjnych.
  - Obiekty na podłodze mogą mieć własne kolidery i skrypty.

---

## 3. Champ
- Typ: **GameObject**
- Zawiera:
  - **Gracz** – obiekt sterowany przez `PlayerMovement`.
  - **Kamera** – śledzi pozycję gracza.
- Funkcja:
  - Reprezentuje główną postać gracza w scenie.
  - Kamera w nim zapewnia widok 2D, podążający za graczem.

---

## 4. EventSystem
- Typ: **UI System**
- Funkcja:
  - Obsługuje interakcje UI, takie jak przyciski i pola tekstowe.
  - Wymagany do działania elementów UI, np. okien dialogowych czy edytora kodu.

---

## 5. CodeEditor
- Typ: **GameObject**
- Zawiera:
  - Canvas z elementami UI (`TMP_InputField`, przyciski do zapisu i zamknięcia).
- Funkcja:
  - Otwiera okno edytora tekstowego po interakcji z NPC (`npc.cs`).
  - Umożliwia wpisanie tekstu przez gracza i zapis do pliku `.txt`.
  - Podczas otwarcia edytora blokuje ruch gracza (`playerMovement.canMove = false`).
- Uwagi:
  - Aktywowane/dezaktywowane w trakcie gry.
  - Powiązane ze skryptem `npc.cs`.

---













#  Struktura Projektu Unity

Opis folderów w projekcie wraz z ich przeznaczeniem i przykładowymi zawartościami.

---

## 1. `Input/`
- Folder, w którym zapisywane są pliki generowane przez gracza, np. `user_code.txt`.
- Obsługiwany przez skrypty typu `npc.cs` i `CodeEditor`.
- Funkcja: przechowywanie danych wejściowych gracza w formacie tekstowym.

---

## 2. `Material/`
- Zawiera **obiekty stworzone ręcznie** przez twórcę projektu.
- Mogą to być materiały przypisane do prefabów, Tilemap, sprite’ów lub innych obiektów 2D/3D.
- Funkcja: własne zasoby graficzne i materiały do obiektów w grze.

---

## 3. `Materials/`
- Zawiera **obiekty pobrane z Asset Store** lub innych źródeł zewnętrznych.
- Funkcja: gotowe materiały lub tekstury używane w projekcie.
- Uwagi: różni się od folderu `Material` tym, że nie są tworzone ręcznie.

---

## 4. `Prefab/`
- Zawiera **przygotowane prefaby** do użycia w scenie.
- Przykłady: gracz, NPC, skrzynki, drzewa, statuy.
- Funkcja: umożliwia łatwe wstawianie obiektów do sceny z gotową konfiguracją komponentów, skryptów i colliderów.

---

## 5. `scena/` *(nieużywana)*
- Folder eksperymentalny, zawiera scenę próbną.
- Nie jest używana w finalnym projekcie.

---

## 6. `Scenes/`
- Zawiera wszystkie sceny projektu oraz powiązane zasoby:
  - Shadery
  - Tekstury
  - Tile Palette
  - Sample Scene
- Funkcja: centralne miejsce przechowywania wszystkich scen gry oraz powiązanych materiałów do tworzenia map i środowiska.

---

## 7. `Scripts/`
- Zawiera wszystkie skrypty C# w projekcie.
- Przykłady: `PlayerMovement.cs`, `ChestInteract.cs`, `npc.cs`.
- Funkcja: logika gry, interakcje, systemy sterowania, UI itp.

---

## 8. `Settings/`
- Zawiera ustawienia projektu Unity.
- Przykłady: Input System, grafika, fizyka, prefab settings.
- Funkcja: konfiguracja globalna projektu.

---

## 9. `TextMeshPro/`
- Zawiera zasoby i ustawienia TextMesh Pro.
- Przykłady: fonty, atlas tekstur, style.
- Funkcja: umożliwia korzystanie z zaawansowanego renderowania tekstu w UI i edytorach tekstowych w grze.

---

##  Podsumowanie
- `Scripts` – logika gry  
- `Scenes` – sceny i zasoby map  
- `Prefab` – gotowe obiekty do użycia w scenach  
- `Material` i `Materials` – materiały własne i pobrane  
- `Input` – dane tworzone w trakcie gry  
- `Settings` – konfiguracja projektu  
- `TextMeshPro` – fonty i style dla UI  
- `scena` – folder eksperymentalny, nieużywany
