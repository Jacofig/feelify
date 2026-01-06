# Raport indywidualny – Sprint 11

**Imię i nazwisko:** Stanisław Golański  
**Zespół:** Loopers  
**Numer sprintu:** 11  
**Okres:** 2025-12-17 – 2026-01-07  

## Zakres moich działań:
# Dokumentacja funkcjonalności gry

## 1. Ekran startowy (Main Menu)


Menu główne zawiera następujące funkcjonalności:

- **New Game** – rozpoczyna nową rozgrywkę i inicjalizuje dane gry  
- **Load Game** – umożliwia wczytanie wcześniej zapisanej gry  
- **Options** – pozwala na konfigurację ustawień gry  
- **Exit** – poprawnie zamyka aplikację  

Menu zostało wykonane w sposób **modułowy**, co umożliwia jego dalszą rozbudowę oraz łatwe zarządzanie poszczególnymi elementami interfejsu użytkownika.

---

## 2. System opcji (Audio & Controls)

W menu opcji zaimplementowałem rozbudowany system ustawień, który jest **współdzielony pomiędzy menu głównym oraz menu pauzy**.

### Audio
- regulacja głośności **Master**, **Music** oraz **SFX**  
- ustawienia realizowane za pomocą **Audio Mixera**, co pozwala na precyzyjne i profesjonalne zarządzanie dźwiękiem  
- zmiany dokonywane przez gracza są **natychmiastowe** i spójne w całej grze  

### Controls
- możliwość zmiany przypisania klawiszy sterujących (**key bindings**)  
- system resetowania sterowania do **wartości domyślnych**  
- elastyczna architektura umożliwiająca łatwe dodawanie kolejnych akcji  

---

## 3. Cutscenka / Intro fabularne

Stworzyłem intro gry w formie **cutscenki** składającej się z czterech paneli:

- każdy panel posiada osobne **tło graficzne**  
- tekst fabularny wyświetlany jest w formie dialogowej  
  - efekt pisania **słowo po słowie**  
  - możliwość **przyspieszenia** wyświetlania tekstu przez gracza  
- każdy panel przedstawia kolejny fragment historii  

Cutscenka pełni funkcję zarówno **narracyjną**, jak i **immersyjną**, budując klimat gry od samego początku.

---

## 4. Menu pauzy (Pause Menu)

Zaimplementowałem w pełni funkcjonalne menu pauzy dostępne w trakcie rozgrywki.

Dostępne opcje:

- **Continue** – powrót do gry  
- **Options** – dostęp do ustawień audio i sterowania  
- **Save Game** – zapis aktualnego stanu gry  
- **Exit** – wyjście do menu głównego lub zamknięcie gry  

Menu pauzy jest w pełni **zsynchronizowane z menu głównym** – wszelkie zmiany ustawień są współdzielone i obowiązują w całej grze.

---

## 5. Poprawa samouczka i fabuły

### Samouczek
- przeprojektowany w celu zwiększenia **czytelności**  
- naturalne wprowadzenie gracza w podstawowe **mechaniki gry**  

### Fabuła
Opracowałem zarys fabularny gry:

- **Akt 1** – wprowadzenie do świata gry, bohatera oraz głównego konfliktu  
- **Akt 2** – rozwinięcie fabuły, eskalacja wydarzeń i przygotowanie gracza na dalszą część historii  

Fabuła została zaplanowana w sposób umożliwiający jej **dalszą rozbudowę** w kolejnych etapach produkcji.

---

## 6. System dźwięku i Audio Mixer

Stworzyłem oraz skonfigurowałem **Audio Mixer**, który:

- umożliwia oddzielne sterowanie kategoriami dźwięku:
  - Master  
  - Music  
  - SFX  
- pozwala na **płynne przejścia** pomiędzy utworami muzycznymi  
- ułatwia dalsze zarządzanie dźwiękiem oraz **skalowanie projektu**  

---



## Wkład w projekt:
Stworzyłem całe UI menu głównego, włącznie z funkcjonalnościami np. zmiana głośności i klawiszy sterujących. Stworzyłem menu pauzy, w którym można zapisać grę oraz ma te same funkcjonalności co menu główne. Stworzyłem prototyp intro do gry składające się z czterech paneli łącznie z tekstem fabularnym. Stworzyłem skrypty w menu: ButtonHover.cs MainMenu.cs RebindMenu.cs SliderPercentage.cs, skrypty w overworld: PauseMenu.cs, SaveableObject.cs, SaveManager.cs oraz skrypty w intro: IntroInput.cs, TypewriterText.cs. Dodatkowo stworzyłem dwa typy muzyk, które wymagają poprawy, ale miały służyć głównie do testowania ustawień głośności.

## Samoocena:

| Obszar | Ocena (1–5) | Komentarz |
|--------|--------------|-----------|
| Zaangażowanie | 5 | Regularna praca, angażowałem się w pracę i realizację wszystkich obowiązków. Aktywnie uczestniczyłem w wykonywaniu zadań. Zadania wykynywałem regularnie przed świętami oraz po świętach, codziennie lub co drugi dzień, gdy tylko miałem czas.|
| Wkład merytoryczny | 4.5 | Stworzyłem dwie sceny przedstawiające menu oraz intro, oraz pauzę w scenie głównej. Wszystko działa w prosty sposób, ale nie jest zrobione idealnie, ponieważ nie mogłem zrobić rzeczy, które były uzależnione od wcześniejszego wykonania pracy innych członków grupy.|
| Komunikacja | 4 | Komunikacja przebiegała bez wiekszych problemów. Każdy wkładał coś od siebie. W trakcie świąt nie było żadnych spotkań poza jednym dzień przed oddaniem sprintu.|
| Terminowość | 5 | Wszystko zakończone w terminie, zadania wykonane kilka dni przed ostatecznym terminem, wszystko było oddawane regularnie i widać postęp przez cały okres wykonywania zadań. |

## Refleksja:
Udało mi się zrealizować wszystkie cele sprintu, aczkolwiek niektóre rzeczy dało się zrobić lepiej, gdyby była lepsza komunikacja i gdybym lepiej pomyślał nad niektórymi rzeczami jednak są to drobne poprawki, które łatwo zastosować później, ogółem wszystko zostało wykonane.
