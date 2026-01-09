
---

# **FORGING TUTORIAL**

### **Stage 0 – Introduction**

**AI:**

> "Welcome, coder! Today you’ll learn to control the world using code. In forging, each action changes the state of the metal. Let’s start step by step."

> "Watch carefully: I’ll show a command, explain why it’s useful, and you’ll see it in action."

---

### **Step 1 – Hit (Strike the Metal)**

**AI:**

> "`hit()` strikes the metal with a hammer. It’s the most basic action. Every sword or weapon starts with hits."

**Why it’s useful:**

> "Without hitting, the metal won’t change shape. Think of it as telling the computer: 'Do this action now.' Each hit makes progress."

**Code:**

```python
hit()
```

**Effect in game:**

* Hammer strikes → sparks appear.
* The metal slightly changes shape.

**AI:**

> "See? Simple commands let you control the world immediately. This is the foundation of coding actions."

---

### **Step 2 – Heat (Heat the Metal)**

**AI:**

> "`heat()` warms the metal. Some actions only work on hot metal."

**Why it’s useful:**

> "You can’t shape cold metal. Using `heat()` first ensures that the next action (`hit()`) will work properly. This shows a dependency: one step must happen before another."

**Code:**

```python
heat()
```

**Effect:**

* Metal glows red → ready to hit.

**AI:**

> "This teaches you sequencing: the order of commands matters. Code is not magic – you tell it step by step."

---

### **Step 3 – Add (Add Ingredients)**

**AI:**

> "`add('ingredient')` mixes special materials into the metal."

**Why it’s useful:**

> "Different ingredients give different effects. Fire essence makes the sword hot and magical. Coding this teaches you **parameters** – giving commands extra information."

**Code:**

```python
add("fire_essence")
```

**Effect:**

* Fire essence floats into the metal → metal glows.

**AI:**

> "See how adding one ingredient changes the result? This is how variables and arguments affect actions."

---

### **Step 4 – Cast (Cast a Spell)**

**AI:**

> "`cast('enchant')` applies magic to your weapon. It completes the forging process."

**Why it’s useful:**

> "Casting is like telling the computer: 'Finish the process now.' It shows **end conditions** – when the task is done."

**Code:**

```python
cast("enchant")
```

**Effect:**

* Metal glows → enchanted sword appears.

**AI:**

> "Every command is an instruction. You control exactly what happens, in order."

---

# **Stage 1 – If Statement (Conditional Actions)**

**AI:**

> "Sometimes you want to make decisions. For example, you only hit metal if it’s hot. That’s where `if` is useful."

**Why it’s useful:**

> "`If` checks a condition and decides which action to do. It prevents mistakes, like hitting cold metal."

**Code:**

```python
metal_temperature = "cold"

if metal_temperature == "hot":
    hit()
else:
    heat()
```

**Effect:**

* Cold → heats up.
* Hot → hammer strikes.

**AI:**

> "If statements are everywhere in code. They let the program react differently depending on the situation. Without them, the computer just follows the same rules blindly."

---

# **Stage 2 – While Statement (Repeating Actions)**

**AI:**

> "Next is `while`. It repeats actions until a condition is met."

**Why it’s useful:**

> "You can strike the metal multiple times without writing `hit()` 5 times. The computer will keep repeating until the goal is reached."

**Code:**

```python
hammer_hits = 0

while hammer_hits < 5:
    hit()
    hammer_hits += 1
```

**Effect:**

* Hammer strikes 5 times → metal forms shape.

**AI:**

> "While loops are powerful. They let you handle repetitive tasks automatically. But be careful: if the condition never becomes false, the loop runs forever!"

---

# **Stage 3 – If + While Together (Advanced Sequence)**

**AI:**

> "Now combine decisions and repetition. Heat the metal if needed, strike 5 times, add fire essence, and cast enchantment."

**Why it’s useful:**

> "This teaches you how to control complex sequences: make decisions AND repeat actions until tasks are complete."

**Code:**

```python
metal_temperature = "hot"
hammer_hits = 0
enchanted = False

while not enchanted:
    if metal_temperature == "hot":
        while hammer_hits < 5:
            hit()
            hammer_hits += 1
        add("fire_essence")
        cast("enchant")
        enchanted = True
    else:
        heat()
```

**Effect:**

* Metal hot → strike 5 times → add ingredient → cast spell → sword appears.

**AI:**

> "If checks conditions. While repeats actions. Together they give you full control over processes in code."

---

# **PORTAL TUTORIAL**

### **Step 1 – Attack**

**AI:**

> "`attack()` defeats a Bugon. Each portal can spawn multiple Bugons."

**Why it’s useful:**

> "You can’t close a portal until all enemies are gone. This shows how code can enforce rules and dependencies."

**Code:**

```python
attack()
```

**Effect:**

* Player attacks → one Bugon disappears.

**AI:**

> "Each attack changes the world immediately. That’s why coding is powerful: each line has a visible effect."

---

### **Step 2 – Close Portal**

**AI:**

> "`close_portal('A')` closes a portal once it’s safe."

**Why it’s useful:**

> "It teaches **conditional actions**: don’t do something until preconditions are satisfied."

**Code:**

```python
close_portal("A")
```

**Effect:**

* Portal closes → Bugons stop emerging.

---

### **Stage 1 – If Statement**

**AI:**

> "Some portals are different colors. Use `if` to decide which to close."

**Code:**

```python
portal_color = "red"

if portal_color == "blue":
    close_portal("B")
else:
    print("This portal is not blue. Skip it.")
```

**Effect:**

* Red portal → ignored.
* Blue portal → closes.

**AI:**

> "If allows decisions based on properties, like color or type. Computers need these to react properly."

---

### **Stage 2 – While Statement**

**AI:**

> "Use `while` to repeat actions until all Bugons are gone."

**Code:**

```python
bugons_remaining = 3

while bugons_remaining > 0:
    attack()
    bugons_remaining -= 1

close_portal("A")
```

**Effect:**

* Player attacks → portal closes when no Bugons remain.

**AI:**

> "While loops automate repetitive tasks and ensure conditions are met before moving on."

---

### **Stage 3 – If + While Together**

**AI:**

> "Manage multiple portals with different colors. Defeat Bugons, then close portals according to rules."

**Code:**

```python
portals = {
    "A": {"color": "red", "open": True, "bugons": 2},
    "B": {"color": "blue", "open": True, "bugons": 1},
    "C": {"color": "green", "open": True, "bugons": 3}
}

while any(p["open"] for p in portals.values()):
    for name, portal in portals.items():
        if portal["open"]:
            print(f"Portal {name} ({portal['color']}) is open! {portal['bugons']} Bugons remain.")
            while portal["bugons"] > 0:
                attack()
                portal["bugons"] -= 1
            if portal["color"] != "red":
                close_portal(name)
```

**Effect:**

* Multiple portals → attack Bugons → close portals based on color.

**AI:**

> "Combining if and while gives you control over complex sequences, multiple objects, and conditions. This is how real programs work."

---



**AI:**

> "You have learned all commands step by step, with explanations:
>
> * Hit, heat, add, cast – for forging.
> * Attack, close_portal – for portals.
> * If – makes decisions.
> * While – repeats actions until conditions are met.
> * Combining if + while – controls complex sequences.
>
> "Every command has an effect. Coding is about controlling the world logically and sequentially. Keep experimenting!"

---

