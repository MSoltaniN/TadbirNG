import { Component, Renderer2 } from '@angular/core';
import { DefaultComponent } from "../../class/default.component";
import { ToastrService } from "toastr-ng2/toastr";
import { TranslateService } from "ng2-translate";
import { MetaDataService } from '../../service/metadata/metadata.service';


@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent extends DefaultComponent {
    constructor(public toastrService: ToastrService,
        public translate: TranslateService, public renderer: Renderer2,public metadata: MetaDataService) {

        super(toastrService, translate, renderer, metadata,'','');
    }

}
