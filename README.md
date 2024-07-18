# Project Management App

## Run docker database
- Run the following command
```
docker compose -f docker-compose-db.yml up
```

## Install EF
```
dotnet tool install --global dotnet-ef
```

## Add Ef Migration

- Add a migration to the project
```
dotnet ef migrations add <migration name>
```

- Update the database
```
dotnet ef database update 
```
