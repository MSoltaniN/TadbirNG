import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable()
export class ScopeService {   
 
  private scope = new Subject<any>();
  
  constructor() { } 
  

  public setScope(component: any) {
    return this.scope.next(component);
  }

  public getScope() {
    return this.scope.asObservable();
  }

  public clearScope() {
    this.scope = new Subject<any>();
    return this.scope.next();
  }
}
