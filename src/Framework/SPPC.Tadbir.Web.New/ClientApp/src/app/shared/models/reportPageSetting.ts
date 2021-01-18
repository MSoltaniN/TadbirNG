

export class ReportPageSetting {

  constructor(public pageSize: string = "A4", public pageOrientation: string = "Portrait", public columnFitPage: boolean = true,
    public marginTop: number = 0.2, public marginBottom: number = 0.2, public marginLeft: number = 0.2, public marginRight: number = 0.2) { }
  
}
