# Framework

This is the software framework designed to be used in the new generation of Tadbir accounting and payroll system (Tadbir NG).

## Solution structure

The Visual Studio 2015 solution for this project is partitioned into 2 separate sections : Framework and Tadbir. Framework is intended to contain all infrastructure code required by a real product (like Tadbir NG), and Tadbir is intended to contain all code that is directly relevant to Tadbir NG as a product. All framework projects are prefixed with "SPPC.Framework" and all Tadbir projects are prefixed with "SPPC.Tadbir".

In later stages, when the framework is mature enough to be maintained by a separate team, all Visual Studio projects for Tadbir can be migrated to a separate solution to be maintained by a potentially different team.
