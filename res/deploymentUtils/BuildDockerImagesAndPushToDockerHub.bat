@echo OFF

set /P BatIsInRightPath="Is '%CD%' your Docker compose Dir?(y/n)"
if %BatIsInRightPath%==n ( set /P DockerComposeDir="Enter Docker compose Dir:" ) else ( set /p DockerComposeDir=%CD%)


cls
echo Change path to 
cd %DockerComposeDir%
echo %DockerComposeDir%
timeout /t 2

cls
echo Build services...
docker context use default
set ServiceName=0
set /P BuildSpecific="Do you want to build specific Service?(y/n)"
if %BuildSpecific%==y ( set /P ServiceName="Enter Service you want to build(1.sppcTadbirDB  2.sppc.tadbir.web.api 3.sppcTadbirWebNew 4.sppcLicensingLocalWeb) :" ) 

if %ServiceName%==0 (docker-compose -f docker-compose.override.yml -f docker-compose.yml build  
                               goto :push
                             )
if %ServiceName%==1 (docker-compose -f docker-compose.override.yml -f docker-compose.yml build sppcTadbirDB)
if %ServiceName%==2 (docker-compose -f docker-compose.override.yml -f docker-compose.yml build sppc.tadbir.web.api)
if %ServiceName%==3 (docker-compose -f docker-compose.override.yml -f docker-compose.yml build sppcTadbirWebNew)
if %ServiceName%==4 (docker-compose -f docker-compose.override.yml -f docker-compose.yml build sppcLicensingLocalWeb)

:push 
cls
echo login to docker hub...
PAUSE

docker login -u msn1368 -p 8713231222
echo Push services to docker hub...
PAUSE
if %ServiceName%==0 (docker push msn1368/sppctadbirdb:dev
                    docker push msn1368/sppctadbirwebnew:dev
                    docker push msn1368/sppctadbirwebapi:latest
                    docker push msn1368/sppclicensinglocalweb:dev
                               goto :end
                             )
if %ServiceName%==1 (docker push msn1368/sppctadbirdb:dev)
if %ServiceName%==2 (docker push msn1368/sppctadbirwebapi:latest)
if %ServiceName%==3 (docker push msn1368/sppctadbirwebnew:dev)
if %ServiceName%==4 (docker push msn1368/sppclicensinglocalweb:dev)
   

:end
PAUSE