# Use official .NET SDK image to build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy csproj and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Use runtime image for the final app
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Expose port (Render assigns PORT env var)
EXPOSE 5280

# Start the app
ENTRYPOINT ["dotnet", "WebApi.dll", "--urls", "http://0.0.0.0:${PORT}"]
