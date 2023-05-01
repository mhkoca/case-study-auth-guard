# case-study-auth-guard

I created 2 APIs: EmployeeAPI and AuthGuardAPI. 

AuthGuardAPI is responsible for generating the JWT Token with the login endpoint and determining the validity of this token with the Authenticate endpoint. EmployeAPI, on the other hand, is a service that enables performing CRUD operation on employees.

To authenticate requests on EmployeeAPI, I send the Bearer token to AuthGuard service with my custom middleware(AuthorizationMiddleware) and check if the token is valid.

I used SQLite as database. When you open the solution, it is enough to run the ```add-migration migration_name``` and ```update-database``` commands in both API projects. There is a User table in the database created for AuthGuard. Employee table in the Employee database. Tables will be seeded with a few records when the migration is done. 

You can use the following parameters to login and get token.
```
{
  "email": "test@test.com",
  "password": "password"
}
```
