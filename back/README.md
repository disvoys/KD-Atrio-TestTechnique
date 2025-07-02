# 📚 Kdatrio

Une API REST pour gérer des personnes et leurs emplois, construite avec **.NET** et **Entity Framework**, conteneurisée avec **Docker**.

---

## 🚀 Démarrage rapide

```bash
cd back
```

### 0️⃣ Restaurer le projet

```bash
dotnet restore back.csproj
```

### 1️⃣ Construire l'image Docker

```bash
docker build -t kdatrio .
```

### 2️⃣ Lancer le conteneur

```bash
docker run -p 5000:8080 -v "%cd%/people.db:/app/people.db" kdatrio
```

L’API sera disponible ici :
👉 [http://localhost:5000/api-doc/index.html](http://localhost:5000/api-doc/index.html)

---

## 🗄️ Gestion de la base de données

Pour réinitialiser la base et appliquer les migrations :

```bash
dotnet ef database drop
dotnet ef migrations add Init
dotnet ef database update
```

---

## ✅ Fonctionnalités

* **Création d’une personne**

  * Personnes de moins de 150 ans uniquement, sinon erreur. ✔️

* **Lister toutes les personnes**

  * Par ordre alphabétique et âge affiché. ✔️

* **Lister toutes les personnes avec leurs emplois actuels** ✔️

* **Ajouter un emploi**

  * Début et fin requis, sauf pour un emploi actuel (date de fin facultative). ✔️
  * Plusieurs emplois peuvent se chevaucher. ✔️

* **Lister toutes les personnes ayant travaillé pour une entreprise donnée** ✔️

* **Lister tous les emplois d’une personne entre deux dates**

  * 🟡 Partiellement fonctionnel.

---

## 📌 Notes

* La base `people.db` est montée en volume.

---

## Curl test

### Récupérer l'ensemble des personnes, par ordre alphabetique, avec leur age et job actuel:
```bash
curl -X 'GET' \
  'http://localhost:5000/api/Person' \
  -H 'accept: text/plain'
```

### Récupérer les informations d'une personne 
```bash
curl -X 'GET' \
  'http://localhost:5000/api/Person/1' \
  -H 'accept: text/plain'
```

### Ajouter une personne
```bash
curl -X 'POST' \
  'http://localhost:5000/api/Person' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "firstName": "Alix",
  "lastName": "Terieur",
  "dateOfBirth": "10/03/1960"
}'
```

### l'api retourne une erreur lorsqu'une personne ajoutée a plus de 150 ans.
```bash
curl -X 'POST' \
  'http://localhost:5000/api/Person' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "firstName": "Michel",
  "lastName": "Blanc",
  "dateOfBirth": "10/03/1800"
}'
```


### Assigner un job à une personne à partir de son ID
```bash
curl -X 'POST' \
  'http://localhost:5000/api/Person/1/jobs' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "companyName": "google",
  "position": "developer",
  "startDate": "10/06/2022",
  "endDate": "20/06/2024"
}'
```

### Assigner un job à une personne à partir de son ID mais sans date de fin, job actuel
```bash
curl -X 'POST' \
  'http://localhost:5000/api/Person/1/jobs' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "companyName": "atrio",
  "position": "intern",
  "startDate": "21/06/2026",
  "endDate": null
}'
```


### Récupérer les employés d'une entreprise
```bash
curl -X 'GET' \
  'http://localhost:5000/api/Person/bycompany?companyName=atrio' \
  -H 'accept: text/plain'
```

### Renvoie les emplois d'une personne entre une plage de deux dates
```bash
curl -X 'GET' \
  'http://localhost:5000/api/Person/1/jobsbetween?start=01%2F01%2F2024&end=01%2F01%2F2026' \
  -H 'accept: text/plain'
```


