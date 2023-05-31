export interface ILoginModel {
    Name: string,
    Password: string
}
export class LoginModel implements ILoginModel {
    constructor(public Name: string, public Password: string ){
        
    }
}
