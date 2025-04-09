FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy only WebApi files
COPY . ./
RUN dotnet restore WebApi.csproj

# Build
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
EXPOSE 5280
ENTRYPOINT ["dotnet", "WebApi.dll", "--urls", "http://0.0.0.0:${PORT}"]