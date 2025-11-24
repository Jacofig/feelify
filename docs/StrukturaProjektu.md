#  Struktura Projektu

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
