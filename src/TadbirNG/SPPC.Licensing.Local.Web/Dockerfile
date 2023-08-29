#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["SPPC.Licensing.Local.Web/SPPC.Licensing.Local.Web.csproj", "SPPC.Licensing.Local.Web/"]
COPY ["SPPC.Framework.Cryptography/SPPC.Framework.Cryptography.csproj", "SPPC.Framework.Cryptography/"]
COPY ["SPPC.Framework.Common/SPPC.Framework.Common.csproj", "SPPC.Framework.Common/"]
COPY ["SPPC.Framework.Interfaces/SPPC.Framework.Interfaces.csproj", "SPPC.Framework.Interfaces/"]
COPY ["SPPC.Framework.Service/SPPC.Framework.Service.csproj", "SPPC.Framework.Service/"]
COPY ["SPPC.Licensing.Model/SPPC.Licensing.Model.csproj", "SPPC.Licensing.Model/"]
COPY ["SPPC.Tadbir.Common/SPPC.Tadbir.Common.csproj", "SPPC.Tadbir.Common/"]
COPY ["SPPC.Tadbir.Interfaces/SPPC.Tadbir.Interfaces.csproj", "SPPC.Tadbir.Interfaces/"]
COPY ["SPPC.Tadbir.Configuration/SPPC.Tadbir.Configuration.csproj", "SPPC.Tadbir.Configuration/"]
COPY ["SPPC.Tadbir.Resources/SPPC.Tadbir.Resources.csproj", "SPPC.Tadbir.Resources/"]
COPY ["SPPC.Tadbir.ViewModel/SPPC.Tadbir.ViewModel.csproj", "SPPC.Tadbir.ViewModel/"]
COPY ["SPPC.Tadbir.Persistence/SPPC.Tadbir.Persistence.csproj", "SPPC.Tadbir.Persistence/"]
COPY ["SPPC.Framework.Persistence/SPPC.Framework.Persistence.csproj", "SPPC.Framework.Persistence/"]
COPY ["SPPC.Tadbir.Mapper/SPPC.Tadbir.Mapper.csproj", "SPPC.Tadbir.Mapper/"]
COPY ["SPPC.Tadbir.Model/SPPC.Tadbir.Model.csproj", "SPPC.Tadbir.Model/"]
COPY ["SPPC.Tadbir.Service/SPPC.Tadbir.Service.csproj", "SPPC.Tadbir.Service/"]
RUN dotnet restore "SPPC.Licensing.Local.Web/SPPC.Licensing.Local.Web.csproj"
COPY . .
WORKDIR "/src/SPPC.Licensing.Local.Web"
RUN dotnet build "SPPC.Licensing.Local.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SPPC.Licensing.Local.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SPPC.Licensing.Local.Web.dll"]