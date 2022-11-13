import { THIS_EXPR } from "@angular/compiler/src/output/output_ast";
import {
  AfterContentInit,
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewChild,
} from "@angular/core";
import { WidgetSetting } from "@sppc/shared/models/widgetSetting";
import { ChartService } from "@sppc/shared/services/widget.service";
import * as echarts from "echarts";

@Component({
  selector: "chart-widget",
  templateUrl: "./chart-widget.component.html",
  styleUrls: ["./chart-widget.component.css"],
})
export class ChartWidgetComponent implements OnInit, AfterContentInit {
  @Input() type: string = "bar";
  @Input() data;
  // @Input() initOptions;
  @Input() title;
  options: any;
  id: number;

  constructor(private chartService: ChartService) {
    this.id = Math.floor(Math.random() * 999999);
    console.log(this.id);
  }
  ngAfterContentInit(): void {
    //this.setOption();
  }

  // @Input() set initOptions(value) {
  //   this.options = value;
  //   const element: any = document.getElementById(this.id.toString());
  //   if (element) {
  //     let echartsObj = echarts.init(element);
  //     if (echartsObj) echartsObj.setOption(value);
  //   }
  // }

  @Input() set initOptions(value) {
    if (value) this.options = JSON.parse(JSON.stringify(value));

    //this.setOption();
  }

  getWidth() {
    const element: any = document.getElementById(this.id.toString());
    if (element) {
      let echartsObj = echarts.getInstanceByDom(element);
      if (echartsObj) return echartsObj.getWidth();
    }
  }

  getHeight() {
    const element: any = document.getElementById(this.id.toString());
    if (element) {
      let echartsObj = echarts.getInstanceByDom(element);
      if (echartsObj) return echartsObj.getHeight();
    }
  }

  ngOnInit() {
    console.table("options:" + this.id + " " + this.options);
  }

  changeSettings(settings: WidgetSetting) {
    debugger;

    const singleType = settings.series.every(
      (val, i, arr) => val.type === arr[0].type
    );
    let newOptions: any = {};
    if (singleType) {
      const type = Number(settings.series[0].type);
      newOptions = this.chartService.getOptions(type, this.data);
    }

    const element: any = document.getElementById(this.id.toString());
    let echartsObj = echarts.getInstanceByDom(element);
    echartsObj.clear();
    const defOption = echartsObj.getOption();
    defOption.legend[0].show = false;
    this.options = null;

    //echartsObj.setOption(defOption);

    // setTimeout(() => {
    //   this.options = Object.assign({}, newOptions);
    //   echartsObj.setOption(newOptions, true, true);
    // }, 10);

    // setTimeout(() => {
    //   this.chart.reinit();
    // }, 10);
  }
}
