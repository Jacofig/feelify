# Raport indywidualny – Sprint 15

**Imię i nazwisko:** Jakub Figiel
**Zespół:** Loopers  
**Numer sprintu:** 15  
**Okres:** 2026-01-21 – 2026-01-28  

## Zakres moich działań:
1. Praca nad mapą
2. Ostateczne ułozenie mapy wioski
3. Dodanie lokacji lasu pod wioską
4. Dodanie colliderów budynkom i drzewom
5. Rozbicie wszystkich struktur na dwa sprity, tak aby gracz był na górze sprita podchodząc od dołu i na dole sprita podchodząc od góry (perspektywa)

## Wkład w projekt:
W trakcie Sprintu 15 mój wkład koncentrował się na dopracowaniu i rozszerzeniu świata gry od strony mapy oraz poprawie aspektów technicznych związanych z poruszaniem się i perspektywą. Finalnie ułożyłem mapę wioski, dbając o spójność rozmieszczenia elementów oraz czytelność przestrzeni dla gracza. Równolegle dodałem nową lokację – las umieszczony pod wioską – co rozszerzyło obszar eksploracji i przygotowało bazę pod kolejne aktywności w grze.

Od strony technicznej dodałem collidery do budynków i drzew, dzięki czemu interakcja z otoczeniem stała się poprawna (blokowanie przejścia, brak „wchodzenia” w obiekty) i bardziej przewidywalna w trakcie rozgrywki. Dodatkowo wprowadziłem rozwiązanie perspektywy poprzez rozbicie struktur na dwa sprity (góra/dół), co pozwala na prawidłowe „nakładanie” gracza na obiekty w zależności od kierunku podejścia. Dzięki temu scena wygląda naturalniej i eliminuje typowe problemy z kolejnością renderowania w widoku top-down.

## Samoocena:

| Obszar | Ocena (1–5) | Komentarz |
|--------|-------------|-----------|
| Zaangażowanie | 5 | Zrealizowałem wszystkie zaplanowane zadania mapowe i techniczne, dopinając zarówno zawartość (wioska/las), jak i kwestie kolizji oraz perspektywy. |
| Wkład merytoryczny | 5 | Wprowadziłem istotne usprawnienia w odbiorze mapy: poprawne kolizje oraz system podziału sprite’ów, który zwiększa czytelność i realizm perspektywy. |
| Komunikacja | 5 | Na bieżąco przekazywałem postępy i ustalenia dotyczące zmian na mapie; część decyzji była podejmowana w trakcie pracy, wraz z doprecyzowaniem potrzeb projektu. |
| Terminowość | 4 | Zadania zostały domknięte w sprincie; najwięcej czasu zajęło dopracowanie perspektywy i colliderów, bo wymagało konsekwentnych zmian w wielu obiektach na mapie. |

## Refleksja:
Sprint 15 był dla mnie nastawiony na „dopinanie jakości” mapy i przygotowanie jej pod stabilną rozgrywkę. Największą wartością było połączenie prac wizualnych z technicznymi: finalizacja wioski i dodanie lasu zwiększyły zakres świata, a collidery oraz podział struktur na dwa sprity znacząco poprawiły odbiór perspektywy i poruszania się gracza. W kolejnym sprincie łatwiej będzie rozwijać te lokacje, bo baza jest już uporządkowana i działa przewidywalnie.