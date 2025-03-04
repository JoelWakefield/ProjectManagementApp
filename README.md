# Project Management App

This app is an example of what a project management app could look like. It is meant as an example, but currently lacks many features which would make it production ready; the main goal of the project is to have something to work on during unemployment, because any experience is good experience.

> NOTE: Currently, this project is transitioning to using an api for the blazor calls; the end goal being a project which has a react client and blazor client.

## Instructions

### Prerequisits
- [Docker](https://www.docker.com/products/docker-desktop/)
- [Visual Studio](https://visualstudio.microsoft.com/downloads/) (recommended)
- [DBeaver](https://dbeaver.io/download/) (recommended)

### Run docker database
- Compose the docker container
```
docker compose -f docker-compose-db.yml up
```

### Install EF
- Necessary for data migration
```
dotnet tool install --global dotnet-ef
```

### Add Ef Migration
- Move down into project solution
```
cd ProjectManagementApp
```

- Add a migration to the project
```
dotnet ef migrations add <migration name>
```

- Update the database (ensure the docker container is running)
```
dotnet ef database update 
```
