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

## Adding Roles
> NOTE: When using `AddIdentityCore`, it's important to call the `AddRoles` before `AddEntityFrameworkStores`.
```
builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();
```