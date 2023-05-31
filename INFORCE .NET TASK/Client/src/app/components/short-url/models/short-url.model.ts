import { IUserModel } from "./user.model";

export interface IShortUrlModel {
    id:string,
    longUrl: string,
    host: string,
    urlCode: string,
    createdDate: Date,
    createdBy: IUserModel;
}