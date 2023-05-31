export interface IRegistrationModel  {
    Name: string,
    Password: string,
    ConfirmPassword: string 
}

export class RegistrationModel implements IRegistrationModel {
    constructor(public Name: string, public Password: string, public ConfirmPassword: string  ){
        
    }
}