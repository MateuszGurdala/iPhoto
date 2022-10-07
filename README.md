# IPhotos

# Table of contents


- [Requirements](#requirements)
- [Project description](#project-description)
- [Possible future improvements](#possible-future-improvements)
- [Screenshots](#screenshots)
- [License](#license)


## Requirements

[(Back to top)](#table-of-contents)

Application uses and requires following Nuget packages:
- Microsoft.EntityFrameworkCore 6.0.4
- Microsoft.EntityFrameworkCore.Sqlite 6.0.4
- Microsoft.EntityFrameworkCore.Tools 6.0.4
- Microsoft.Xaml.Behaviors.Wpf 1.1.39
- GMap.NET.Core
- GMap.NET.WinPresentation
- Google.Apis.Drive.v3

Aplication can be run using .exe file provided in repo under this path:
iPhoto\bin\Debug\net6.0-windows\iPhoto.exe

## Project description

[(Back to top)](#table-of-contents)

iPhotos is an group project which main purspose is to simplify management of photos. The project consist of two subprojects:
Desktop application in .NET using WPF Framework, with use of SQLite as offline database and 
web application written in Python framework - Django. Web application can be found [here](https://github.com/kraszor/Iphotos).

Authors of desktop application:

[Krystian Grela](https://github.com/GreysonKrystian/),
[Mateusz Gurdała](https://github.com/MateuszGurdala/)

Author of web application:

[Igor Kraszewski](https://github.com/kraszor/)

Program dzieli się na podstrony oznaczone odpowiednimi ikonami:

- Strona startowa - strona zawierająca ...

- Albumy - strona zawierająca listę albumów, użytkownik może dodawać albumy z wybraną przez
        siebie nazwą i kolorem. Albumy można edytować i usuwać po utworzeniu. Album wyświetla
        informacje o ilości zdjęć, rozmiarze i hashtagach jakie posiadają zdjęcia wewnątrz albumu.
        Zawartość albumu posiada taką samą funkcjonalność jak wyszukiwarka zdjęć opisania poniżej
        z tą różnicą że wyszukiwanie występuje w obrębie albumu.

- Wyszukiwarka zdjęć - główny element naszej aplikacji desktopowej. Odpowiada za CRUD zdjęć.
        Do dodania zdjęć do lokalnej bazy danych służy przycisk kartki z plusem
        znajdująca się w prawym górnym rogu ekranu. W celu wypisania wszystkich zdjęć
        należy w polu *Title wpisać %ALL i nacisnąć przyicsk lupy powyżej.
        Powiekszyć zdjęcie można poprzez podwójne naciśnięcie na zdjęcie.
        Po kliknięciu przycisku listy w prawym górnym rogu i wybraniu zdjęcia jest możliwość
        sprawdzenia szczegółów zdjęcia. Kliknięcie prawym przyciskiem na zdjęcie umożliwia
        zmianę szczegółów zdjęcia lub jego usunięcie. Z prawej strony można rozwinąć panel odpowiedzialny
        za informowanie użytkownika o szczegółach zdjęcia.

- Miejsca - zawiera mapę na której można tworzyć miejsca w których zostały utworzone miejsca. Każde miejsce
        posiada informacje o ilości zdjęć tam wykonanych. Zmianę widoku z dodawania miejsc do listy miejsc
        można wykonać za pomocą przycisku strzałki w górnej części aplikacji.


- Profil - Zawiera informacje o części online naszej aplikacji. Posiada informacje o albumach stworzonych
        online dla grup użytkowników. Umożliwia też przeniesienie na stronę rejestracji w webowej części
        aplikacji.
        
## Possible future improvements
  
[(Back to top)](#table-of-contents)

- Add automated tests - project does not contain any automated tests, therefore there still could be some bugs
- test desktop application with attention to performance
- expand functionalities - 
- create now web application - there are plans to rewrite django application in ASP.NET MVC.


## Screenshots

[(Back to top)](#table-of-contents)

[(Back to top)](#table-of-contents)

<strong></strong>:
<br>
<p align="center" width="100%">
<img alt="Screenshot from menu" src="https://raw.github.com/GreysonKrystian/Minesweeper/master/Example%20Photos/Menu_1.png" width=800 height= auto>
<br>
<br>
<img alt="Screenshot from menu" src="https://raw.github.com/GreysonKrystian/Minesweeper/master/Example%20Photos/Menu_2.png" width=800 height= auto>
</p>

<strong></strong>:
<br>
<p align="center" width="100%">
<img alt="Screenshot of gameplay" src="https://raw.github.com/GreysonKrystian/Minesweeper/master/Example%20Photos/Gameplay_1.png" width=800 height= auto>
 <br>
 <br>
<img alt="Screenshot of gameplay" src="https://raw.github.com/GreysonKrystian/Minesweeper/master/Example%20Photos/Gameplay_Won.png" width=800 height= auto> 
<br>
<br>
<img alt="Screenshot of gameplay" src="https://raw.github.com/GreysonKrystian/Minesweeper/master/Example%20Photos/Game_Ended.png" width=800 height= auto>
<br>
<br>
</p>

## License

[(Back to top)](#table-of-contents)


The MIT License (MIT) 2022 - [Krystian Grela](https://github.com/GreysonKrystian/), [Mateusz Gurdała](https://github.com/MateuszGurdala/), [Igor Kraszewski](https://github.com/kraszor/) . Please have a look at the [LICENSE.md](LICENSE.md) for more details.





