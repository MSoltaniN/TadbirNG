FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY SPPC.Tadbir.Web.Api/SPPC.Tadbir.Web.Api.csproj SPPC.Tadbir.Web.Api/
COPY SPPC.Tadbir.CrossCutting/SPPC.Tadbir.CrossCutting.csproj SPPC.Tadbir.CrossCutting/
COPY SPPC.Tadbir.Common/SPPC.Tadbir.Common.csproj SPPC.Tadbir.Common/
COPY SPPC.Framework.Common/SPPC.Framework.Common.csproj SPPC.Framework.Common/
COPY SPPC.Framework.Interfaces/SPPC.Framework.Interfaces.csproj SPPC.Framework.Interfaces/
COPY SPPC.Tadbir.Interfaces/SPPC.Tadbir.Interfaces.csproj SPPC.Tadbir.Interfaces/
COPY SPPC.Licensing.Model/SPPC.Licensing.Model.csproj SPPC.Licensing.Model/
COPY SPPC.Tadbir.ViewModel/SPPC.Tadbir.ViewModel.csproj SPPC.Tadbir.ViewModel/
COPY SPPC.Tadbir.Configuration/SPPC.Tadbir.Configuration.csproj SPPC.Tadbir.Configuration/
COPY SPPC.Framework.Persistence/SPPC.Framework.Persistence.csproj SPPC.Framework.Persistence/
COPY SPPC.Framework.Model/SPPC.Framework.Model.csproj SPPC.Framework.Model/
COPY SPPC.Tadbir.Service/SPPC.Tadbir.Service.csproj SPPC.Tadbir.Service/
COPY SPPC.Framework.Service/SPPC.Framework.Service.csproj SPPC.Framework.Service/
COPY SPPC.Tadbir.Resources/SPPC.Tadbir.Resources.csproj SPPC.Tadbir.Resources/
COPY SPPC.Tadbir.Persistence/SPPC.Tadbir.Persistence.csproj SPPC.Tadbir.Persistence/
COPY SPPC.Tadbir.Model/SPPC.Tadbir.Model.csproj SPPC.Tadbir.Model/
COPY SPPC.Framework.Cryptography/SPPC.Framework.Cryptography.csproj SPPC.Framework.Cryptography/
COPY SPPC.Licensing.Local.Persistence/SPPC.Licensing.Local.Persistence.csproj SPPC.Licensing.Local.Persistence/
COPY SPPC.Licensing.Interfaces/SPPC.Licensing.Interfaces.csproj SPPC.Licensing.Interfaces/
COPY SPPC.Tadbir.Mapper/SPPC.Tadbir.Mapper.csproj SPPC.Tadbir.Mapper/
RUN dotnet restore SPPC.Tadbir.Web.Api/SPPC.Tadbir.Web.Api.csproj
COPY . .
WORKDIR /src/SPPC.Tadbir.Web.Api
RUN dotnet build SPPC.Tadbir.Web.Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish SPPC.Tadbir.Web.Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SPPC.Tadbir.Web.Api.dll"]
