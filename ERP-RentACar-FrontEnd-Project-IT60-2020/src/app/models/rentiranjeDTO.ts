import { Guid } from "guid-typescript";

export class RentiranjeDTO {
    brojDanaIzdavanja!:number;
    datumPocetkaIzdavanja!:Date;
    datumKrajaIzdavanja!:Date;
    ukupnaCenaRentiranja!:number;
    placenoR!:String;
    datumPlacanja?:Date;
    pristupniKod!:number;
    automobilId!:string;
    korisnikId!:string;
    zaposleniId!:string;
    stripeToken!:String;

    /*
    constructor(
        brojDanaIzdavanja:number,
        datumPocetkaIzdavanja:Date,
        datumKrajaIzdavanja:Date,
        ukupnaCenaRentiranja:number,
        placenoR:String,
        datumPlacanja:Date,
        pristupniKod:number,
        automobilId:string,
        korisnikId:Guid,
        zaposleniId:Guid,
        stripeToken:String
    ) {
        this.brojDanaIzdavanja = brojDanaIzdavanja;
        this.datumPocetkaIzdavanja = datumPocetkaIzdavanja;
        this.datumKrajaIzdavanja = datumKrajaIzdavanja;
        this.ukupnaCenaRentiranja = ukupnaCenaRentiranja;
        this.placenoR = placenoR;
        this.datumPlacanja = datumPlacanja;
        this.pristupniKod = pristupniKod;
        this.automobilId = automobilId;
        this.korisnikId = korisnikId;
        this.zaposleniId = zaposleniId;
        this.stripeToken = stripeToken;
    }
    */
}