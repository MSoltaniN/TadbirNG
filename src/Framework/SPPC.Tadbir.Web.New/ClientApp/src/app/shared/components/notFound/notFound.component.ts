import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';


@Component({
  selector: 'notFound',
  templateUrl: 'notFound.component.html',
  styleUrls: ['./notFound.component.css']

})
export class NotFoundComponent implements OnInit {
   
    constructor(private router: Router) 
    {
    }

    ngOnInit() {       
    }

}
