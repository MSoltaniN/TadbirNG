@echo OFF
cls
set DeploymentFilesPath=/home/visualuser/Desktop/repo/tadbirng/src/Framework
echo connecting to remote server and updating docker compose config files ...
ssh visualuser@185.231.115.236  mkdir    %DeploymentFilesPath% 
ssh visualuser@185.231.115.236 chmod 775 %DeploymentFilesPath%
scp ..\..\src\Framework\docker-compose.override.yml visualuser@185.231.115.236:%DeploymentFilesPath%
scp ..\..\src\Framework\docker-compose.yml          visualuser@185.231.115.236:%DeploymentFilesPath%

timeout /t 5

set ServiceName=0
set /P BuildSpecific="Do you want to pull specific Service?(y/n)"
if %BuildSpecific%==y ( set /P ServiceName="Enter Service you want to pull(1.DbServer  2.ApiServer 3.WebApp 4:LicenseServer) :" ) 

cls
docker context use remote
echo login to docker hub...
docker login -u msn1368 -p 8713231222
timeout /t 5

cls
echo Pull services from docker hub...
if %ServiceName%==0 ( docker pull msn1368/db-server:dev
                      docker pull msn1368/api-server:latest
                      docker pull msn1368/web-app:dev
                      docker pull msn1368/license-server:dev
                         goto :Run
                    )
if %ServiceName%==1 ( docker pull msn1368/db-server:dev)
if %ServiceName%==2 ( docker pull msn1368/api-server:latest)
if %ServiceName%==3 ( docker pull msn1368/web-app:dev)
if %ServiceName%==4 ( docker pull msn1368/license-server:dev)

:Run
timeout /t 5
cls
echo Refresh services ...
set DeploymentFilesPath=/home/visualuser/Desktop/repo/tadbirng/src/Framework

 ssh visualuser@185.231.115.236 docker-compose   -f %DeploymentFilesPath%/docker-compose.override.yml -f %DeploymentFilesPath%/docker-compose.yml down
 ssh visualuser@185.231.115.236 docker-compose   -f %DeploymentFilesPath%/docker-compose.override.yml -f %DeploymentFilesPath%/docker-compose.yml up --no-build 

::if %ServiceName%==0 (  docker-compose -f %DeploymentFilesPath%/docker-compose.override.yml -f %DeploymentFilesPath%/docker-compose.yml up --no-build 
::                      goto :end
                             )
::if %ServiceName%==1 (   docker-compose -f %DeploymentFilesPath%/docker-compose.override.yml -f %DeploymentFilesPath%/docker-compose.yml up --no-build  sppcTadbirDB)
::if %ServiceName%==2 (   docker-compose -f %DeploymentFilesPath%/docker-compose.override.yml -f %DeploymentFilesPath%/docker-compose.yml up --no-build  sppc.tadbir.web.api)
::if %ServiceName%==3 (   docker-compose -f %DeploymentFilesPath%/docker-compose.override.yml -f %DeploymentFilesPath%/docker-compose.yml up --no-build  sppcTadbirWebNew)
::if %ServiceName%==4 (   docker-compose -f %DeploymentFilesPath%/docker-compose.override.yml -f %DeploymentFilesPath%/docker-compose.yml up --no-build  sppcLicensingLocalWeb)

:end
docker context use default

timeout /t 20
PAUSE



