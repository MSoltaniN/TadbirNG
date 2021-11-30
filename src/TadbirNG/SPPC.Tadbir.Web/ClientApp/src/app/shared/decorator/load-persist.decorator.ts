
import { OnDestroy, OnInit } from '@angular/core';
import { ContextInfo } from '@sppc/core/services/authentication.service';
import { SessionKeys } from '@sppc/shared/services/browserStorage.service';

interface IComponent extends OnInit, OnDestroy {
}

interface TFunction { 
    new(...args: any[]): IComponent;
  }

  export function LoadPersist(): <T extends TFunction>(constructor: T) => T {
    return function decorator<T extends TFunction>(constructor: T): T {
      return class extends constructor {

  
        ngOnInit(): void {
            
            var componentName = (<any>this.constructor).__proto__.name;            
            var propName = "persistenceData"
            var data = extractData(componentName);
            
            if(data == null)
            {
                data = new Object();
                this[propName] = data;
            }
            else
            {
                Object.keys(data).forEach((item)=>{
                    this[item] = data[item]
                });
            }
            
            super.ngOnInit();
        }
  
        ngOnDestroy(): void {
          
          super.ngOnDestroy();
        }
      };
    };
  }

function extractData(componentName)
{
    var currentContext = getCurrentUser();
    if(currentContext == null)
        return null;

    var companyId = currentContext.companyId;  
    var mainkey = `values_${componentName}_${companyId}`;
    var data = JSON.parse(sessionStorage.getItem(mainkey));
    
    return data;
}


export function getCurrentUser(): ContextInfo | null {
    var currentUser: ContextInfo;
    var item: string | null = '';
    if (localStorage.getItem(SessionKeys.CurrentContext)) {
      item = localStorage.getItem(SessionKeys.CurrentContext);
    }
    else if (sessionStorage.getItem(SessionKeys.CurrentContext)) {
      item = sessionStorage.getItem(SessionKeys.CurrentContext);
    }

    if (item) {
      var currentUser: ContextInfo = item !== null ? JSON.parse(item) : null;
      return currentUser;
    }

    return null;
  }
