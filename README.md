# XSqlClient
This is a DNX command-line utility for accessing an MS SQL Server database.

One of the utilities that I've wanted to write since I learned about DNX is a command-line SQL client.  Quite often, I need to run exploratory queries to investigate data, usually when troubleshooting.  It is a pain to switch from my editor of choice (Emacs) on my Mac to a virtual machine so that I can run a basic SELECT statement.

Now that .NET Core supports native SQL Server access, I'm creating a cross-platform client to access SQL Server.