FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy entire repo and restore from root
COPY ../../ ./
RUN dotnet restore backend/src/WebApi/WebApi.csproj

# Build WebApi specifically
WORKDIR /app/backend/src/WebApi
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/backend/src/WebApi/out ./
EXPOSE 5280
ENTRYPOINT ["dotnet", "WebApi.dll", "--urls", "http://0.0.0.0:${PORT}"]