@ECHO OFF

cd .\SPPC.Tadbir.Tests

ECHO Running unit tests using NUnit Test Adapter...
dotnet test SPPC.Tadbir.Tests.csproj
