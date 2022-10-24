FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env

WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY Dulce.Heladeria.Api/*.*.csproj ./Dulce.Heladeria.Api/
COPY Dulce.Heladeria.Common/*.*.csproj ./Dulce.Heladeria.Common/
COPY Dulce.Heladeria.DataAccess/*.*.csproj ./Dulce.Heladeria.DataAccess/
COPY Dulce.Heladeria.Models/*.*.csproj ./Dulce.Heladeria.Models/
COPY Dulce.Heladeria.Repositories/*.*.csproj ./Dulce.Heladeria.Repositories/
COPY Dulce.Heladeria.Services/*.*.csproj ./Dulce.Heladeria.Services/

RUN dotnet restore

# copy everything else and build app
COPY Dulce.Heladeria.Api/. ./Dulce.Heladeria.Api/
COPY Dulce.Heladeria.Common/. ./Dulce.Heladeria.Common/
COPY Dulce.Heladeria.DataAccess/. ./Dulce.Heladeria.DataAccess/
COPY Dulce.Heladeria.Models/. ./Dulce.Heladeria.Models/
COPY Dulce.Heladeria.Repositories/. ./Dulce.Heladeria.Repositories/
COPY Dulce.Heladeria.Services/. ./Dulce.Heladeria.Services/

WORKDIR /app/Dulce.Heladeria.Api

RUN dotnet publish -c Release -o out

# RUN dotnet tool install --global dotnet-ef --version 3.1.30
# ENV PATH="$PATH:/root/.dotnet/tools"
# RUN dotnet ef database update

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/Dulce.Heladeria.Api/out ./
ENV ASPNETCORE_URLS=http://+:5005  
ENTRYPOINT ["dotnet", "Dulce.Heladeria.Api.dll"]
# Use the following instead for Heroku
#CMD ASPNETCORE_URLS=http://*:$PORT dotnet Dulce.Heladeria.Api.dll