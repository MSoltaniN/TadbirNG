// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1717:Only FlagsAttribute enums should have plural names", Justification = "Warning is a false positive since status is not plural.", Scope = "type", Target = "~T:SPPC.Tadbir.Domain.DocumentStatus")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1028:Enum Storage should be Int32", Justification = "Default int type causes runtime problems when mapping from string values, because model type is short.", Scope = "type", Target = "~T:SPPC.Tadbir.Domain.TurnoverMode")]