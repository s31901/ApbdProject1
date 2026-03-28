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

- *Models* zawierają wyłącznie dane i właściwości domenowe. Klasy modelowe nie wiedzą o istnieniu serwisów ani konsoli. Przykład: Rental oblicza DueDate i IsOverdue na podstawie własnych pól - to jedyna logika która tu się pojawia, bo dotyczy bezpośrednio stanu obiektu.
- *Services* zawierają całą logikę biznesową. Serwisy nie wiedzą o istnieniu konsoli - nie ma w nich żadnych wywołań Console.WriteLine. Każdy serwis ma jedną odpowiedzialność: EquipmentService zarządza sprzętem, RentalService zarządza wypożyczeniami, UserService zarządza użytkownikami.
- *Config* (RentalPolicy) to jedyne miejsce gdzie zdefiniowane są reguły biznesowe: stawka kary za dzień opóźnienia (DailyFineRate), limity aktywnych wypożyczeń dla studenta (StudentMaxRentals = 2) i pracownika (EmployeeMaxRentals = 5). Zmiana dowolnej reguły wymaga edycji jednego miejsca w kodzie.
- *UI* (ConsoleMenu) odpowiada wyłącznie za komunikację z użytkownikiem - czyta input, wywołuje odpowiednią metodę serwisu i wyświetla wynik. Nie zawiera logiki biznesowej.
