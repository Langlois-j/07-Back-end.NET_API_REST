# P7CreateRestApi — API REST Findexium

API REST développée dans le cadre du parcours **Développeur Back-End .NET** (OpenClassrooms — Projet 7).  
Elle simule le back-end d'une application de gestion d'instruments financiers pour la société fictive **Findexium**.

---

## Contexte

Ce projet reprend et complète un squelette de code laissé par une collègue fictive (Stéphanie).  
L'objectif est de livrer une API REST fonctionnelle, sécurisée et testée, couvrant la gestion de cinq entités financières et des utilisateurs.

---

## Stack technique

| Composant | Version |
|---|---|
| Framework | ASP.NET Core / .NET 8 |
| ORM | Entity Framework Core 8 |
| Base de données | SQL Server Express |
| Authentification | Microsoft Identity + JWT Bearer |
| Documentation API | Swagger / Swashbuckle 6.5 |
| Tests | xUnit, Moq, EF Core InMemory |

---

## Prérequis

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server Express (instance `SQLEXPRESS`)
- Visual Studio 2022 ou supérieur (recommandé)

---

## Installation et lancement

**1. Cloner le dépôt**

```bash
git clone <url-du-depot>
cd P7CreateRestApi
```

**2. Configurer la connexion à la base de données**

Dans `appsettings.json`, vérifier la chaîne de connexion :

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.\\SQLEXPRESS;Database=Projet7;Trusted_Connection=True;TrustServerCertificate=True"
}
```

**3. Appliquer les migrations Entity Framework**

```bash
dotnet ef database update
```

Cette commande crée la base de données `Projet7` et toutes les tables, y compris les tables Identity (`AspNetUsers`, `AspNetRoles`, etc.).

**4. Lancer l'application**

```bash
dotnet run
```

Ou via Visual Studio : `F5`.

L'application démarre sur `https://localhost:7210` (ou le port défini dans `launchSettings.json`).

---

## Configuration JWT

Les paramètres JWT sont définis dans `appsettings.json` :

```json
"Jwt": {
  "Issuer": "PostTradesAPI",
  "Audience": "PostTradesClient",
  "SecretKey": "..."
}
```

> ⚠️ En production, la `SecretKey` ne doit **jamais** être stockée en clair dans `appsettings.json`.  
> Elle doit être externalisée via une variable d'environnement ou un coffre-fort tel qu'**Azure Key Vault**.

---

## Authentification

L'API utilise **Microsoft Identity** pour la gestion des utilisateurs et **JWT (JSON Web Token)** pour l'authentification.

### Obtenir un token

```
POST /Login/login
```

Corps de la requête :
```json
{
  "userName": "monUtilisateur",
  "password": "MonMotDePasse1!"
}
```

Réponse :
```json
{
  "token": "eyJhbGci..."
}
```

### Utiliser le token

Ajouter l'en-tête suivant à chaque requête protégée :

```
Authorization: Bearer <token>
```

Dans Swagger, cliquer sur le bouton **Authorize** (🔒) et coller le token.

---

## Rôles et autorisations

| Rôle | Droits |
|---|---|
| `User` | Lecture seule (GET) sur toutes les entités financières |
| `Admin` | Lecture + écriture (POST, PUT, DELETE) sur toutes les entités, gestion des utilisateurs |

Les rôles sont créés automatiquement au démarrage de l'application.

La création d'un compte (`POST /User/validate`) est **ouverte sans authentification** pour permettre l'inscription initiale.

---

## Architecture

### Repositories

Le projet utilise un pattern **repository générique** (`BaseRepository<T>`) qui factorise les opérations CRUD communes (FindAll, FindById, Add, Update, Delete) pour toutes les entités financières.

Chaque entité financière possède son propre repository qui hérite de `BaseRepository<T>` et expose uniquement le `DbSet` correspondant.

`UserRepository` est traité séparément car il s'appuie sur `UserManager<User>` fourni par Microsoft Identity, dont le contrat de méthodes diffère du repository générique.

### DTOs et Mappers

Chaque entité dispose de DTO(s) dédié(s) et d'un mapper implémentant l'interface `IMapper<TEntity, TDTO>`.

Les DTOs sont **volontairement partiels** : seuls les champs nécessaires aux opérations exposées sont inclus. Ce choix respecte le principe de **minimisation des données** défini par le RGPD (Article 5).

Pour les utilisateurs, deux DTOs distincts sont utilisés :
- `UserCreateDTO` : utilisé à la création, contient le mot de passe et le rôle.
- `UserDTO` : utilisé en lecture/mise à jour, ne contient jamais le mot de passe.

### Validateurs personnalisés

Des attributs de validation custom sont définis dans le dossier `Validators/` pour couvrir des cas non gérés nativement par Data Annotations : plages de valeurs numériques, contraintes de dates, validation de mot de passe, etc.

### Base de données

Le contexte `LocalDbContext` hérite de `IdentityDbContext<User>`, ce qui génère automatiquement toutes les tables Identity (`AspNetUsers`, `AspNetRoles`, `AspNetUserRoles`, etc.) lors de la migration.

Les migrations sont gérées en **Code First** via Entity Framework Core.

---

## Tests

Le projet de tests (`P7CreateRestApi.Tests`) couvre :

| Catégorie | Contenu |
|---|---|
| **Mappers** | Tests unitaires pour les  mappers  |
| **Controllers** | Tests unitaires pour les controllers via `FakeRepository` et `FakeMapper` |
| **Repositories** | Tests d'intégration pour les repositories via EF Core InMemory |
| **Routes** | Tests d'intégration HTTP pour les  controllers via `WebApplicationFactory` |
| **Services** | Tests unitaires du `TokenService` |
| **Validators** | Tests unitaires pour les  attributs de validation personnalisés |
