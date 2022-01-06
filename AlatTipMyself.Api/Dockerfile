FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copy csjoj file and restore dependencies via Nuget
COPY *.csproj  ./
RUN dotnet restore

# Copy project files and build release
COPY . ./
RUN dotnet publish -c Release -o out

#Generate runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 80
ENTRYPOINT [ "dotnet", "AlatTipMyself.Api.dll" ]