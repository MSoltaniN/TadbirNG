
export function Persist<Type>(defaultValue:Type,entityName?:string,entityAction?:number)
{
    return (target:object,propertyName:string)=>{        
        var value = target[propertyName];
        var key = target.constructor.name;
        var tempKey = `temp_${target.constructor.name}`;

        Object.defineProperty(target,propertyName,{
            get():Type
            {             
                if (entityName && entityAction) 
                {   
                    if(this.isAccess(entityName, entityAction))                        
                    {                        
                        value = getValueFromStorage(key,propertyName);
                        setValueToStorage(tempKey,propertyName,value);
                    }
                    else
                        value = defaultValue;
                }  
                else
                {
                    if(getValueFromStorage(key,propertyName))
                    {
                        value = getValueFromStorage(key,propertyName);
                        setValueToStorage(tempKey,propertyName,value);
                    }
                    else
                    {
                        value = defaultValue;
                    }
                }

                return value;
            },
            set(item:Type):void
            {                
                setValueToStorage(tempKey,propertyName,item);
            }
        })
    };    
}

export function SavePersist():MethodDecorator 
{
    

    return (target: object,key: string, descriptor: any) => {
        

        const originalMethod = descriptor.value; 

        descriptor.value =  function (...args: any[]) { 

            var key = target.constructor.name;
            var tempKey = `temp_${target.constructor.name}`;
            if (sessionStorage.getItem(tempKey)) {
                sessionStorage.setItem(key, sessionStorage.getItem(tempKey));
                //sessionStorage.removeItem(tempKey);
            }

            const result = originalMethod.apply(this, args);                        
            return result;
        }
        
        
    };
    
}

function getValueFromStorage(key,propertyName)
{
    var tempKey = `temp_${key}`;
    if(sessionStorage.getItem(tempKey) != null)
    {
        var state = JSON.parse(sessionStorage.getItem(tempKey));
        if(state && state[propertyName] != undefined)
            return state[propertyName];
    }

    var state = JSON.parse(sessionStorage.getItem(key));
    if(state)
        return state[propertyName];

    return null;
}


function setValueToStorage(key,propertyName,item)
{
    var state = JSON.parse(sessionStorage.getItem(key));
    if(!state)
        state = new Object();

    state[propertyName] = item;
    sessionStorage.setItem(key,JSON.stringify(state));    
}

