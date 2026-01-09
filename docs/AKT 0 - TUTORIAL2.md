
Miejsce: dom gracza  
Cel: nauczyć myślenia jak komputer  
Forma: interaktywny samouczek fabularny  

**AI:**  
> “This Bugon is unstable. It is trapped in corrupted code.”  
> “You cannot fight yet. First — you must fix the error.”  


________________________________________
MISJA TUTORIALOWA – „THE NEVER-ENDING TASK”  
Nauka: while  
Lokalizacja: pokój roboczy  
Na biurku: komputer z otwartym procesem  
Na ekranie:  
Working...  
Working...  
Working...  
**AI:**  
> “Nothing is broken.”   
> “This is repetition.”   
**AI** – wyjaśnienie od podstaw:  
> “A computer cannot guess when to stop.”  
> “It only follows rules.”  
> “A while loop is a rule that says: repeat this action while a condition is true.”  
**AI** – przykład z życia:  
> “While the room is dark — keep the light on.”  
 “When it becomes bright — turn it off.”  
**AI** (pokazuje problem):  
> “This process says:”  
> “While the task is not finished — keep working.”  
> “But the system is never told when the task is finished.”  
Quest Log:  
•	Observe the condition.  
•	Decide when the task should end.  
Widok logiczny:  
WHILE: task_finished = false  
→ repeat  
**AI** (kluczowa lekcja):  
> “A loop is not dangerous.”    
> “An unchanged condition is.”   
Akcja gracza:  
•	Ustawia moment zmiany stanu:  
task_finished = False  

while not task_finished:  
    print("Working...")  
    task_finished = True  

print("Task finished!")  
Efekt:  
•	Proces zatrzymuje się.  
•	Ekran wraca do normalnego stanu.  
**AI** (podsumowanie):  
> “While means: wait and repeat until something changes.”    
________________________________________
MISJA TUTORIALOWA – „THE SIMPLE DECISION”  
Nauka: if / else  
Lokalizacja: kuchnia, panel sterowania piekarnikiem  
**AI** (wprowadzenie):  
> “Now we teach the system how to choose.”  
**AI** – absolutne podstawy:  
> “If means: check a condition.”  
> “If it is true — do one thing.”  
> “If it is false — do something else.”  
**AI** – przykład codzienny:  
> “If the temperature is too high — turn off the oven.”  
> “Else — keep it on.”  
Quest Log:  
•	Check the condition.  
•	Define behavior for both outcomes.  
Widok logiczny:  
IF: temperature > safe_limit  
→ turn off  
ELSE:  
→ keep running  
**AI** (o else bardzo jasno):  
> “Else means: every situation where the if is not true.”  
> “It prevents uncertainty.”  
Akcja gracza:  
•	Ustawia poprawne reakcje:  
temperature = 220    

if temperature > 200:  
    print("Too hot! Turn off the oven.")  
else:  
    print("Temperature is safe. Keep cooking.")  
Efekt:  
•	Piekarnik działa stabilnie.  
**AI** (podsumowanie):  
> “If checks.”    
> “Else responds.”    
________________________________________
MISJA FINAŁOWA TUTORIALA – „THE EXIT CONDITION”  
Nauka: if + while razem  
Zakończenie: WYJŚCIE Z DOMU  
Lokalizacja: drzwi wyjściowe, panel sterujący obok nich  
Stan: drzwi zamknięte  
**AI** (poważnie, ale spokojnie):  
> “Real systems combine rules.”    
> “This door waits.”    
> “And it decides.”    
**AI** – rozbicie logiki:  
> “While the door is locked — it stays closed.”    
> “If access is granted — unlock it.”    
> “Else — do nothing.”    
Quest Log:  
•	Define the unlock condition.  
•	Control the waiting loop.  
Widok logiczny:
WHILE: door_locked = true
→ remain closed
IF: access_granted = true
→ door_locked = false
ELSE:
→ stay locked
**AI** (najważniejsza lekcja):
> “While handles waiting.”  
> “If handles change.”  
> “Else ensures stability.”  
Akcja gracza:
•	Ustawia poprawną logikę i zmienia stan access_granted:
# Stan drzwi
door_locked = True
access_granted = False

# Powtarzaj dopóki drzwi są zamknięte
while door_locked:
    print("Door is locked. Waiting...")
    
    # Sprawdź, czy gracz może odblokować drzwi
    if access_granted:
        door_locked = False  # odblokuj drzwi
    else:
        print("Access not granted. Stay locked.")

print("The door is now open. You can exit!")
Efekt:
•	Pętla while kończy się
•	Drzwi odblokowują się
•	Słychać klik mechanizmu
•	Drzwi się otwierają
**AI** (zakończenie tutoriala):
> “You didn’t guess.”  
> “You defined rules.”  
> “Step outside.”  
Gracz wychodzi z domu.
KONIEC TUTORIALA

