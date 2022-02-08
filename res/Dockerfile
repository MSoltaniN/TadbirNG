#mcr.microsoft.com/mssql/server:2017-latest ---> 2017-latest-non-root
FROM mcr.microsoft.com/mssql/server:2019-latest AS build
RUN whoami
USER root
RUN whoami
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=Pwd12345!

WORKDIR /tmp
COPY TadbirSys_CreateDbObjects.sql .
COPY SetupDefaultLogin.sql .
COPY Docker_FirstCompany.sql .
COPY Tadbir_CreateDbObjects.sql .
COPY Docker_StatesAndCities.sql .

RUN /opt/mssql/bin/sqlservr --accept-eula & sleep 100 \
	&& /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "Pwd12345!" -i /tmp/SetupDefaultLogin.sql \
	&& /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "Pwd12345!" -i /tmp/TadbirSys_CreateDbObjects.sql \
	&& /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "Pwd12345!" -i /tmp/Docker_FirstCompany.sql \
	&& /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "Pwd12345!" -i /tmp/Tadbir_CreateDbObjects.sql \
	&& /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "Pwd12345!" -i /tmp/Docker_StatesAndCities.sql \
	&& pkill sqlservr

FROM mcr.microsoft.com/mssql/server:2019-latest AS release
ENV ACCEPT_EULA=Y
EXPOSE 1433
COPY --from=build /var/opt/mssql/data /var/opt/mssql/data
