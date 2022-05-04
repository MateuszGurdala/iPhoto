# PAP21L-Z01

## Prototyp aplikacji desktopowej

### Uruchomienie

Do działania aplikacji wymagane są następujące Nuget Packages:
- Microsoft.EntityFrameworkCore 6.0.4
- Microsoft.EntityFrameworkCore.Sqlite 6.0.4
- Microsoft.EntityFrameworkCore.Tools 6.0.4
- Microsoft.Xaml.Behaviors.Wpf 1.1.39

Można je zainstalować wpisując w Package Manager Console następujące komendy:
_Install-Package Microsoft.EntityFrameworkCore
Install-Package Microsoft.EntityFrameworkCore.Sqlite
Install-Package Microsoft.EntityFrameworkCore.Tools
Install-Package Microsoft.Xaml.Behaviors.Wpf_

Aplikacje można włączyć za pomomocą pliku .exe znajdującego się pod podaną ścieżką repozytorium:
iPhoto\bin\Debug\net6.0-windows\iPhoto.exe

### Podstawowe Funkcjonalności Programu

Obecnie działająca funkcjonalność zawierająca podstawowe operacje CRUD bazy danych
znajduje się w zakładce search (ikona lupy z lewej strony ekranu).

Do dodania zdjęć do lokalnej bazy danych służy przycisk kartki z plusem
znajdująca się w prawym górnym rogu ekranu. W celu wypisania wszystkich zdjęć
należy w polu *Title wpisać %ALL i nacisnąć przyicsk lupy powyżej.
Powiekszyć zdjęcie można poprzez podwójne naciśnięcie na zdjęcie.
Po kliknięciu przycisku listy w prawym górnym rogu i wybraniu zdjęcia jest możliwość
sprawdzenia szczegółów zdjęcia.
