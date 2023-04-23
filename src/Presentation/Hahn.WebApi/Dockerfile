#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Application/Hahn.Application.Contract/Hahn.Application.Contract.csproj", "src/Application/Hahn.Application.Contract/"]
COPY ["src/Application/Hahn.Application.Services/Hahn.Application.Services.csproj", "src/Application/Hahn.Application.Services/"]
COPY ["src/Domain/Hahn.Domain.Model/Hahn.Domain.Model.csproj", "src/Domain/Hahn.Domain.Model/"]
COPY ["src/Framework/Hahn.Framework.Domain/Hahn.Framework.Domain.csproj", "src/Framework/Hahn.Framework.Domain/"]
COPY ["src/Infrastructure/Hahn.Infrastructure.EfCore/Hahn.Infrastructure.EfCore.csproj", "src/Infrastructure/Hahn.Infrastructure.EfCore/"]
COPY ["src/Presentation/Hahn.WebApi/Hahn.WebApi.csproj", "src/Presentation/Hahn.WebApi/"]
RUN dotnet restore "src/Presentation/Hahn.WebApi/Hahn.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Presentation/Hahn.WebApi"
RUN dotnet build "Hahn.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hahn.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hahn.WebApi.dll"]