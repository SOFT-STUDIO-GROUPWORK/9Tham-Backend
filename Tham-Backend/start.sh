#!/bin/sh
echo "Start 9Tham Back-end"
dotnet ef migrations remove --force
dotnet ef database update 0
docker volume prune --force
docker run --rm --name sql_server -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=1234abCD' -e "MSSQL_PID=Express" -p 1433:1433 -v sql_persist_volume:/var/opt/mssql -d mcr.microsoft.com/mssql/server
dotnet ef migrations add FinalMigration
dotnet ef database update