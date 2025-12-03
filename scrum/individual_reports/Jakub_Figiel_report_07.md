# Raport indywidualny – Sprint 7

**Imię i nazwisko:** Jakub Figiel  
**Zespół:** Loopers  
**Numer sprintu:** 7  
**Okres:** 2025-11-26 – 2025-12-02  

## Zakres moich działań:
1. Research na temat implementacji mechaniki walki.
2. Dodanie nowego npc - pierwszego wroga i dodanie interakcji z nim.
3. Utworzenie nowej sceny (sceny walki) z UI (stworki gracza i wroga i ich statystyki)
4. Utworzenie obiektów stworków, tak aby tworzenie przyszłych walk było proste (zmiana stworka u przeciwnika).
5. Import statystyk z sceny overworld do sceny walki.

## Wkład w projekt:
W tym sprincie zrealizowałem fundamenty systemu walki, co znacząco posuwa projekt do przodu.    
Przygotowałem przejście ze świata overworld do sceny walki oraz stworzyłem pełną infrastrukturę:    
	•	dodanie przeciwnika z triggerem i interakcją (wejście do walki po naciśnięciu E),   
	•	stworzenie nowej sceny BattleScene wraz z podstawowym UI walki (nazwa, poziom, HP, sprite’y stworków),  
	•	przygotowanie systemu ScriptableObject PokémonData do przechowywania statystyk stworków,    
	•	opracowanie prefabów stworków oraz sposobu ich instancjonowania w walce,    
	•	zaimplementowanie BattleData, które umożliwia przenoszenie danych o walczących stworkach między scenami,    
	•	przygotowanie podstaw logiki walki, co pozwoli łatwo rozszerzać system o ataki, obrażenia, rundy itd.       

## Samoocena:

| Obszar | Ocena (1–5) | Komentarz |
|--------|--------------|-----------|
| Zaangażowanie | 5 | Regularna praca, angażowałem się w realizację wszystkich obowiązków. Aktywnie uczestniczyłem w wykonywaniu zadań i wdrażaniu systemu walki od podstaw. |
| Wkład merytoryczny | 5 | Przygotowałem dokumentację, koncepcję walki, zaprojektowałem strukturę systemu oraz stworzyłem działające przejście do sceny walki i system przenoszenia statystyk. |
| Komunikacja | 4.5 | Komunikacja przebiegała bez wiekszych problemów. Każdy wkładał coś od siebie. |
| Terminowość | 3 | Wszystko zakończone w terminie, ale na ostatnią chwilę |

## Refleksja:
Udało mi się zrealizować wszystkie cele sprintu.
Stworzyłem solidną podstawę systemu walki, która będzie rozwijana w kolejnych iteracjach (dodanie ataków, logiki tury, animacji itp.). Kolejnym krokiem będzie integracja powstałego systemu z interpreterem.
