import { Guid } from 'guid-typescript';

export class Korisnik{
    korisnikId?:Guid;
    jmbgK?:String;
    imeK!:String;
    przK!:String;
    gradK!:String;
    adresaK!:String;
    kontaktK!:String;
    uloga?:String;
    username!:String;
    password!:String;
}