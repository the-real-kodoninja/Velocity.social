FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy everything and list for debug
COPY . .
RUN dotnet restore backend/src/WebApi/WebApi.csproj
RUN ls -R || dir  # Linux or Windows debug—see what’s there

# Build
WORKDIR /src/backend/src/WebApi
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /src/backend/src/WebApi/out ./
EXPOSE 5280
ENTRYPOINT ["dotnet", "WebApi.dll", "--urls", "http://0.0.0.0:${PORT}"]