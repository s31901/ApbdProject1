# ApbdProject1 - Wypożyczalnia sprzętu
Aplikacja konsolowa w C# symulująca system wypożyczalni sprzętu. Projekt realizowany w ramach przedmiotu APBD na PJATK.

## Uruchomienie
1. Sklonuj repozytorium:
```
git clone https://github.com/s31901/ApbdProject1
```
2.  Otwórz projekt w Rider lub Visual Studio.
3.  Uruchom projekt ( `Run` / `F5` ).

> Nie są wymagane żadne dodatkowe zależności ani konfiguracja.

## Funkcjonalności
1. Dodawanie użytkowników ( `Student` / `Employee` )
2. Dodawanie sprzętu trzech typów: `Laptop`, `Projector`, `Camera`
3. Wyświetlanie całego sprzętu oraz tylko dostępnego do wypożyczenia
4. Wypożyczanie sprzętu z walidacją reguł biznesowych (limit aktywnych wypożyczeń, dostępność sprzętu)
5. Zwrot sprzętu z automatycznym naliczaniem kary za opóźnienie
6. Oznaczanie sprzętu jako uszkodzonego / w serwisie (oznaczone jako `out of order`)
7. Wyświetlanie aktywnych wypożyczeń dla konkretnego użytkownika
8. Wyświetlanie przeterminowanych wypożyczeń
9. Generowanie raportu podsumowującego stan wypożyczalni

## Struktura projektu
```
ApbdProject1/
├── Models/         - obiekty domenowe, czyste dane bez logiki biznesowej
├── Services/       - logika biznesowa, operacje na danych
├── Config/         - reguły biznesowe (RentalPolicy)
└── UI/             - interfejs konsolowy, obsługa wejścia/wyjścia
```

## Branches
- `feature/domain-model` - modele domenowe (Equipment, User, Rental i podklasy)
- `feature/logic` - serwisy z logiką biznesową (RentalService, EquipmentService, UserService)
- `feature/console-interaction` - warstwa UI (ConsoleMenu), poprawki po testach
> [!IMPORTANT]
> Wszystkie gałęzie zostały połączone z `master`.

## Decyzje projektowe
### Podział warstw i kohezja
Projekt jest podzielony na cztery warstwy z jasno określonymi odpowiedzialnościami:

- **Models** zawierają wyłącznie dane i właściwości domenowe. Klasy modelowe nie wiedzą o istnieniu serwisów ani konsoli. Przykład: Rental oblicza DueDate i IsOverdue na podstawie własnych pól - to jedyna logika która tu się pojawia, bo dotyczy bezpośrednio stanu obiektu.
- **Services** zawierają całą logikę biznesową. Serwisy nie wiedzą o istnieniu konsoli - nie ma w nich żadnych wywołań Console.WriteLine. Każdy serwis ma jedną odpowiedzialność: EquipmentService zarządza sprzętem, RentalService zarządza wypożyczeniami, UserService zarządza użytkownikami.
- **Config** (RentalPolicy) to jedyne miejsce gdzie zdefiniowane są reguły biznesowe: stawka kary za dzień opóźnienia (DailyFineRate), limity aktywnych wypożyczeń dla studenta (StudentMaxRentals = 2) i pracownika (EmployeeMaxRentals = 5). Zmiana dowolnej reguły wymaga edycji jednego miejsca w kodzie.
- **UI** (ConsoleMenu) odpowiada wyłącznie za komunikację z użytkownikiem - czyta input, wywołuje odpowiednią metodę serwisu i wyświetla wynik. Nie zawiera logiki biznesowej.

### Coupling
Zależności są jednostronne - `ConsoleMenu` zna serwisy, serwisy znają modele, modele nie znają nikogo. `RentalService` nie zależy bezpośrednio od EquipmentService ani UserService - obiekty `Equipment` i `User` są przekazywane z zewnątrz przez `ConsoleMenu`, które pobiera je od odpowiednich serwisów przed wywołaniem NewRental().

### Dziedziczenie i abstrakcja
Klasa `Equipment` jest abstrakcyjna - nie można stworzyć "gołego" sprzętu, tylko konkretny typ (`Laptop`, `Projector`, `Camera`). Każda podklasa ma minimum dwa własne pola specyficzne i nadpisuje ToString(). Decyzja o użyciu dziedziczenia wynika z modelu domeny - sprzęt ma wspólne cechy (nazwa, producent, dostępność) i cechy specyficzne dla typu.

### Generowanie ID
ID obiektów jest nadawane przez serwis w momencie dodania do kolekcji (`_nextId++`), a nie przez sam model. Dzięki temu model nie zarządza własnym globalnym stanem - to odpowiedzialność serwisu.

