FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY src/SmartAC.Domain/*.csproj ./src/SmartAC.Domain/
COPY src/SmartAC.Infrastructure/*.csproj ./src/SmartAC.Infrastructure/
COPY src/SmartAC.Web/*.csproj ./src/SmartAC.Web/
COPY *.sln ./

# Copy everything else and build
COPY . ./
WORKDIR /app/src/SmartAC.Web
RUN dotnet publish -c Release -o ../../out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "SmartAC.Web.dll"]