
# BPS - Backend State-of-the-art Project Starter Kit 🌐
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=7peakssoftware_bps-backend-boilerplate-api&metric=alert_status&token=3b2afaf8e978a6980ba5cdf564cacefc047a1bc5)](https://sonarcloud.io/summary/new_code?id=7peakssoftware_bps-backend-boilerplate-api)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=7peakssoftware_bps-backend-boilerplate-api&metric=coverage&token=3b2afaf8e978a6980ba5cdf564cacefc047a1bc5)](https://sonarcloud.io/summary/new_code?id=7peakssoftware_bps-backend-boilerplate-api)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=7peakssoftware_bps-backend-boilerplate-api&metric=sqale_rating&token=3b2afaf8e978a6980ba5cdf564cacefc047a1bc5)](https://sonarcloud.io/summary/new_code?id=7peakssoftware_bps-backend-boilerplate-api)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=7peakssoftware_vnu-bps-backend-boilerplate-api&metric=vulnerabilities&token=3b2afaf8e978a6980ba5cdf564cacefc047a1bc5)](https://sonarcloud.io/summary/new_code?id=7peakssoftware_bps-backend-boilerplate-api)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=7peakssoftware_bps-backend-boilerplate-api&metric=security_rating&token=3b2afaf8e978a6980ba5cdf564cacefc047a1bc5)](https://sonarcloud.io/summary/new_code?id=7peakssoftware_bps-backend-boilerplate-api)
---

## 🌟 Introduction

Welcome to the BPS - Backend - State of the Art - Project Starter Kit! This project serves as the backbone for the BPS platform.

This project houses the Backoffice for BPS Application. For a more detailed overview, please refer to our [documentation](#) (To be added).

---


### Application Configuration 🛠️

Make sure your environment is Development:

Set the `ASPNETCORE_ENVIRONMENT` variable to `Development` for the `SevenPeaks.Employee.Api`.


If you have a different envrionment run:
```
$env:ASPNETCORE_ENVIRONMENT="Development"
```
---
## 🛠️ Setting up the Database 🐳

Migration are saved under `Infrastructure\Persistence\Migrations` folder.

To add a new migration you can run the following command on the root folder of the project:


then run the following:

```
dotnet ef migrations add InitialSchema -o .\Infrastructure\Persistence\Migrations\

dotnet ef database update
```
---
## 🧪 Local Testing

### Quality Gate Requirement 🛡️

Ensure you meet the [SonarCloud Quality Gate](https://sonarcloud.io/dashboard?id=7peakssoftware_bps-backend-boilerplate-api) by running the test coverage reporting.

#### With Visual Studio 🖥️

- Refer to [Microsoft's Official Documentation](https://docs.microsoft.com/en-us/visualstudio/test/using-code-coverage-to-determine-how-much-code-is-being-tested?view=vs-2019).

#### With Console ⌨️

- On Windows:
  ```shell
  dotnet test -c Release /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=<YOUR_LOCAL_DIRECTORY>/
  ```
- On Linux:
  ```shell
  dotnet test -c Release --collect:"XPlat Code Coverage" /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=<YOUR_LOCAL_DIRECTORY>/
  ```

See the results in the standard output.

---
### Codecoverage Report  ⌨️

To generate a code coverage report for the project, you can use the `reportgenerator` tool. Follow the steps below:
### Prerequisites

Make sure you have the following prerequisites installed:

- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [reportgenerator](https://danielpalme.github.io/ReportGenerator/)

### Generate Code Coverage Report

1. Open a terminal or command prompt.

2. Navigate to the root directory of your project.

3. Run the following command:

```shell
reportgenerator -reports:"test/**/coverage.opencover.xml" -targetdir:"coveragereport" -reporttypes:Html
 ```

## 🚀 How to Contribute

To contribute, follow these steps:

1. Clone the repository and create a new branch with a `feature/<ticket>` prefix.
2. Implement the required changes.
3. Commit your changes.
4. Raise a Pull Request (PR).

👷‍♀️ Upon PR commit, our CI/CD pipeline will take care of running unit tests, code coverage, and code quality checks.

If all checks pass, you need to get the review of a team member, then your PR approved by the reviewer, then your PR can be merged into the main branch.

---

## 📚 Additional Resources

- **Coding Guidelines**: [Coding Standards](#)
- **Changelog**: [Latest Changes](#)
- **FAQ**: [Frequently Asked Questions](#)

---

💌 **Built with ❤️ by [7 Peaks Software](https://7peakssoftware.com/)**
