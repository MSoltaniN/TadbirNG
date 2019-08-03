import { Component, OnInit, Inject } from '@angular/core';
import { DOCUMENT } from '@angular/common';
import { BrowserStorageService } from '@sppc/shared';


@Component({
  selector: 'app-appsetting',
  templateUrl: './appsetting.component.html',
  styleUrls: ['./appsetting.component.css']
})
export class AppsettingComponent implements OnInit {

  constructor(@Inject(DOCUMENT) private document: Document, public bStorageService: BrowserStorageService) { }

  ngOnInit() {
  }


  onChangeSkin(skin: string) {
    this.document.getElementById('mainBody').classList.remove('skin-blue');
    this.document.getElementById('mainBody').classList.remove('skin-black');
    this.document.getElementById('mainBody').classList.remove('skin-purple');
    this.document.getElementById('mainBody').classList.remove('skin-red');
    this.document.getElementById('mainBody').classList.remove('skin-yellow');
    this.document.getElementById('mainBody').classList.remove('skin-green');
    this.document.getElementById('mainBody').classList.remove('skin-blue-light');
    this.document.getElementById('mainBody').classList.remove('skin-black-light');
    this.document.getElementById('mainBody').classList.remove('skin-purple-light');
    this.document.getElementById('mainBody').classList.remove('skin-green-light');
    this.document.getElementById('mainBody').classList.remove('skin-red-light');
    this.document.getElementById('mainBody').classList.remove('skin-yellow-light');

    this.bStorageService.setCurrentSkin(skin);
    this.document.getElementById('mainBody').classList.add(skin);
  }
}
