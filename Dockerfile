FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY NumericalAnalysisApi/NumericalAnalysisApi.csproj NumericalAnalysisApi/
RUN dotnet restore NumericalAnalysisApi/NumericalAnalysisApi.csproj

# Copy everything else and build
COPY NumericalAnalysisApi/ NumericalAnalysisApi/
RUN dotnet publish NumericalAnalysisApi/NumericalAnalysisApi.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
EXPOSE 8080

# Copy published output
COPY --from=build /app/publish .

# Set environment variables
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://0.0.0.0:8080

# Run the app
ENTRYPOINT ["dotnet", "NumericalAnalysisApi.dll"]