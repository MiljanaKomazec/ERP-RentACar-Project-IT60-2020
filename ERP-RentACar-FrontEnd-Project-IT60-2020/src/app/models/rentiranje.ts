import { Guid } from "guid-typescript";
import { Automobil } from "./automobil";
import { Korisnik } from "./korisnik";

export class Rentiranje {
    rentiranjeId!:Guid;
    brojDanaIzdavanja!:number;
    datumPocetkaIzdavanja!:Date;
    datumKrajaIzdavanja!:Date;
    ukupnaCenaRentiranja!:number;
    placenoR!:String;
    datumPlacanja?:Date;
    pristupniKod!:number;
    automobilId!:Guid;
    automobil!:Automobil;
    korisnikId!:Guid;
    korisnik!:Korisnik;
}