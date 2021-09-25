@echo OFF
cls
echo connecting to remote server and makes things ready to send...
ssh visualuser@185.231.115.236  mkdir /var/lib/docker/volumes/framework_productdata/_data/scripts  
ssh visualuser@185.231.115.236 chmod 775 /var/lib/docker/volumes/framework_productdata/_data/scripts

PAUSE
cls
echo sending update file...
scp ..\TadbirSys_UpdateDbObjects.sql visualuser@185.231.115.236:/var/lib/docker/volumes/framework_productdata/_data/scripts

timeout /t 7
cls
echo running update script....
::ssh visualuser@185.231.115.236 systemctl enable --now cockpit.socket
docker context use remote
docker exec -u mssql -it framework_sppcTadbirDB_1   /opt/mssql-tools/bin/sqlcmd -S localhost -U NgTadbirUser -P "Demo1234" -i /var/opt/mssql/data/scripts/TadbirSys_UpdateDbObjects.sql
docker context use default

PAUSE