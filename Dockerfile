FROM mcr.microsoft.com/dotnet/sdk:5.0

MAINTAINER Yazan Kassam, yazankassam.codavia@gmail.com

WORKDIR /app

ENV ASPNETCORE_URLS="http://interaction_ms"

ENV ASPNETCORE_ENVIRONMENT=Development

ENV ConnectionStrings:DefaultConnection="Server=mysql_interaction_ms;Database=interaction_ms_db;Uid=codavia;Pwd=cod@v!@; convert zero datetime=True"

ENV ConsulConfig:Host="http://consul:8500"

EXPOSE 5016

COPY ./publish .

ENTRYPOINT ["dotnet","iread_interaction_ms.dll"]
