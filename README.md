# 🐾 VetApp

Aplikacja VetApp umożliwia użytkownikom czasowe przygarnięcie pieska ze schroniska, aby opiekować się nim, udomowić i potencjalnie adoptować na stałe. Celem aplikacji jest wspieranie adopcji oraz poprawa jakości życia psów w schroniskach.
 
 
## 🚀 Technologie
  - Backend: ASP.NET MVC (C#)
  - Baza danych: SQL Server

## 🎯 Funkcje
  - Rejestracja i logowanie użytkowników
  - Zarządzanie kontem
  - Przygarnięcie pupila
  - Zarządzanie bazą zwierząt przez Administratora

## 🛠️ Instrukcja instalacji
  - Sklonuj repozytorium
  ```
  git clone https://github.com/Projekt-C/VetApp.git
  ```
  - Otwórz projekt w Visual Studio.
  - Przygotuj bazę danych (np. SQL Server).
  ```
  EntityFrameworkCore\update-database
  ```
  - Skonfiguruj connection string w pliku appsettings.json.
  ```
  "DevConnection": "Server=YOUR_SERVER_NAME;Database=VetAppDb;Trusted_Connection=True;TrustServerCertificate=True;"
  ```

## 👓 Domyślne konto Administratora
  - Email: `admin@example.com`
  - Hasło: `Admin123!.`
