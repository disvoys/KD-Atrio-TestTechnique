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

