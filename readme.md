# NineTham Backend

A Backend service for NineTham made up of ASP.NET 6 Web APIs and SQL Server on Docker

## Run Locally

Prefer Powershell for following command

Install dotnet tool

```bash
  dotnet tool install --global dotnet-ef
```

Pull Microsoft SQL Server image

```bash
  docker pull mcr.microsoft.com/mssql/server
```

Run the container (with docker volumn to persist data)

```bash
  docker run --rm --name sql_server -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=1234abCD' -e "MSSQL_PID=Express" -p 1433:1433 -v sql_persist_volume:/var/opt/mssql -d mcr.microsoft.com/mssql/server
```

Migration

```bash
  dotnet-ef migrations add ${name}
  dotnet-ef database update
```

Exec sqlcmd client

```bash
  docker exec -it sql_server /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 1234abCD
```

Or connect with SSMS with

```bash
  127.0.0.1,1433
```

## Authors

- [@Pakorn Thanaprachanon (62010694)](https://github.com/T-Pakorn)
- [@Tanapol Wong-asa](https://github.com/ApexTone)
