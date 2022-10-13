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

Project contains a few main tabs with functionalities: 

- Albums - page containing a list of albums. User is able to add albums with custom name and color.
           Albums can be then edited or deleted. Album can display info such as: total size, amount of photos,
           and hashtags of photos inside the album.

- Photo browser - there user can search for any photo he added in application. Browser can also be accessed inside certain album.
        By pushing button with plus icon in top right corner user can choose and add photo.
        Before adding photo user can change its details. To display all photos from all albums type
        %ALL in *Title form. By double clicking on photo user can desplay it in full scale.
        With right clicking on photo user is able to edit or delete it. On the right user can 
        expand panel which inform about photo details.
        
- Places - contatins map where user can create places where user took pictures. Every created place
           contains information about how many photos were took there. User can switch to list of places
           by using arrow button.

- Profile - contains information about online functionalities of application. User can display 
            online albums created and shared among group of users. Users can create online albums and register
            only using website.

        
## Possible future improvements
  
[(Back to top)](#table-of-contents)

- Add automated tests - project does not contain any automated tests, therefore there still could be some bugs
- test desktop application with attention to performance
- expand functionalities - one of functionalities which was in plans, but was not included in final release are
                favourite albums and photos.
- create now web application - there are plans to rewrite django application in ASP.NET MVC.


## Screenshots

[(Back to top)](#table-of-contents)

#### Below are some screenshot from final version of desktop application. In screenshots folder you can also find more images from prototipe version of application, web application prototipe and validations.  

<strong>TABS</strong>:
<br>
<p align="center" width="100%">
<img alt="Screenshot of logo" src="https://raw.github.com/GreysonKrystian/iPhoto/master/screenshots/iphotos_logo.png" width=1000 height= auto>
<br>
<br>
<img alt="Screenshot of albums" src="https://raw.github.com/GreysonKrystian/iPhoto/master/screenshots/iphotos_albumy.png" width=1000 height= auto>
<br>
<br>
<img alt="Screenshot of account" src="https://raw.github.com/GreysonKrystian/iPhoto/master/screenshots/iphotos_konto.png" width=1000 height= auto>
<br>
<br>
<img alt="Screenshot of places" src="https://raw.github.com/GreysonKrystian/iPhoto/master/screenshots/iphotos_miejsca.png" width=1000 height= auto>
<br>
<br>
<img alt="Screenshot of all photos" src="https://raw.github.com/GreysonKrystian/iPhoto/master/screenshots/iphotos_zdjecie.png" width=1000 height= auto>
</p>

<strong>LOCAL DATABASE</strong>:
<br>
<p align="center" width="100%">
<img alt="Screenshot of db diagram" src="https://raw.github.com/GreysonKrystian/iPhoto/master/screenshots/Diagram_.png" width=800 height= auto>
 <br>
 <br>
</p>

## License

[(Back to top)](#table-of-contents)


The MIT License (MIT) 2022 - [Krystian Grela](https://github.com/GreysonKrystian/), [Mateusz Gurdała](https://github.com/MateuszGurdala/), [Igor Kraszewski](https://github.com/kraszor/) . Please have a look at the [LICENSE.md](LICENSE.md) for more details.





