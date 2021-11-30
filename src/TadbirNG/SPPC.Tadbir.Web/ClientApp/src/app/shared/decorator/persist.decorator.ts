
import { getCurrentUser } from "./load-persist.decorator";
  
export function Persist<Type>()
{
    return (target:object,propertyName:string)=>{           
                                            
        var propName = "persistenceData";        
        let value : Type;        
        
        Object.defineProperty(target,propertyName,{
            
            get():Type
            {  
                return value;                
            },
            set(item:Type):void
            {     
                var data = target[propName];
                if(data == undefined)
                    data = new Object();

                data[propertyName] = item;
                target[propName] = data;
                value = item;           
            }
        })
    };    
}

export function SavePersist():MethodDecorator 
{
    

    return (target: object,key: string, descriptor: any) => {
        

        var propName = "persistenceData";
        const originalMethod = descriptor.value; 

        descriptor.value =  function (...args: any[]) { 
            
            debugger;

            var persistenceData = new Object();
            Object.keys(target[propName]).forEach((item)=>{
                persistenceData[item] = target[item];
            })

            var currentContext = getCurrentUser();
            var companyId = currentContext.companyId;            
            var compoentName = target.constructor.name;
            var mainkey = `values_${compoentName}_${companyId}`;
                        
            sessionStorage.setItem(mainkey, JSON.stringify(persistenceData));                
            
            const result = originalMethod.apply(this, args);                        
            return result;
        }
        
        
    };
    
}



