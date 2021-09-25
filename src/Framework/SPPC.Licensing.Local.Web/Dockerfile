#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
#COPY ["nuget.config", "."]
COPY ["SPPC.Licensing.Local.Web/SPPC.Licensing.Local.Web.csproj", "SPPC.Licensing.Local.Web/"]
COPY ["SPPC.Licensing.Model/SPPC.Licensing.Model.csproj", "SPPC.Licensing.Model/"]
COPY ["SPPC.Framework.Interfaces/SPPC.Framework.Interfaces.csproj", "SPPC.Framework.Interfaces/"]
COPY ["SPPC.Framework.Common/SPPC.Framework.Common.csproj", "SPPC.Framework.Common/"]
COPY ["SPPC.Framework.Cryptography/SPPC.Framework.Cryptography.csproj", "SPPC.Framework.Cryptography/"]
COPY ["SPPC.Framework.Service/SPPC.Framework.Service.csproj", "SPPC.Framework.Service/"]
COPY ["SPPC.Licensing.Local.Persistence/SPPC.Licensing.Local.Persistence.csproj", "SPPC.Licensing.Local.Persistence/"]
COPY ["SPPC.Tadbir.Interfaces/SPPC.Tadbir.Interfaces.csproj", "SPPC.Tadbir.Interfaces/"]
COPY ["SPPC.Tadbir.Common/SPPC.Tadbir.Common.csproj", "SPPC.Tadbir.Common/"]
COPY ["SPPC.Tadbir.ViewModel/SPPC.Tadbir.ViewModel.csproj", "SPPC.Tadbir.ViewModel/"]
COPY ["SPPC.Tadbir.Configuration/SPPC.Tadbir.Configuration.csproj", "SPPC.Tadbir.Configuration/"]
COPY ["SPPC.Licensing.Interfaces/SPPC.Licensing.Interfaces.csproj", "SPPC.Licensing.Interfaces/"]
RUN dotnet restore "SPPC.Licensing.Local.Web/SPPC.Licensing.Local.Web.csproj"
#COPY . .
WORKDIR "/src/SPPC.Licensing.Local.Web"
RUN dotnet build "SPPC.Licensing.Local.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SPPC.Licensing.Local.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SPPC.Licensing.Local.Web.dll"]