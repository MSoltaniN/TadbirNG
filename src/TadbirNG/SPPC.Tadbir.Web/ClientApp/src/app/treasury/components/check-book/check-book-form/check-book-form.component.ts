import { Component, ElementRef, EventEmitter, Input, OnInit, Output, Renderer2 } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { DetailComponent } from '@sppc/shared/class';
import { Entities } from '@sppc/shared/enum/metadata';
import { ViewName } from '@sppc/shared/security';
import { BrowserStorageService, MetaDataService } from '@sppc/shared/services';
import { CheckBook } from '@sppc/treasury/models/checkBook';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'check-book-form',
  templateUrl: './check-book-form.component.html',
  styleUrls: ['./check-book-form.component.css']
})
export class CheckBookFormComponent extends DetailComponent implements OnInit {

  @Input() public model: CheckBook;
  @Input() public isNew: boolean = false;
  @Input() public errorMessage: string = '';

  @Input() public isWizard: boolean = false;

  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() save: EventEmitter<CheckBook> = new EventEmitter();


  constructor(public toastrService: ToastrService,
     public translate: TranslateService,
     public bStorageService: BrowserStorageService,
     public renderer: Renderer2,
     public metadata: MetaDataService,
     public elem:ElementRef)
  {
    super(toastrService, translate, bStorageService, renderer, metadata, Entities.CheckBook, ViewName.CheckBook,elem);
  }

  ngOnInit(): void {
    this.editForm.reset();
  }

  public onSave(e: any): void {
    e.preventDefault();
    if (this.editForm.valid) {
      this.save.emit(this.editForm.value);
    }
  }

  public onCancel(e: any): void {
    e.preventDefault();
    this.closeForm();
  }

  private closeForm(): void {
    this.cancel.emit();
  }

  escPress() {
    this.closeForm();
  }

}
