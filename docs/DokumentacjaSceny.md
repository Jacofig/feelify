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

