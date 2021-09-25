import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { BrowserStorageService } from '@sppc/shared/services';
import * as moment from 'jalali-moment';

@Component({
  selector: 'app-license-info',
  templateUrl: './license-info.component.html',
  styleUrls: ['./license-info.component.css']
})
export class LicenseInfoComponent implements OnInit {
  @Output() cancel: EventEmitter<any> = new EventEmitter();

  constructor(private bStorageService: BrowserStorageService) { }

  customerName: string;
  contactName: string;
  userCount: string;
  edition: string;
  startDate: string;
  endDate: string;

  ngOnInit() {
    
    var licenseInfo = this.bStorageService.getLicenseInfo();
    this.customerName = licenseInfo.customerName;
    this.contactName = licenseInfo.contactName;
    this.userCount = licenseInfo.userCount;
    this.edition = licenseInfo.edition;
    this.startDate = moment(licenseInfo.startDate).locale('fa').format("YYYY/MM/DD");
    this.endDate = moment(licenseInfo.endDate).locale('fa').format("YYYY/MM/DD");
  }

  escPress() {
    this.cancel.emit();
  }
}
