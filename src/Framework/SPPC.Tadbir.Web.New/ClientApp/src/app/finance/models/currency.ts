// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.665
//     Template Version: 1.0
//     Generation Date: 7/20/2019 12:06:36 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------


export interface Currency {
  id: number;
  name: string;
  country: string;
  code: string;
  minorUnit: string;
  multiplier: number;
  decimalCount: number;
  description?: string;
  branchScope: number;
  isActive: boolean;
}