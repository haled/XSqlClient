# XSqlClient
This is a DNX command-line utility for accessing an MS SQL Server database from a non-Windows system.  It uses the RC1 release of the .NET Execution environment (DNX) and the CoreCLR.

The assumption that has to be made is that the database to access has SQL Server authentication configured and the user of this utility has a valid SQL Server user name and password.  With this configuration, the tool accepts arguments for the server, database, user, password, and query to run.  The usage is:

```
XSqlClient <args>

    -s <server_name>
    -d <database_name>
    -u <user_name>
    -p <password>
    -q <query>
```

My typical call with this utility is:

`dnx run -s <server> -d <db> -u <username> -p <password> -q "quote delimited query"`