### Obsługa błędów
Walidacja wypożyczenia (dostępność sprzętu, status usterki, limit użytkownika) odbywa się w `RentalService` przed dodaniem rekordu. Konstruktory modeli walidują dane wejściowe przez `ArgumentNullException` i `ArgumentException`. Warstwa UI opakowuje operacje wejściowe w bloki try/catch - błędny format inputu nie przerywa działania aplikacji, tylko wyświetla komunikat i wraca do menu.

### Raport końcowy
Wygenerowany raport zawiera:
1. Całkowitą liczbę sprzętów.
2. Liczbę dostępnego sprzętu.
3. Liczbę zarejestrowanych użytkowników.
4. Liczbę wszystkich wypożyczeń.
5. Liczbę aktywnych wypożyczeń (te które nie zostały jeszcze zwrócone).
6. Liczbę aktywnych przeterminowanych wypożyczeń.

> [!CAUTION]
> **UWAGA DO OPCJI 12** <br/>
> Opcja 12 (`[For testing!!!] Change rental dates`) służy wyłącznie do celów demonstracyjnych i nie jest częścią docelowej funkcjonalności systemu. Pozwala przesunąć datę wypożyczenia wstecz, co umożliwia zasymulowanie przeterminowanego zwrotu bez czekania na upływ rzeczywistego czasu. Używana w scenariuszu demonstracyjnym do pokazania naliczania kary za opóźnienie (punkt 6 scenariusza).

## Scenariusz demonstracyjny
Aplikacja obsługuje pełny scenariusz demonstracyjny wymagany w treści zadania:
> *krótsza wersja scenariusza*
1. Dodanie sprzętu różnych typów (opcja 2)
2. Dodanie użytkowników różnych typów (opcja 1)
3. Poprawne wypożyczenie sprzętu (opcja 5)
4. Próba niepoprawnej operacji - wypożyczenie niedostępnego sprzętu lub przekroczenie limitu (opcja 5)
5. Zwrot sprzętu w terminie (opcja 6)
6. Zwrot opóźniony z naliczeniem kary - symulacja przez przesunięcie daty wypożyczenia wstecz (opcja 12), następnie zwrot (opcja 6)
7. Raport końcowy (opcja 11)

> *dłuższa wersja scenariusza z przykładami input/output z konsoli*
1. Dodanie sprzętu różnych typów (opcja 2)
```
Type (1 - Laptop, 2 - Projector, 3 - Camera): 1
Name: Macbook
Producer: Apple
Production year: 2025
RAM (GB): 16
Screen size: 16
Equipment added.
```
```
Type (1 - Laptop, 2 - Projector, 3 - Camera): 2
Name: Freestyle
Producer: Samsung
Production year: 2024
Brightness (lm): 230
Resolution: Full HD
Equipment added.
```
2. Dodanie użytkowników różnych typów (opcja 1)
```
Name: Jane
Surname: Doe
Type (1 - Student, 2 - Employee): 1
User added.
```
```
Name: Adam
Surname: Smith
Type (1 - Student, 2 - Employee): 2
User added.
```
3. Poprawne wypożyczenie sprzętu (opcja 5)
```
User ID: 1
Equipment ID: 1
Duration (days): 5
Rental created: 1, Macbook, Jane
```
4. Próba niepoprawnej operacji - wypożyczenie niedostępnego sprzętu lub przekroczenie limitu (opcja 5)
```
User ID: 2
Equipment ID: 1
Duration (days): 1
Equipment is already rented
```
5. Zwrot sprzętu w terminie (opcja 6)
```
Rental ID: 1
Equipment returned on time.
```
6. Zwrot opóźniony z naliczeniem kary - symulacja przez przesunięcie daty wypożyczenia wstecz (opcja 12), następnie zwrot (opcja 6)
> Nowe wypożyczenie (opcja 5)
```
User ID: 2
Equipment ID: 1
Duration (days): 2
Rental created: 2, Macbook, Adam
```
> Symulacja opóźnienia - przesunięcie daty wypozyczenia o 10 dni (opcja 12)
```
Rental ID: 2
Shift rental date back by how many days? 10
Rental date shifted. Now return the equipment to see the fine.
```
> Zwrot sprzętu (opcja 6)
```
Rental ID: 2
Rental is overdue. Fine: 400,00 zł
```
7. Raport końcowy (opcja 11)
```
*******************
*SUMMARY REPORT*
Total number of equipment: 2
Number of available equipment: 2
Number of users: 2
Total number of rentals: 2
Total number of active rentals: 0
Total number of overdue rentals: 0
*******************
```
