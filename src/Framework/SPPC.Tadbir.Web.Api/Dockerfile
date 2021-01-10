#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM microsoft/aspnetcore:2.0-stretch AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0-stretch AS build
WORKDIR /src
COPY ["SPPC.Tadbir.Web.Api/SPPC.Tadbir.Web.Api.csproj", "SPPC.Tadbir.Web.Api/"]
COPY ["SPPC.Tadbir.CrossCutting/SPPC.Tadbir.CrossCutting.csproj", "SPPC.Tadbir.CrossCutting/"]
COPY ["SPPC.Tadbir.Common/SPPC.Tadbir.Common.csproj", "SPPC.Tadbir.Common/"]
COPY ["SPPC.Framework.Common/SPPC.Framework.Common.csproj", "SPPC.Framework.Common/"]
COPY ["SPPC.Framework.Interfaces/SPPC.Framework.Interfaces.csproj", "SPPC.Framework.Interfaces/"]
COPY ["SPPC.Framework.Persistence/SPPC.Framework.Persistence.csproj", "SPPC.Framework.Persistence/"]
COPY ["SPPC.Framework.Model/SPPC.Framework.Model.csproj", "SPPC.Framework.Model/"]
COPY ["SPPC.Tadbir.Service/SPPC.Tadbir.Service.csproj", "SPPC.Tadbir.Service/"]
COPY ["SPPC.Framework.Service/SPPC.Framework.Service.csproj", "SPPC.Framework.Service/"]
COPY ["SPPC.Tadbir.Interfaces/SPPC.Tadbir.Interfaces.csproj", "SPPC.Tadbir.Interfaces/"]
COPY ["SPPC.Tadbir.ViewModel/SPPC.Tadbir.ViewModel.csproj", "SPPC.Tadbir.ViewModel/"]
COPY ["SPPC.Tadbir.Resources/SPPC.Tadbir.Resources.csproj", "SPPC.Tadbir.Resources/"]
COPY ["SPPC.Tadbir.Configuration/SPPC.Tadbir.Configuration.csproj", "SPPC.Tadbir.Configuration/"]
COPY ["SPPC.Tadbir.Persistence/SPPC.Tadbir.Persistence.csproj", "SPPC.Tadbir.Persistence/"]
COPY ["SPPC.Workflow.Persistence/SPPC.Workflow.Persistence.csproj", "SPPC.Workflow.Persistence/"]
COPY ["SPPC.Tadbir.Model/SPPC.Tadbir.Model.csproj", "SPPC.Tadbir.Model/"]
COPY ["SPPC.Tadbir.Mapper/SPPC.Tadbir.Mapper.csproj", "SPPC.Tadbir.Mapper/"]
RUN dotnet restore "SPPC.Tadbir.Web.Api/SPPC.Tadbir.Web.Api.csproj"
COPY . .
WORKDIR "/src/SPPC.Tadbir.Web.Api"
RUN dotnet build "SPPC.Tadbir.Web.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SPPC.Tadbir.Web.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SPPC.Tadbir.Web.Api.dll"]